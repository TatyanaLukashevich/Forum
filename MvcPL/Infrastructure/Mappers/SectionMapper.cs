using BLL.Interface.Entities;
using MvcPL.Models;
using System.Linq;

namespace MvcPL.Infrastructure.Mappers
{
    public static class SectionMapper
    {
        public static SectionViewModel ToMvcSection(this SectionEntity sectionEntity)
        {
            return new SectionViewModel()
            {
                SectionId = sectionEntity.Id,
                SectionName = sectionEntity.SectionName,
                PostsInSection = sectionEntity.PostsInSection.Select(p => new PostViewModel()
                { PostId = p.Id, PostName = p.Name, AuthorLogin = p.AuthorLogin, DateOfPost = p.DateOfPost, SectionId = p.SectionId, AuthorId = p.AuthorId })                            
            };
        }

        public static SectionEntity ToBllSection(this SectionViewModel sectionViewModel)
        {
            return new SectionEntity()
            {
                Id = sectionViewModel.SectionId,
                SectionName = sectionViewModel.SectionName,
               
            };
        }
    }
}