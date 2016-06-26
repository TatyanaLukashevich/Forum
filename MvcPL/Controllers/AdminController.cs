using System.Linq;
using System.Web.Mvc;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;

namespace MvcPL.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService service;
        
        public AdminController(IUserService service)
        {
            this.service = service;
        }

        #region Create user
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel userViewModel)
        {
            service.CreateUser(userViewModel.ToBllUser());
            return RedirectToAction("UserList");
        }
        #endregion

        #region Delete user
        //GET-запрос к методу Delete несет потенциальную уязвимость!
        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            UserEntity user = service.GetUserEntity(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user.ToMvcUser());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(UserEntity user, int id=0)
        {
            user.UserId = id;
            service.DeleteUser(user);
            return RedirectToAction("UserList");
        }
        #endregion

        #region Addition information
        [ActionName("UserList")]
        public ActionResult GetAllUsers()
        {
            return View(service.GetAllUserEntities().Select(user => user.ToMvcUser()));
        }
        public ActionResult Details(int id = 0)
        {
            return View(service.GetUserEntity(id).ToMvcUser());
        }
        #endregion

        #region Edit user role
        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            UserEntity user = service.GetUserEntity(id);
            return View(user.ToMvcUser());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserViewModel userModel,int id = 0)
        {
            var user = userModel.ToBllUser();
            int newRoleId = user.RoleId;
            user = service.GetUserEntity(id);
            service.ChangeRole(user.Email, newRoleId);
            return View(user.ToMvcUser());
        }
        #endregion

    }
}