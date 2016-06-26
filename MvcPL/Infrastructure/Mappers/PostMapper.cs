using BLL.Interface.Entities;
using MvcPL.Models;
using System.Linq;

namespace MvcPL.Infrastructure.Mappers
{
    public static class PostMapper
    {
        public static PostViewModel ToMvcPost(this PostEntity postEntity)
        {
            return new PostViewModel()
            {
                PostId = postEntity.Id,
                PostName = postEntity.Name,
                Body = postEntity.Body,
                DateOfPost = postEntity.DateOfPost,
                AuthorLogin = postEntity.AuthorLogin,
                AmountMessages = postEntity.AmountMessages,
                MessagesToPost = postEntity.PostMessage.Select(m => new MessageViewModel() {AuthorLogin = m.AuthorLogin, Body = m.Body, DateOfPost=m.DateOfMessage, Id = m.Id, PostId = m.PostID }),
                AuthorId = postEntity.AuthorId,
                Section = (Section)postEntity.SectionId,
            };
        }

        public static PostEntity ToBllPost(this PostViewModel postViewModel)
        {
            return new PostEntity()
            {
               Id = postViewModel.PostId,
               Name = postViewModel.PostName,
               Body = postViewModel.Body,
               DateOfPost = postViewModel.DateOfPost,
               AuthorId = postViewModel.AuthorId,
               SectionId = (int)postViewModel.Section
            };
        }

        
    }
}