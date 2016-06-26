using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
   public class RoleService :IRoleService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IRoleRepository roleRepository;
        #endregion

        #region Delegate
        public delegate RoleEntity TagForSearch(string tag);
        #endregion

        #region Constructor
        public RoleService(IUnitOfWork uow, IRoleRepository repository)
        {
            this.uow = uow;
            this.roleRepository = repository;
        }
        #endregion

        #region Public methods
        public RoleEntity GetRoleById(int id)
        {
            NullRefCheck();
            return roleRepository.GetById(id).ToBllRole();
        }

        public void CreateRole(RoleEntity role)
        {
            NullRefCheck();
            ArgumentNullCheck(role);
            roleRepository.CreateNewRole(role.ToDalRole());
            uow.Commit();
        }

        public IEnumerable<RoleEntity> GetAllRoles()
        {
            NullRefCheck();
            return roleRepository.GetAllRoles().Select(role => role.ToBllRole());
        }

        public void DeleteRole(RoleEntity role)
        {
            NullRefCheck();
            ArgumentNullCheck(role);
            roleRepository.Delete(role.ToDalRole());
            uow.Commit();
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("role");
            }
        }

        private void NullRefCheck()
        {
            if (this == null)
            {
                throw new NullReferenceException("role");
            }
        }
        #endregion
    }
}
