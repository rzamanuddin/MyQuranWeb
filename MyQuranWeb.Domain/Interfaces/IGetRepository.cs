using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyQuranWeb.Domain.Interfaces
{

    public interface IGetRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetByID(int id);
    }

    public interface IGetRepository<T, F>
        where T : class
        where F : class
    {
        /// <summary>
        /// Get data based on filter object
        /// </summary>
        /// <param name="objectFilter"></param>
        /// <returns></returns>
        Task<IEnumerable<T>> Get(F filter);

        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get data based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByID(int id);
    }
}
