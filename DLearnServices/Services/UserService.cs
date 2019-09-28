using DLearnRepositories.UnitOfWork;
using DLearnServices.Entities;
using DLearnServices.Interfaces;
using System.Collections.Generic;

namespace DLearnServices.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IList<UserEntity> GetAllUsers()
        {
            List<UserEntity> userEntities = new List<UserEntity>();
            var userList = _unitOfWork.UserRepository.GetAll();
            foreach (var item in userList)
            {
                UserEntity userEntity = new UserEntity
                {
                    DOB = item.DATE_OF_BIRTH,
                    Email = item.EMAIL,
                    FirstName = item.FIRST_NAME,
                    FullName = item.FULL_NAME,
                    Gender = item.GENDER,
                    LastName = item.LAST_NAME,
                    Phone = item.PHONE_NUMBER,
                    SubscriptionType = item.SUBSCRIPTION_TYPE,
                    UserId = item.USER_ID
                };
                userEntities.Add(userEntity);
            }
            return userEntities;
        }
    }
}
