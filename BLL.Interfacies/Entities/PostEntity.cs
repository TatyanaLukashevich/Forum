﻿using DAL.Interface.DTO;
using System;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
   public class PostEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPost { get; set; }
        public string Body { get; set; }
        public IEnumerable<MessageEntity> PostMessage { get; set; }
        public int AmountMessages { get; set; }
        public string AuthorLogin{ get; set; }
        public int AuthorId { get; set; }
        public int SectionId { get; set; }
    }
}
