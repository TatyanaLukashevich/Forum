using BLL.Interface.Entities;
using BLL.Interface.Services;
using MvcPL.Infrastructure.Mappers;
using PagedList;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISectionService sectionService;
        private readonly IPostService postService;
        private readonly IUserService userService;


        public HomeController(ISectionService sectionService, IPostService postService, IUserService userService)
        {
            this.sectionService = sectionService;
            this.postService = postService;
            this.userService = userService;
        }
        public ActionResult Index()
        {
            return View(sectionService.GetAllSectionEntities().Select(section => section.ToMvcSection()));
        }

        public ActionResult Rules()
        {
            return View();
        }

        public ActionResult PostInSection(int? page, int id = 0)
        {
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            var a = postService.GetAllPostEntities().Where(p => p.SectionId == id).Select(p => p.ToMvcPost());
            var posts = a.ToList();
            return View(posts.ToPagedList(pageNumber, pageSize));
        }

       [HttpPost]
        public JsonResult JsonSearch(string name)
        {
            var jsondata = postService.GetAllPostEntities().Where(a => a.Name.Contains(name)).ToList<PostEntity>();
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

      

    }
}
