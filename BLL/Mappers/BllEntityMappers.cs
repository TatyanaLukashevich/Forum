using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
    public static class BllEntityMappers
    {
        public static DalUser ToDalUser(this UserEntity userEntity)
        {
            return new DalUser()
            {
                Id = userEntity.UserId,
                Login = userEntity.Login,
                RoleId = userEntity.RoleId,
                Email = userEntity.Email,
                Password = userEntity.Password,    
            };
        }

        public static UserEntity ToBllUser(this DalUser dalUser)
        {
            return new UserEntity()
            {
                UserId = dalUser.Id,
                Login = dalUser.Login,
                RoleId = dalUser.RoleId,
                Email = dalUser.Email,
                Password = dalUser.Password,
                UserPost = dalUser.UserPost.Select(p => new PostEntity() { Id = p.Id, Name = p.Name, AuthorLogin = p.AuthorLogin, DateOfPost = p.DateOfPost, SectionId = p.SectionId })
            };
        }

        public static DalRole ToDalRole(this RoleEntity roleEntity)
        {
            return new DalRole()
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name,
                Description = roleEntity.Description
            };
        }

        public static RoleEntity ToBllRole(this DalRole dalRole)
        {
            return new RoleEntity()
            {
                Id = dalRole.Id,
                Name = dalRole.Name,
                Description = dalRole.Description
            };
        }
    }
}
