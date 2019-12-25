using System;

namespace DLearnServices.Entities
{
    public class AddressEntity
    {
        public long AddressId { get; set; }
        public Guid UserId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        public int StateId { get; set; }
        public int PinCode { get; set; }
    }
}
