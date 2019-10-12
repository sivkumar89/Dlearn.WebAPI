using DLearnServices.Entities;
using System;
using System.Collections.Generic;

namespace DLearnServices.Interfaces
{
    public interface IUserService
    {
        IList<UserEntity> GetAllUsers();
        UserValidationEntity GetUserDetailsByEmail(string email);
        void UpdateUserTimestamp(Guid userID);
        Guid CreateUser(UserCreateRequestEntity userCreateRequest);
    }
}
