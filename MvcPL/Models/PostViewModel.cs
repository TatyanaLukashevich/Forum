using BLL.Interface.Entities;
using BLL.Interface.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public enum Section
    {
        AspNet = 1,
        HTML,
        Java,
        PHP,
        CSS,
        Python,
        Другое
    }
    public class PostViewModel
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }
        public DateTime DateOfPost { get; set; }
        public string AuthorLogin { get; set; }
        public IEnumerable<MessageViewModel> MessagesToPost { get; set; }
        public int AmountMessages { get; set; }

        public int AuthorId { get; set; }
        public int SectionId { get; set; }
        public Section Section { get; set; }

    }
}