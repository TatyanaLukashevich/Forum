using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using System;

namespace BLL.Services
{
   public class MessageService :IMessageService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IMessageRepository messageRepository;
        #endregion

        #region Constructor
        public MessageService(IUnitOfWork uow, IMessageRepository repository)
        {
            this.uow = uow;
            this.messageRepository = repository;
        }
        #endregion

        #region Public methods
        public MessageEntity GetMessageEntity(int id)
        {
            NullRefCheck();
            return messageRepository.GetById(id).ToBllMessage();
        }

        public IEnumerable<MessageEntity> GetAllMessageEntities()
        {
            NullRefCheck();
            return messageRepository.GetAll().Select(message => message.ToBllMessage());
        }

        public void CreateMessage(MessageEntity message)
        {
            NullRefCheck();
            ArgumentNullCheck(message);
            messageRepository.Create(message.ToDalMessage());
            uow.Commit();
        }

        public void DeleteMessage(MessageEntity message)
        {
            NullRefCheck();
            ArgumentNullCheck(message);
            messageRepository.Delete(message.ToDalMessage());
            uow.Commit();
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
        }
        private void NullRefCheck()
        {
            if (this == null)
            {
                throw new NullReferenceException("message");
            }
        }
        #endregion
    }
}
