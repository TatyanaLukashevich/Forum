using System.Collections.Generic;
using BLL.Interface.Entities;
using DAL.Interface.DTO;

namespace BLL.Interface.Services
{
    public interface ISectionService
    {
        SectionEntity GetSectionEntity(int id);
        IEnumerable<SectionEntity> GetAllSectionEntities();
        void CreateSection(SectionEntity section);
        void DeleteSection(SectionEntity section);
    }
}
