using System;
using System.Collections.Generic;
using System.Linq;
using DapperExtensions;
using DLearnRepositories.DapperEntities;
using DLearnRepositories.UnitOfWork;
using DLearnServices.Entities;
using DLearnServices.Interfaces;

namespace DLearnServices.Services
{
    #region User Service
    public class UserService : IUserService
    {
        #region Private Declarations
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Constructor
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        #endregion

        #region User Service Methods
        public IEnumerable<StatesEntity> GetAllStates()
        {
            var statesList = _unitOfWork.StatesRepository.GetAll().Where(s => s.ISACTIVE).Select(s => new StatesEntity
            {
                StateId = s.STATEID,
                StateName = s.STATENAME
            });
            
            return statesList;
        }

        public UserValidationEntity GetUserDetailsByEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                PredicateGroup predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                predicateGroup.Predicates.Add(Predicates.Field<USERS>(u => u.EMAIL, Operator.Eq, email));
                predicateGroup.Predicates.Add(Predicates.Field<USERS>(u => u.ISACTIVE, Operator.Eq, true));
                USERS dbUser = _unitOfWork.UserRepository.Find(predicateGroup).SingleOrDefault();
                if (dbUser != null)
                {
                    UserValidationEntity userValidationEntity = new UserValidationEntity()
                    {
                        DOB = dbUser.DATEOFBIRTH,
                        Email = dbUser.EMAIL,
                        FullName = dbUser.FULLNAME,
                        PasswordHash = dbUser.PASSWORDHASH,
                        Salt = dbUser.SALT,
                        UserId = dbUser.USERID
                    };
                    return userValidationEntity;
                }
            }
            return null;
        }
        #endregion

        public void UpdateUserTimestamp(Guid userID)
        {
            USERS dbUser = _unitOfWork.UserRepository.Get(userID);
            dbUser.LASTMODIFIED = DateTime.Now;
            dbUser.LASTLOGON = DateTime.Now;
            _unitOfWork.UserRepository.Update(dbUser);
        }

        public Guid CreateUser(UserCreateRequestEntity userCreateRequest)
        {
            if (userCreateRequest != null)
            {
                USERS dbUser = new USERS
                {
                    DATEOFBIRTH = userCreateRequest.DOB,
                    EMAIL = userCreateRequest.Email,
                    FIRSTNAME = userCreateRequest.FirstName,
                    FULLNAME = userCreateRequest.FullName,
                    GENDER = userCreateRequest.Gender,
                    LASTLOGON = DateTime.Now,
                    LASTNAME = userCreateRequest.LastName,
                    PASSWORDHASH = userCreateRequest.PasswordHash,
                    PHONENUMBER = userCreateRequest.Phone,
                    SALT = userCreateRequest.Salt,
                    SUBSCRIPTIONTYPE = userCreateRequest.SubscriptionType,
                    CREATEDON = DateTime.Now,
                    ISACTIVE = true
                };
                return _unitOfWork.UserRepository.InsertWithReturnGuidId(dbUser);
            }
            return Guid.Empty;
        }

        public long AddUserAddress(AddressEntity addressEntity)
        {
            if (addressEntity != null)
            {
                USERADDRESS dbUserAddress = new USERADDRESS
                {
                    ADDRESSLINE1 = addressEntity.AddressLine1,
                    ADDRESSLINE2 = addressEntity.AddressLine2,
                    CITY = addressEntity.City,
                    LANDMARK = addressEntity.Landmark,
                    PINCODE = addressEntity.PinCode,
                    STATEID = addressEntity.StateId,
                    USERID = addressEntity.UserId,
                    CREATEDON = DateTime.Now,
                    COUNTRYCODE = "IN",
                    ADDRESSID = addressEntity.AddressId
                };

                if (addressEntity.AddressId == 0)
                {
                    return _unitOfWork.UserAddressRepository.InsertWithReturnLongId(dbUserAddress);
                }
                else
                {
                    _unitOfWork.UserAddressRepository.Update(dbUserAddress);
                    return addressEntity.AddressId;
                }
            }
            return 0;
        }
    }
    #endregion
}
