using DAL.Interface.DTO;
using System.Collections.Generic;

namespace BLL.Interface.Entities
{
    public class SectionEntity
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public IEnumerable<PostEntity> PostsInSection { get; set; }
    }
}
