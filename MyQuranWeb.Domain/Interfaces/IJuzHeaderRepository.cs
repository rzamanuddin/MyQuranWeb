using MyQuranWeb.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{
    public interface IJuzHeaderRepository : IGetRepository<JuzHeader, JuzHeaderFilter>
    {
    }
}
