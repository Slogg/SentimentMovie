using System.Collections.Generic;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public interface IOpinionExecution
    {
        /// <summary>
        /// Посчитать рейтинг для каждого фильма
        /// </summary>
        /// <param name="movies">список с фильмами</param>
        /// <returns>movId - Id фильма, season - лучший сезон</returns>
        IEnumerable<(int movId, decimal rating)> CalculateRating(IEnumerable<MovieDTO> movies);
        /// <summary>
        /// Посчитать рейтинг для фильма
        /// </summary>
        /// <param name="movie">фильм, которые необходимо проверить</param>
        /// <returns>итоговый рейтинг</returns>
        decimal CalculateRating(MovieDTO movie);
        /// <summary>
        /// Определить луший сезон для каждого фильма
        /// </summary>
        /// <param name="movies">список с фильмами</param>
        /// <returns>movId - Id фильма, season - лучший сезон</returns>
        IEnumerable<(int movId, string season)> DetermineBestSeason(IEnumerable<MovieDTO> movies);
        /// <summary>
        /// Определить лучший сезон для фильма
        /// </summary>
        /// <param name="movie">фильм, которые необходимо проверить</param>
        /// <returns>номер месяца</returns>
        int DetermineBestSeason(MovieDTO movie);
    }
}