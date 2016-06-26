using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DbContext context;

        public RoleRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region Public methods
        public IEnumerable<DalRole> GetAllRoles()
        {
            NullRefCheck();
            return context.Set<Role>().Select(role => new DalRole()
            {
                Id = role.RoleId,
                Name = role.Name,
               Description = role.Description,
            });
        }

        public DalRole GetById(int? key)
        {
            NullRefCheck();
            var ormrole = context.Set<Role>().FirstOrDefault(role => role.RoleId == key);
            return new DalRole()
            {
                Id = ormrole.RoleId,
                Name = ormrole.Name,
                Description = ormrole.Description,
            };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            NullRefCheck();
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void CreateNewRole(DalRole e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var role = new Role()
            {
                RoleId = e.Id,
                Name = e.Name,
                Description =e.Description,
            };
            context.Set<Role>().Add(role);
        }

        public void Delete(DalRole e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var role = new Role()
            {              
                Name = e.Name,
                RoleId = e.Id,
                Description = e.Description
            };
            role = context.Set<Role>().Single(r => r.RoleId == role.RoleId);
            context.Set<Role>().Remove(role);
        }

        public void Update(DalUser entity)
        {
            NullRefCheck();
            throw new NotImplementedException();
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