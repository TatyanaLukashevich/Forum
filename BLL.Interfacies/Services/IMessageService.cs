using System.Collections.Generic;
using BLL.Interface.Entities;

namespace BLL.Interface.Services
{
    public interface IMessageService
    {
        MessageEntity GetMessageEntity(int id);
        IEnumerable<MessageEntity> GetAllMessageEntities();
        void CreateMessage(MessageEntity message);
        void DeleteMessage(MessageEntity post);     
    }
}
