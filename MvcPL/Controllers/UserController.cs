using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using MvcPL.Providers;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    public class UserController : Controller
    {
        private IPostService postService;
        private IUserService userService;
        private IMessageService messageService;

        public UserController(IPostService postService, IUserService userService, IMessageService messageService)
        {
            this.postService = postService;
            this.userService = userService;
            this.messageService = messageService;
        }


        public ActionResult Cabinet()
        {
            return View();
        }      

        [HttpGet]
        public ActionResult EditInfo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult EditInfo(EditInfoViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                MembershipUser currentUser = Membership.GetUser();
                viewModel.Email = currentUser.UserName;
                userService.GetUserByEmail(viewModel.Email);

                var membershipUser = ((CustomMembershipProvider)Membership.Provider)
                        .ChangePassword(viewModel.Email, viewModel.OldPassword, viewModel.NewPassword);
                return PartialView("_PasswordChanged");
            }
           
            return View();
        }

      [HttpGet]
      public ActionResult UserPage(int id)
        {
            return View(userService.GetUserEntity(id).ToMvcUser());
        }
    }
}
