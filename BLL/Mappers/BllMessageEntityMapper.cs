using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
   public static class BllMessageEntityMapper
    {
        public static DalMessage ToDalMessage(this MessageEntity messageEntity)
        {
            return new DalMessage()
            {
                Id = messageEntity.Id,
                Body = messageEntity.Body,
                DateOfMessage = messageEntity.DateOfMessage,
                AuthorLogin = messageEntity.AuthorLogin,
                AuthorId = messageEntity.AuthorId,
                PostID = messageEntity.PostID,
                ReplyId = messageEntity.ReplyId
            };
        }

        public static MessageEntity ToBllMessage(this DalMessage dalMessage)
        {
            return new MessageEntity()
            {
                Id = dalMessage.Id,
                Body = dalMessage.Body,
                DateOfMessage = dalMessage.DateOfMessage,
                AuthorLogin = dalMessage.AuthorLogin,
                AuthorId = dalMessage.AuthorId,
                PostID = dalMessage.PostID,
                ReplyId = dalMessage.ReplyId
            };
        }
    }
}
