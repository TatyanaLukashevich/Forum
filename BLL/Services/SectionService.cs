using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using BLL.Interface.Entities;
using BLL.Interface.Services;
using BLL.Mappers;
using DAL.Interface.Repository;
using DAL.Concrete;

namespace BLL.Services
{
    public class SectionService : ISectionService
    {
        #region Fields
        private readonly IUnitOfWork uow;
        private readonly ISectionRepository sectionRepository;
        #endregion

        #region Constructor
        public SectionService(IUnitOfWork uow, ISectionRepository repository)
        {
            this.uow = uow;
            this.sectionRepository = repository;
        }
        #endregion

        #region Public methods
        public SectionEntity GetSectionEntity(int id)
        {
            return sectionRepository.GetById(id).ToBllSection();
        }

        public IEnumerable<SectionEntity> GetAllSectionEntities()
        {
            return sectionRepository.GetAllSections().Select(section => section.ToBllSection());
        }

        public void CreateSection(SectionEntity section)
        {
            ArgumentNullCheck(section);
            sectionRepository.CreateNewSection(section.ToDalSection());
            uow.Commit();
        }


        public void DeleteSection(SectionEntity section)
        {
            ArgumentNullCheck(section);
            sectionRepository.Delete(section.ToDalSection());
            uow.Commit();
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
                throw new NullReferenceException("section");
            }
        }
        #endregion

    }
}
