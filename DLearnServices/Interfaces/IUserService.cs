using DLearnServices.Entities;
using System;
using System.Collections.Generic;

namespace DLearnServices.Interfaces
{
    public interface IUserService
    {
        IEnumerable<StatesEntity> GetAllStates();
        UserValidationEntity GetUserDetailsByEmail(string email);
        void UpdateUserTimestamp(Guid userID);
        Guid CreateUser(UserCreateRequestEntity userCreateRequest);
        long AddUserAddress(AddressEntity addressEntity);
    }
}
