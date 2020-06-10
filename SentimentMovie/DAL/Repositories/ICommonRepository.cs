using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentMovie.DAL
{
    /// <summary>
    /// Интерфейс репозитория для общих комманд
    /// </summary>
    public interface ICommonRepository<T>
        where T : class
    {
        /// <summary>
        /// Получить все данные
        /// </summary>
        /// <typeparam name="T">тип</typeparam>
        /// <returns>коллекция с данными </returns>
        IEnumerable<T> GetAll();

        void Update(T data);
    }

}
