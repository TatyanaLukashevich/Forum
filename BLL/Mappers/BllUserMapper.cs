using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
    public static class BllUserMapper
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
                UserPost = dalUser.UserPost.Select(p => new PostEntity()
                {
                    Id = p.Id,
                    Name = p.Name,
                    AuthorLogin = p.AuthorLogin,
                    DateOfPost = p.DateOfPost,
                    SectionId = p.SectionId
                }),
                UserMessage = dalUser.UserMessage.Select(p => new MessageEntity()
                {
                    Id = p.Id,
                    Body = p.Body,
                    AuthorLogin = p.AuthorLogin,
                    DateOfMessage = p.DateOfMessage,
                    AuthorId = p.AuthorId,
                    PostID = p.PostID
                }),
            };
        }
    }
}
