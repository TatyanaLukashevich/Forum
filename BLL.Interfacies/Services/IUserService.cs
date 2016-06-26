using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IUserService
    {
        UserEntity GetUserEntity(int id);
        IEnumerable<UserEntity> GetAllUserEntities();
        void CreateUser(UserEntity user);
        void DeleteUser(UserEntity user);
        UserEntity GetUserByEmail(string email);
        void ChangePassword(string email, string newPassword);
        void ChangeRole(string email, int roleId);
        //etc.
    }
}