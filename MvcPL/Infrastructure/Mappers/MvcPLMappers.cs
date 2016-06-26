using BLL.Interface.Entities;
using MvcPL.Models;
using System.Linq;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MvcMappers
    {
        public static UserViewModel ToMvcUser(this UserEntity userEntity)
        {
            return new UserViewModel()
            {
                UserId = userEntity.UserId,
                Login = userEntity.Login,
                Role = (Role)userEntity.RoleId,
                Email = userEntity.Email,
                Password = userEntity.Password,
                UserPost = userEntity.UserPost.Select(p => new PostViewModel()
                { PostId = p.Id,
                    PostName = p.Name,
                    AuthorLogin = p.AuthorLogin,
                    DateOfPost = p.DateOfPost,
                    SectionId = p.SectionId
                }),
                UserMessage = userEntity.UserMessage.Select(p => new MessageViewModel()
                {
                    Id = p.Id,
                    Body = p.Body,
                    AuthorLogin = p.AuthorLogin,
                    DateOfPost = p.DateOfMessage,
                    UserId = p.AuthorId,
                    PostId = p.PostID
                }),

            };
        }

        public static UserEntity ToBllUser(this Models.UserViewModel userViewModel)
        {
            return new UserEntity()
            {
                UserId = userViewModel.UserId,
                Login = userViewModel.Login,
                RoleId = (int)userViewModel.Role,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
            };
        }


        public static UserEntity ToBllUser(this RegisterViewModel userViewModel)
        {
            return new UserEntity()
            {
                UserId = userViewModel.Id,
                Login = userViewModel.Username,
                RoleId = userViewModel.RoleId,
                Email = userViewModel.Email,
                Password = userViewModel.Password,
            };
        }

        public static RegisterViewModel ToMvcUserRegister(this UserEntity userEntity)
        {
            return new RegisterViewModel()
            {
                Id = userEntity.UserId,
                Username = userEntity.Login,
                RoleId = userEntity.RoleId,
                Email = userEntity.Email,
                Password = userEntity.Password,
            };
        }
    }
}