using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
   public static class BllPostEntityMapper
    {
        public static DalPost ToDalPost(this PostEntity postEntity)
        {
            return new DalPost()
            {
                Id = postEntity.Id,
                Name = postEntity.Name,
                Body = postEntity.Body,
                DateOfPost = postEntity.DateOfPost,
                AuthorId = postEntity.AuthorId,
                SectionId = postEntity.SectionId              
            };
        }

        public static PostEntity ToBllPost(this DalPost dalPost)
        {
            return new PostEntity()
            {
                Id = dalPost.Id,
                Name = dalPost.Name,
                Body = dalPost.Body,
                AuthorLogin = dalPost.AuthorLogin,
                AmountMessages = dalPost.AmountMessages,
                PostMessage = dalPost.PostMessage.Select(m => new MessageEntity()
                { AuthorLogin = m.AuthorLogin, Body = m.Body, DateOfMessage = m.DateOfMessage, Id = m.Id, PostID = m.PostID }),
                DateOfPost = dalPost.DateOfPost,
                AuthorId = dalPost.AuthorId,
                SectionId = dalPost.SectionId
            };
        }
    }
}
