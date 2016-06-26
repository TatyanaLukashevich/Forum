using System.Collections.Generic;
using BLL.Interface.Entities;


namespace BLL.Interface.Services
{
    public interface IRoleService
    {
        RoleEntity GetRoleById(int id);
        void CreateRole(RoleEntity role);
        IEnumerable<RoleEntity> GetAllRoles();
        void DeleteRole(RoleEntity role);
    }
}
