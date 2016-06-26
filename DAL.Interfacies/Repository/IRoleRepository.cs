using System.Collections.Generic;
using DAL.Interface.Repository;
using DAL.Interface.DTO;

namespace DAL.Interface.Repository
{
    #region Delegate
    public delegate DalRole TagForSearch(string tag);
    #endregion
    public interface IRoleRepository 
    {
        IEnumerable<DalRole> GetAllRoles();
        void CreateNewRole(DalRole role);
        DalRole GetById(int? roleId);
        void Delete(DalRole role);
    }
}
