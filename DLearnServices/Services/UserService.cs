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
    }
}
