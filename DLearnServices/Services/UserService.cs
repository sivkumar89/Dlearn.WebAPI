using DapperExtensions;
using DLearnRepositories.DapperEntities;
using DLearnRepositories.UnitOfWork;
using DLearnServices.Entities;
using DLearnServices.Interfaces;
using System.Collections.Generic;
using System.Linq;

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
        public IList<UserEntity> GetAllUsers()
        {
            List<UserEntity> userEntities = new List<UserEntity>();
            var userList = _unitOfWork.UserRepository.GetAll();
            foreach (var item in userList)
            {
                UserEntity userEntity = new UserEntity
                {
                    DOB = item.DATEOFBIRTH,
                    Email = item.EMAIL,
                    FirstName = item.FIRSTNAME,
                    FullName = item.FULLNAME,
                    Gender = item.GENDER,
                    LastName = item.LASTNAME,
                    Phone = item.PHONENUMBER,
                    SubscriptionType = item.SUBSCRIPTIONTYPE,
                    UserId = item.USERID
                };
                userEntities.Add(userEntity);
            }
            return userEntities;
        }

        public UserValidationEntity GetUserDetailsByEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                PredicateGroup predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                predicateGroup.Predicates.Add(Predicates.Field<USERS>(u => u.EMAIL, Operator.Eq, email));
                predicateGroup.Predicates.Add(Predicates.Field<USERS>(u => u.ISACTIVE, Operator.Eq, true));
                USERS userDetail = _unitOfWork.UserRepository.Find(predicateGroup).SingleOrDefault();
                if (userDetail != null)
                {
                    UserValidationEntity userValidationEntity = new UserValidationEntity()
                    {
                        DOB = userDetail.DATEOFBIRTH,
                        Email = userDetail.EMAIL,
                        FullName = userDetail.FULLNAME,
                        PasswordHash = userDetail.PASSWORDHASH,
                        Salt = userDetail.SALT,
                        UserId = userDetail.USERID
                    };
                    return userValidationEntity;
                }
            }
            return null;
        }
        #endregion
    }
    #endregion
}
