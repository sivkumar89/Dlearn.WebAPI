using DLearnInfrastructure.Utilities;
using Microsoft.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DLearnAPI.Providers
{
    public class DLearnAuthMiddleware : OwinMiddleware
    {
        public DLearnAuthMiddleware(OwinMiddleware next) : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var owinRequest = context.Request;
            var owinResponse = context.Response;
            //buffer the response stream for later
            var owinResponseStream = owinResponse.Body;
            //buffer the response stream in order to intercept downstream writes
            using (var responseBuffer = new MemoryStream())
            {
                //assign the buffer to the resonse body
                owinResponse.Body = responseBuffer;

                await Next.Invoke(context).ConfigureAwait(false);

                //reset body
                owinResponse.Body = owinResponseStream;

                if (responseBuffer.CanSeek && responseBuffer.Length > 0 && responseBuffer.Position > 0)
                {
                    //reset buffer to read its content
                    responseBuffer.Seek(0, SeekOrigin.Begin);
                }

                if (!IsSuccessStatusCode(owinResponse.StatusCode) && responseBuffer.Length > 0)
                {
                    //NOTE: perform your own content negotiation if desired but for this, using JSON
                    var headerValues = context.Response.Headers.GetValues(DLearnConstants.OwinChallengeFlag);
                    string httpStatusCode = headerValues.FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(httpStatusCode))
                    {
                        owinResponse.StatusCode = Convert.ToInt32(httpStatusCode);
                    }
                    var body = await CreateCommonApiResponse(owinResponse, responseBuffer).ConfigureAwait(false);
                    
                    var content = JsonConvert.SerializeObject(body);

                    var mediaType = MediaTypeHeaderValue.Parse(owinResponse.ContentType);
                    using (var customResponseBody = new StringContent(content, Encoding.UTF8, mediaType.MediaType))
                    {
                        var customResponseStream = await customResponseBody.ReadAsStreamAsync().ConfigureAwait(false);
                        await customResponseStream.CopyToAsync(owinResponseStream, (int)customResponseStream.Length, owinRequest.CallCancelled).ConfigureAwait(false);
                        owinResponse.ContentLength = customResponseStream.Length;
                    }
                }
                else
                {
                    //copy buffer to response stream this will push it down to client
                    await responseBuffer.CopyToAsync(owinResponseStream, (int)responseBuffer.Length, owinRequest.CallCancelled).ConfigureAwait(false);
                    owinResponse.ContentLength = responseBuffer.Length;
                }
            }
        }

        async Task<object> CreateCommonApiResponse(IOwinResponse owinResponse, Stream stream)
        {

            string json = await new StreamReader(stream).ReadToEndAsync().ConfigureAwait(false);

            string responseReason = ((HttpStatusCode)owinResponse.StatusCode).ToString();

            //Is this a HttpError
            var httpError = JsonConvert.DeserializeObject<HttpError>(json);
            if (httpError != null)
            {
                return new
                {
                    error = responseReason,
                    error_description = (object)httpError.Where(i => i.Key == "error").Select(i => i.Value).FirstOrDefault()
                    ?? (object)httpError.MessageDetail
                    ?? (object)httpError.ModelState
                    ?? (object)httpError.ExceptionMessage
                };
            }

            //Is this an OAuth Error
            var oAuthError = Newtonsoft.Json.Linq.JObject.Parse(json);
            if (oAuthError["error"] != null && oAuthError["error_description"] != null)
            {
                dynamic obj = oAuthError;
                return new
                {
                    error = (string)obj.error,
                    error_description = (object)obj.error_description
                };
            }

            //Is this some other unknown error (Just wrap in common model)
            var error = JsonConvert.DeserializeObject(json);
            return new
            {
                error = responseReason,
                error_description = error
            };
        }

        bool IsSuccessStatusCode(int statusCode)
        {
            return statusCode >= 200 && statusCode <= 299;
        }
    }
}