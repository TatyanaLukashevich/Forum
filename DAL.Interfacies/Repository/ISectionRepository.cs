using DAL.Interface.DTO;
using System.Collections.Generic;

namespace DAL.Interface.Repository
{
    public interface ISectionRepository //Add user repository methods!
    {
        DalSection GetById(int id);
        IEnumerable<DalSection> GetAllSections();
        void CreateNewSection(DalSection section);
        void Delete(DalSection e);
    }
}