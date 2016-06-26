using System.Collections.Generic;

namespace DAL.Interface.DTO
{
    public class DalSection : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<DalPost> PostsInSection { get; set; }
    }
}
