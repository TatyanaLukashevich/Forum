using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;


namespace MvcPL.Providers
{
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserService
            => (IUserService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IUserService));

        public IRoleService RoleService
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof(IRoleService));

        public override bool IsUserInRole(string email, string roleName)
        {

            UserEntity user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Email == email);

            if (user == null) return false;

            RoleEntity userRole = RoleService.GetRoleById(user.RoleId);

            if (userRole != null && userRole.Name == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {

            var roles = new string[] { };
            var user = UserService.GetAllUserEntities().FirstOrDefault(u => u.Email == email);

            if (user == null) return roles;

            var userRole = RoleService.GetRoleById(user.RoleId);

            if (userRole != null)
            {
                roles = new string[] { userRole.Name };
            }
            return roles;

        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() { Name = roleName };

            using (var context = new DbContext("Forum"))
            {
                RoleService.CreateRole(newRole);
                context.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var roles = RoleService.GetAllRoles();

            string[] rolesArrau = new string[] { };
            int i = 0;
            foreach (var role in roles)
            {
                rolesArrau[i] = role.Name;
                i++;
            }

            return rolesArrau;
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName { get; set; }
    }
}