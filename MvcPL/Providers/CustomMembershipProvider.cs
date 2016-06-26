using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Collections.Specialized;
using System.Data.Entity;

namespace MvcPL.Providers
{
    //провайдер членства помогает системе идентифицировать пользователя
    public class CustomMembershipProvider : MembershipProvider
    {
        #region Private fields
        private string _ApplicationName;
        private bool _EnablePasswordReset;
        private bool _EnablePasswordRetrieval = false;
        private bool _RequiresQuestionAndAnswer = false;
        private bool _RequiresUniqueEmail = true;
        private int _MaxInvalidPasswordAttempts;
        private int _PasswordAttemptWindow;
        private int _MinRequiredPasswordLength;
        private int _MinRequiredNonalphanumericCharacters;
        private string _PasswordStrengthRegularExpression;
        private MembershipPasswordFormat _PasswordFormat = MembershipPasswordFormat.Hashed;
        #endregion
        public IUserService UserService
            => (IUserService) System.Web.Mvc.DependencyResolver.Current.GetService(typeof (IUserService));

        public IRoleService RoleService
            => (IRoleService)System.Web.Mvc.DependencyResolver.Current.GetService(typeof (IRoleService));

        public MembershipUser CreateUser(string email, string password)
        {
            MembershipUser membershipUser = GetUser(email, false);

            if (membershipUser != null)
            {
                return membershipUser;
            }

            var user = new UserViewModel()
            {
                Email = email,
                Password = Crypto.HashPassword(password),
            };

            var role = RoleService.GetAllRoles().FirstOrDefault(r => r.Name == "User");
            if (role != null)
            {
                user.RoleId = role.Id;
            }

            UserService.CreateUser(user.ToBllUser());
            membershipUser = GetUser(email, false);
            return membershipUser;
        }

        public override bool ValidateUser(string email, string password)
        {
            var user = UserService.GetUserByEmail(email);

            if (user != null && Crypto.VerifyHashedPassword(user.Password, password))
            {
                return true;
            }
            return false;
        }

        public override MembershipUser GetUser(string email, bool userIsOnline)
        {
            var user = UserService.GetUserByEmail(email);

            if (user == null) return null;

            var memberUser = new MembershipUser("CustomMembershipProvider", user.Email,
                null, null, null, null,
                false, false, DateTime.Now,
                DateTime.MinValue, DateTime.MinValue,
                DateTime.MinValue, DateTime.MinValue);

            return memberUser;
        }

        #region Stabs
        public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion,
            string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
        {
            ValidatePasswordEventArgs args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            MembershipUser u = GetUser(username, false);
            if (u == null)
            {
                UserService.CreateUser(UserService.GetUserByEmail(email));
                status = MembershipCreateStatus.Success;

                return GetUser(username, false);
            }
            else
            {
                status = MembershipCreateStatus.DuplicateUserName;
            }
            return null;
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion,
            string newPasswordAnswer)
        {
            return false;
        }

        public override string GetPassword(string username, string answer)
        {
            var user = UserService.GetUserByEmail(username);
            return user.Password;
        }

        public override bool ChangePassword(string email, string oldPassword, string newPassword)
        {
            try
            {
                UserEntity user = UserService.GetUserByEmail(email);
                if (user == null)
                    throw new Exception("Пользователя не существует!");

                if (!Crypto.VerifyHashedPassword( user.Password, oldPassword))
                    throw new InvalidOperationException("неправильный пароль");

                if (ValidateUser(user.Email, oldPassword))
                {
                    base.OnValidatingPassword(
                        new ValidatePasswordEventArgs(
                           email, newPassword, false));
                    using (var context = new DbContext("Forum"))
                    {
                        user.Password = Crypto.HashPassword(newPassword);
                        UserService.ChangePassword(email, newPassword);
                        context.SaveChanges();
                    }  
                    return true;
                }

                return false;
            }
            catch
            {
                 new RedirectResult("Error");
                return false;
            }
        }

        public override string ResetPassword(string username, string answer)
        {
            var user = UserService.GetUserByEmail(username);
            user.Password = null;
            return user.Password;
        }

        public override void UpdateUser(MembershipUser user)
        {
            var userEntity = UserService.GetUserByEmail(user.Email);
           UserService.ChangeRole(userEntity.Email, userEntity .RoleId);
        }

        public override bool UnlockUser(string userName)
        {
            return false; ;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
                return GetUser((string)providerUserKey, false);
        }

        public override string GetUserNameByEmail(string email)
        {
            return UserService.GetUserByEmail(email).Login;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            var user = UserService.GetUserByEmail(username);
            UserService.DeleteUser(user);
            return true;
        }

        public override bool EnablePasswordRetrieval
        {
            get
            {
                return _EnablePasswordRetrieval;
            }
        }

        public override bool EnablePasswordReset
        {
            get
            {
                return _EnablePasswordReset;
            }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get
            {
                return _RequiresQuestionAndAnswer;
            }
        }

        public override string ApplicationName
        {
            get
            {
                return _ApplicationName;
            }
            set
            {
                _ApplicationName = value;
            }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get
            {
                return _MaxInvalidPasswordAttempts;
            }
        }

        public override int PasswordAttemptWindow
        {
            get
            {
                return _PasswordAttemptWindow;
            }
        }

        public override bool RequiresUniqueEmail
        {
            get
            {
                return _RequiresUniqueEmail;
            }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get
            {
                return _PasswordFormat;
            }
        }

        public override int MinRequiredPasswordLength
        {
            get
            {
                return _MinRequiredPasswordLength;
            }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get
            {
                return _MinRequiredNonalphanumericCharacters;
            }
        }

        public override string PasswordStrengthRegularExpression
        {
            get
            {
                return _PasswordStrengthRegularExpression;
            }
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotImplementedException();
        }
        #endregion     
    }
}