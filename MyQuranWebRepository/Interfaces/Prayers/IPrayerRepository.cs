using MyQuranWeb.Domain.Models.Prayers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Interfaces.Prayers
{
    public interface IPrayerRepository
    {
        Task<IEnumerable<Intention>> GetIntentions();
        Task<IEnumerable<PrayerRead>> GetPrayerReads();
        Task<IEnumerable<PrayerZikr>> GetPrayerZikrs();
        Task<IEnumerable<PrayerZikr>> GetPrayerZikrs2();
    }
}
