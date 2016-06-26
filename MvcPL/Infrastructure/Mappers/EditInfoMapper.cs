//using BLL.Interface.Entities;
//using MvcPL.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace MvcPL.Infrastructure.Mappers
//{
//    public static class EditInfoMapper
//    {
//        public static EditInfoViewModel ToMvcUser(this UserEntity userEntity)
//        {
//            return new EditInfoViewModel()
//            {
//                UserId = userEntity.UserId,
//                Login = userEntity.Login,
//                Role = (Role)userEntity.RoleId,
//                Email = userEntity.Email,
//                Password = userEntity.Password,
//                //RoleId = userEntity.RoleId,
//                //CreationDate = userEntity.CreationDate

//            };
//        }

//        public static UserEntity ToBllUser(this Models.UserViewModel userViewModel)
//        {
//            return new UserEntity()
//            {
//                UserId = userViewModel.UserId,
//                Login = userViewModel.Login,
//                RoleId = (int)userViewModel.Role,
//                Email = userViewModel.Email,
//                Password = userViewModel.Password,
//                //CreationDate = userViewModel.CreationDate
//            };
//        }

//    }
//}