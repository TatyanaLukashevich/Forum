using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
   public class MessageRepository :IMessageRepository
    {
        private readonly DbContext context;

        public MessageRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region Public methods
        public IEnumerable<DalMessage> GetAll()
        {
            NullRefCheck();
            return context.Set<Message>().Select(message => new DalMessage()
            {
                Id = message.Id,
                Body = message.Body,
                DateOfMessage = message.DateOfCreation,
                AuthorLogin = message.User.Email,
                AuthorId = message.UserId,
                PostID = message.PostId,
                ReplyId = message.ReplyId
            });
        }

        public DalMessage GetById(int key)
        {
            NullRefCheck();
            var ormMessage = context.Set<Message>().FirstOrDefault(message => message.Id == key);
            return new DalMessage()
            {
                Id = ormMessage.Id,
                Body = ormMessage.Body,
                DateOfMessage = ormMessage.DateOfCreation,
                AuthorId = ormMessage.UserId,
                PostID = ormMessage.PostId,
                AuthorLogin = ormMessage.User.Email,
                ReplyId = ormMessage.ReplyId

            };
        }

        public DalMessage GetByPredicate(Expression<Func<DalMessage, bool>> f)
        {
            NullRefCheck();
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalMessage m)
        {
            NullRefCheck();
            ArgumentNullCheck(m);
            var message = new Message()
            {
                Id = m.Id,
                Body = m.Body,
                DateOfCreation = DateTime.Now,
                UserId = m.AuthorId,
                PostId = m.PostID,
                ReplyId = m.ReplyId
            };
            context.Set<Message>().Add(message);
        }

        public void Delete(DalMessage m)
        {
            NullRefCheck(); 
            ArgumentNullCheck(m);
            var message = new Message()
            {
                Id = m.Id,
                Body = m.Body,
                DateOfCreation = m.DateOfMessage,
                UserId = m.AuthorId,
                PostId = m.PostID,
                ReplyId = m.ReplyId
            };
            message = context.Set<Message>().Single(u => u.Id == message.Id);
            context.Set<Message>().Remove(message);
        }

        public void Update(DalMessage entity)
        {
            NullRefCheck();
            throw new NotImplementedException();
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
            if(this==null)
            {
                throw new NullReferenceException("message");
            }
        }
        #endregion
    }
}
