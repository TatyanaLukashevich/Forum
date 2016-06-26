using BLL.Interface.Entities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class SectionViewModel 
    {
        public int SectionId { get; set; }
        public string SectionName { get; set; }
        public IEnumerable<PostViewModel> PostsInSection { get; set; }

    }
}