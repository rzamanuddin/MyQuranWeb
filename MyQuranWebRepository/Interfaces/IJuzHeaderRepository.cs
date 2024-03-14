using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWebRepository.Interfaces
{
    public interface IJuzHeaderRepository : IGetRepository<JuzHeader, JuzHeaderFilter>
    {
    }
}
