using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interface.DTO;
using DAL.Interface.Repository;
using ORM;

namespace DAL.Concrete
{
    public class SectionRepository : ISectionRepository
    {
        private readonly DbContext context;

        public SectionRepository(DbContext uow)
        {
            this.context = uow;
        }

        #region Public methods
        public IEnumerable<DalSection> GetAllSections()
        {
            NullRefCheck();
            return context.Set<Section>().Select(section => new DalSection()
            {
                Id = section.Id,
                Name = section.Name,
                PostsInSection = section.Posts.Select(p => new DalPost()
                { Id = p.PostId, Name = p.Name, AuthorLogin = p.Author.Login, DateOfPost = p.DateOfCreation, SectionId = p.SectionId, AuthorId=p.Author.UserId })

            });
        }

        public DalSection GetById(int key)
        {
            NullRefCheck();
            var ormsection = context.Set<Section>().FirstOrDefault(section => section.Id == key);
            return new DalSection()
            {
                Id = ormsection.Id,
                Name = ormsection.Name,
                PostsInSection = ormsection.Posts.Select(p => new DalPost()
                { Id = p.PostId, Name = p.Name, AuthorLogin = p.Author.Login, DateOfPost = p.DateOfCreation, SectionId = p.SectionId, AuthorId = p.Author.UserId })
            };
        }

        public DalPost GetByPredicate(Expression<Func<DalPost, bool>> f)
        {
            NullRefCheck();
            //Expression<Func<DalUser, bool>> -> Expression<Func<User, bool>> (!)
            throw new NotImplementedException();
        }

        public void CreateNewSection(DalSection e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var section = new Section()
            {
                Id = e.Id,
                Name = e.Name,
            };
            context.Set<Section>().Add(section);
        }

        public void Delete(DalSection e)
        {
            NullRefCheck();
            ArgumentNullCheck(e);
            var section = new Section()
            {
                Id = e.Id,
                Name = e.Name,
            };
            section = context.Set<Section>().Single(u => u.Id == section.Id);
            context.Set<Section>().Remove(section);
        }

        public void Update(DalSection entity)
        {
            NullRefCheck();
            throw new NotImplementedException();
        }
        #endregion

        #region Private methods
        private void ArgumentNullCheck(params object[] section)
        {
            if (section == null)
            {
                throw new ArgumentNullException("section");
            }
        }

        private void NullRefCheck()
        {
            if (this == null)
            {
                throw new NullReferenceException("role");
            }
        }
        #endregion
    }
}