using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using MvcPL.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcPL.Controllers
{
    public class PostController : Controller
    {
        private IPostService postService;
        private IUserService userService;
        private IMessageService messageService;

        public PostController(IPostService postService, IUserService userService, IMessageService messageService)
        {
            this.postService = postService;
            this.userService = userService;
            this.messageService = messageService;
        }

        #region Create post
        [HttpGet]
        public ActionResult CreatePost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePost(PostViewModel postModel, UserViewModel userModel)
        {
            MembershipUser currentUser = Membership.GetUser();
            userModel.Email = currentUser.UserName;
            userService.GetUserByEmail(userModel.Email);
            postModel.AuthorId = userService.GetUserByEmail(userModel.Email).UserId;
            postService.CreatePost(postModel.ToBllPost());
            return RedirectToAction("UserPost");
        }
        #endregion

        #region DeletePost
        [HttpGet]
        public ActionResult DeletePost(int id = 0)
        {
            PostEntity post = postService.GetPostEntity(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post.ToMvcPost());
        }

        //Post/Redirect/Get (PRG) — модель поведения веб-приложений, используемая
        //разработчиками для защиты от повторной отправки данных веб-форм
        //(Double Submit Problem)
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id = 0)
        {
            PostEntity post = postService.GetPostEntity(id);
            postService.DeletePost(post);
            return RedirectToAction("Cabinet", "User");
        }
        #endregion

        #region Edit post
        [HttpGet]
        public ActionResult EditPost(int id=0)
        {
            return View(postService.GetPostEntity(id).ToMvcPost());
        }

        [HttpPost]
        public ActionResult EditPost(PostViewModel postModel, int id = 0)
        {
            var post = postModel.ToBllPost();
            var oldPost = postService.GetPostEntity(id);
            post.Id = oldPost.Id;
            post.AuthorId = oldPost.AuthorId;
            post.AuthorLogin = oldPost.AuthorLogin;
            post.DateOfPost = oldPost.DateOfPost;
            post.PostMessage = oldPost.PostMessage;
            if (post.Body==null)
            {
                post.Body = oldPost.Body;
            }
            if (post.Name == null)
            {
                post.Name = oldPost.Name;
            }
            postService.Update(post);
            return View(post.ToMvcPost());
        }
        #endregion

        #region User's posts
        [ActionName("UserPost")]
        public ActionResult GetAllPosts(PostViewModel postModel, UserViewModel userModel)
        {
            MembershipUser currentUser = Membership.GetUser();
            userModel.Email = currentUser.UserName;
            userService.GetUserByEmail(userModel.Email);
            postModel.AuthorId = userService.GetUserByEmail(userModel.Email).UserId;
            return View(postService.GetAllPostEntities().Select(post => post.ToMvcPost()).Where(p => p.AuthorId == postModel.AuthorId));
        }
        #endregion

        #region PostPage
        [HttpGet]
        public ActionResult SinglePost(int id = 0)
        {
            PostEntity post = postService.GetPostEntity(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            post.PostMessage = messageService.GetAllMessageEntities().Where(m => m.PostID == post.Id);
            return View(post.ToMvcPost());
        }

        #endregion

        #region Add message

        [HttpPost]
        public ActionResult SinglePost(MessageViewModel messageModel, UserViewModel userModel, int id = 0)
        {
            if (Request.IsAjaxRequest())
            {
                MembershipUser currentUser = Membership.GetUser();
                userModel.Email = currentUser.UserName;
                messageModel.UserId = userService.GetUserByEmail(userModel.Email).UserId;
                messageModel.PostId = id;
                messageService.CreateMessage(messageModel.ToBllMessage());
                PostEntity post = postService.GetPostEntity(id);
                post.PostMessage = messageService.GetAllMessageEntities().Where(m => m.PostID == post.Id);
                return PartialView("_commentsToPost", post.ToMvcPost());
            }
            return View();
        }
        #endregion

        #region All messages to post
        [ActionName("UserMessages")]
        public ActionResult GetAllMessage(MessageViewModel messageModel, UserViewModel userModel)
        {
            MembershipUser currentUser = Membership.GetUser();
            userModel.Email = currentUser.UserName;
            userService.GetUserByEmail(userModel.Email);
            return View(messageService.GetAllMessageEntities().Select(post => post.ToMvcMessage()).Where(p => p.PostId == messageModel.PostId));
        }
        #endregion

        public ActionResult DeleteMessage(int id = 0, int postId = 0)
        {
            if (Request.IsAjaxRequest())
            {
                var message = messageService.GetMessageEntity(id);
                messageService.DeleteMessage(message);
                PostEntity post = postService.GetPostEntity(postId);
                post.PostMessage = messageService.GetAllMessageEntities().Where(m => m.PostID == post.Id);
                return PartialView("_commentsToPost", post.ToMvcPost());
            }
            return RedirectToAction("SinglePost");
        }
    }
}
