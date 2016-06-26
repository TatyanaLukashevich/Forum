using BLL.Interface.Entities;
using MvcPL.Models;

namespace MvcPL.Infrastructure.Mappers
{
    public static class MessageMapper
    {
        public static MessageViewModel ToMvcMessage(this MessageEntity messageEntity)
        {
            return new MessageViewModel()
            {
                Id = messageEntity.Id,
                Body = messageEntity.Body,
                DateOfPost = messageEntity.DateOfMessage,
                PostId = messageEntity.PostID,
                UserId = messageEntity.AuthorId,
                ReplyId = messageEntity.ReplyId
            };
    }

    public static MessageEntity ToBllMessage(this MessageViewModel messageViewModel)
    {
        return new MessageEntity()
        {
            Id = messageViewModel.Id,
            Body = messageViewModel.Body,
            DateOfMessage = messageViewModel.DateOfPost,
            PostID = messageViewModel.PostId,
            AuthorId = messageViewModel.UserId,
            ReplyId = messageViewModel.ReplyId
        };
    }


}
}