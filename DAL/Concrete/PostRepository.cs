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
    public class PostRepository : IPostRepository
    {
        private readonly DbContext context;

        public PostRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region Public methods
        public IEnumerable<DalPost> GetAll()
        {
            NullRefCheck();
            return context.Set<Post>().Select(post => new DalPost()
            {
                Id = post.PostId,
                Name = post.Name,
                Body = post.Body,
                DateOfPost = post.DateOfCreation,
                AuthorLogin = post.Author.Login,
                PostMessage = post.PostMessages.Select(m => new DalMessage()
                { AuthorLogin = m.User.Login, Body = m.Body, DateOfMessage = m.DateOfCreation, Id = m.Id, PostID = m.PostId }),
                AmountMessages = post.PostMessages.Count,
                AuthorId = post.UserID,
                SectionId = post.SectionId
            });
        }

        public DalPost GetById(int key)
        {
            NullRefCheck();
            var ormpost = context.Set<Post>().FirstOrDefault(post => post.PostId == key);
            return new DalPost()
            {
                Id = ormpost.PostId,
                Name = ormpost.Name,
                Body = ormpost.Body,
                DateOfPost = ormpost.DateOfCreation,
                AuthorLogin = ormpost.Author.Login,
                PostMessage = ormpost.PostMessages.Select(m => new DalMessage()
                { AuthorLogin = m.User.Login, Body = m.Body, DateOfMessage = m.DateOfCreation, Id = m.Id, PostID= m.PostId}),
                AmountMessages = ormpost.PostMessages.Count,
                AuthorId = ormpost.UserID,
                SectionId = ormpost.SectionId

            };
        }

        public DalPost GetByPredicate(Expression<Func<DalPost, bool>> f)
        {
            NullRefCheck();
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void Create(DalPost p)
        {
            NullRefCheck();
            ArgumentNullCheck(p);
            var post = new Post()
            {
                PostId = p.Id,
                Name = p.Name,
                Body = p.Body,
                DateOfCreation = DateTime.Now,
                UserID = p.AuthorId,
                SectionId = p.SectionId
            };
            context.Set<Post>().Add(post);
        }

        public void Delete(DalPost p)
        {
            NullRefCheck();
            ArgumentNullCheck(p);
            var post = new Post()
            {
                PostId = p.Id,
                Name = p.Name,
                Body = p.Body,
                DateOfCreation = p.DateOfPost,
                UserID = p.AuthorId,
                SectionId = p.SectionId
            };
            post = context.Set<Post>().Single(u => u.PostId == post.PostId);
            context.Set<Post>().Remove(post);
        }

        public void Update(DalPost post)
        {
            NullRefCheck();
            ArgumentNullCheck(post);
            var postDB = context.Set<Post>().FirstOrDefault(p=> p.PostId == post.Id);
            postDB.Name = post.Name;
            postDB.Body = post.Body;
            postDB.SectionId = post.SectionId;
            postDB.DateOfCreation = post.DateOfPost;
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] post)
        {
            if (post == null)
            {
                throw new ArgumentNullException("post");
            }
        }

        private void NullRefCheck()
        {
            if (this == null)
            {
                throw new NullReferenceException("post");
            }
        }
        #endregion
    }
}