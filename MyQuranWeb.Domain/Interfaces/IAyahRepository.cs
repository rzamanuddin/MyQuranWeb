using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{
    //public interface ICompanyRegistrationRepository : IGenericRepository<Models.Altus.Finance.CompanyRegistrations.CompanyRegistration>,
    //    IGetRepository<Models.Altus.Finance.CompanyRegistrations.CompanyRegistration, CompanyRegistrationFilter>
    //{

    //    //Task<IEnumerable<Models.Altus.Finance.CompanyRegistrations.CompanyRegistrationDTO>> GetByName(string name);
    //}
    public interface IAyahRepository : IGetRepository<Ayah, AyahFilter>
    {
        Task<IEnumerable<Ayah>> GetBySurahID(int surahID);
        Task<Ayah> GetBySurahAndAyahID(int surahID, int ayahID);
        Task<Ayah> GetByIDForPopUp(int id);
    }
}
