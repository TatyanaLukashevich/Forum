using BLL.Interface.Entities;
using DAL.Interface.DTO;
using System.Linq;

namespace BLL.Mappers
{
    public static class BllSectionEntityMapper
    {
        public static DalSection ToDalSection(this SectionEntity sectionEntity)
        {
            return new DalSection()
            {
                Id = sectionEntity.Id,
                Name = sectionEntity.SectionName,         
            };
        }

        public static SectionEntity ToBllSection(this DalSection dalSection)
        {
            return new SectionEntity()
            {
                Id = dalSection.Id,
                SectionName = dalSection.Name,
                PostsInSection = dalSection.PostsInSection.Select(p => new PostEntity()
                { Id = p.Id, Name = p.Name, AuthorLogin = p.AuthorLogin, DateOfPost = p.DateOfPost, SectionId = p.SectionId, AuthorId= p.AuthorId })
            };
        }
    }
}
