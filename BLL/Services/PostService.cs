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
    public class PostService : IPostService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IPostRepository postRepository;
        #endregion

        #region Constructor
        public PostService(IUnitOfWork uow, IPostRepository repository)
        {
            this.uow = uow;
            this.postRepository = repository;
        }
        #endregion

        #region Public methods
        public PostEntity GetPostEntity(int id)
        {
            NullRefCheck();
            return postRepository.GetById(id).ToBllPost();
        }
        
        public IEnumerable<PostEntity> GetAllPostEntities()
        {
            NullRefCheck();
            return postRepository.GetAll().Select(post => post.ToBllPost());
        }

        public void CreatePost(PostEntity post)
        {
            NullRefCheck();
            ArgumentNullCheck(post);
            postRepository.Create(post.ToDalPost());
            uow.Commit();
        }

        public void DeletePost(PostEntity oldPost)
        {
            NullRefCheck();
            ArgumentNullCheck(oldPost);
            postRepository.Delete(oldPost.ToDalPost());
            uow.Commit();
        }

        public void Update (PostEntity post)
        {
            NullRefCheck();
            ArgumentNullCheck(post);
            postRepository.Update(post.ToDalPost());
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
            if(this==null)
            {
                throw new NullReferenceException("post");
            }
        }
        #endregion
    }
}
