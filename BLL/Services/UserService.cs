using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly IUserRepository userRepository;
        private readonly IPostRepository postRepository;
        #endregion

        #region Constructor
        public UserService(IUnitOfWork uow, IUserRepository repository, IPostRepository postRepository)
        {
            this.uow = uow;
            this.userRepository = repository;
            this.postRepository = postRepository;
        }
        #endregion

        #region Public methods
        public UserEntity GetUserEntity(int id)
        {
            NullRefCheck();
            return userRepository.GetById(id).ToBllUser();
        }

        public IEnumerable<UserEntity> GetAllUserEntities()
        {
            NullRefCheck();
            return userRepository.GetAll().Select(user => user.ToBllUser());
        }

        public void CreateUser(UserEntity user)
        {
            NullRefCheck();
            ArgumentNullCheck(user);
            userRepository.Create(user.ToDalUser());
            uow.Commit();
        }

        public UserEntity GetUserByEmail(string email)
        {
            NullRefCheck();
            ArgumentNullCheck(email);
            return userRepository.GetUserByEmail(email).ToBllUser();
        } 

        public void DeleteUser(UserEntity user)
        {
            NullRefCheck();
            ArgumentNullCheck(user);
            userRepository.Delete(user.ToDalUser());
            uow.Commit();
        }

        public void ChangePassword(string email, string newPassword)
        {
            NullRefCheck();
            ArgumentNullCheck(email, newPassword);
            userRepository.ChangePassword(email, newPassword);
            uow.Commit();
        }

        public void ChangeRole(string email, int roleId)
        {
            NullRefCheck();
            ArgumentNullCheck(email);
            userRepository.ChangeRole(email, roleId);
            uow.Commit();
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] user)
        {
            if (user == null)
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
