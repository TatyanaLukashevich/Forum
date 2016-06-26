using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;
using System.Web.Helpers;
using System.Web;

namespace DAL.Concrete
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext context;

        public UserRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region Public methods
        public IEnumerable<DalUser> GetAll()
        {
            NullRefCheck();
            return context.Set<User>().Select(user => new DalUser()
            {
                Id = user.UserId,
                Login = user.Login,
                RoleId = user.RoleId,
                Email = user.Email,
                Password = user.Password,
                UserPost = user.Posts.Select(p => new DalPost()
                { Id = p.PostId, Name = p.Name, AuthorLogin = p.Author.Login, DateOfPost = p.DateOfCreation, SectionId = p.SectionId }),
                UserMessage = user.Messages.Select(p => new DalMessage()
                {
                    Id = p.Id,
                    Body = p.Body,
                    AuthorLogin = p.User.Email,
                    DateOfMessage = p.DateOfCreation,
                    AuthorId = p.UserId,
                    PostID = p.PostId
                })
            });
        }

        public DalUser GetById(int key)
        {
            NullRefCheck();
            var ormuser = context.Set<User>().FirstOrDefault(user => user.UserId == key);
            return new DalUser()
            {
                Id = ormuser.UserId,
                Login = ormuser.Login,
                RoleId = ormuser.RoleId,
                Email = ormuser.Email,
                Password = ormuser.Password,
                UserPost = ormuser.Posts.Select(p => new DalPost()
                { Id = p.PostId, Name = p.Name, AuthorLogin = p.Author.Login, DateOfPost = p.DateOfCreation, SectionId = p.SectionId }),
                 UserMessage = ormuser.Messages.Select(p => new DalMessage()
                 { Id = p.Id, Body = p.Body, AuthorLogin = p.User.Email, DateOfMessage = p.DateOfCreation,
                     AuthorId = p.UserId, PostID = p.PostId })
            };
        }

        public DalUser GetUserByEmail(string email)
        {
            NullRefCheck();
            var ormuser = context.Set<User>().FirstOrDefault(user => user.Email == email);
            return new DalUser()
            {
                Id = ormuser.UserId,
                Login = ormuser.Login,
                RoleId = ormuser.RoleId,
                Email = ormuser.Email,
                Password = ormuser.Password,
                UserPost = ormuser.Posts.Select(p => new DalPost()
                { Id = p.PostId, Name = p.Name, AuthorLogin = p.Author.Login, DateOfPost = p.DateOfCreation }),
                UserMessage = ormuser.Messages.Select(p=>new DalMessage()
                { Id = p.Id, Body = p.Body, AuthorLogin = p.User.Email,DateOfMessage = p.DateOfCreation,
                    AuthorId = p.UserId,PostID = p.PostId
                })
            };
        }

        public DalUser GetByPredicate(Expression<Func<DalUser, bool>> f)
        {
            NullRefCheck();
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalUser e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var password = Crypto.HashPassword(e.Password);
            var user = new User()
            {
                UserId = e.Id,
                Login = e.Login,
                RoleId = e.RoleId,
                Email = e.Email,
                Password = password
                //CreationDate = e.CreationDate,
            };
            context.Set<User>().Add(user);
        }

        public void Delete(DalUser e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var user = new User()
            {
                UserId = e.Id,
                Login = e.Login,
                RoleId = e.RoleId,
                Email = e.Email,
                Password = e.Password,
            };
            user = context.Set<User>().Single(u => u.UserId == user.UserId);
            context.Set<User>().Remove(user);
        }

        public void Update(DalUser entity)
        {
            NullRefCheck();
            throw new NotImplementedException();
        }

        public void ChangePassword(string email, string newPassword)
        {
            NullRefCheck();
            ArgumentNullCheck(email, newPassword);
            var password = Crypto.HashPassword(newPassword);
            var userDB = context.Set<User>().FirstOrDefault(u => u.Email == email);
            userDB.Password = password;
        }

        public void ChangeRole(string email, int roleId)
        {
            NullRefCheck();
            ArgumentNullCheck(email);
            var userDB = context.Set<User>().FirstOrDefault(u => u.Email == email);
            userDB.RoleId = roleId;
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] u)
        {
            if (u == null)
            {
                throw new ArgumentNullException("user");
            }
        }

        private void NullRefCheck()
        {
            if (this == null)
            {
                throw new NullReferenceException("user");
            }
        }
        #endregion
    }
}