using MyQuranWeb.Domain.Models.Hadiths;
using MyQuranWeb.Domain.Models.Prays;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Interfaces.Prays
{
    public interface IPrayRepository
    {
        Task<IEnumerable<PrayData>> Get();
        Task<PrayData> Get(int prayDataId);
    }
}
