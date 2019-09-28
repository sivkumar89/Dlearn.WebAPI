using DLearnServices.Entities;
using System.Collections.Generic;

namespace DLearnServices.Interfaces
{
    public interface IUserService
    {
        IList<UserEntity> GetAllUsers();
    }
}
