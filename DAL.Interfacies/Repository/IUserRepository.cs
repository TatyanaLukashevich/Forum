using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface IUserRepository /*: IRepository<DalUser>//Add user repository methods! */
    {
        IEnumerable<DalUser> GetAll();
        DalUser GetById(int key);
        DalUser GetUserByEmail(string email);
        void Create(DalUser user);
        void ChangePassword(string email, string newPassword);
        void Delete(DalUser e);
        void ChangeRole(string email, int roleId);
    }
}