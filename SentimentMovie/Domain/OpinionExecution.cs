using System;
using System.Collections.Generic;
using System.Linq;
using SentimentMovie.DAL;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public class OpinionExecution : IOpinionExecution
    {
        private IEnumerable<OpinionDTO> _opinions;

        public OpinionExecution(ICommonRepository<OpinionDTO> opinionRepository)
        {
            _opinions = opinionRepository.GetAll();
        }

        /// <summary>
        /// <see cref="IOpinionExecution.CalculateRating(IEnumerable{MovieDTO})"/>
        /// </summary>
        public IEnumerable<(int movId, decimal rating)> CalculateRating(IEnumerable<MovieDTO> movies)
        {
            var ratings = new List<(int movId, decimal rating)>();
            foreach (var item in movies)
            {
                ratings.Add((item.Id, CalculateRating(item)));
            }
            return ratings;
        }
        /// <summary>
        /// <see cref="IOpinionExecution.CalculateRating(MovieDTO)"/>
        /// </summary>
        public decimal CalculateRating(MovieDTO movie)
        {
            var currMovieOpinions = _opinions.Where(x => x.MovieId == movie.Id).ToList();
            var result = 0m;
            if (currMovieOpinions.Count == 0)
            {
                return result;
            }

            result = Math.Round((decimal)currMovieOpinions.Sum(x => x.Rating) / currMovieOpinions.Count, 1);

            return result;
        }

        /// <summary>
        /// <see cref="IOpinionExecution.DetermineBestSeason(IEnumerable{MovieDTO})"/>
        /// </summary>
        public IEnumerable<(int movId, string season)> DetermineBestSeason(IEnumerable<MovieDTO> movies)
        {
            var ratings = new List<(int movId, string season)>();
            foreach (var item in movies)
            {
                ratings.Add((item.Id, ConvertSeason(DetermineBestSeason(item))));
            }
            return ratings;
        }

        /// <summary>
        /// <see cref="IOpinionExecution.DetermineBestSeason(MovieDTO)"/>
        /// </summary>
        public int DetermineBestSeason(MovieDTO movie)
        {
            var currMovieOpinions = _opinions.Where(x => x.MovieId == movie.Id).ToList();
            var result = 0;
            if (currMovieOpinions.Count == 0)
            {
                return 0;
            }

            List<(byte season, int quantity)> seasonCnt = currMovieOpinions.GroupBy(x => x)
              .Where(g => g.Count() > 0)
              .Select(y => (y.Key.Season, y.Count()))
              .ToList();

            var seasonCntMax = seasonCnt.Max(x => x.quantity);

            if (seasonCnt.Count(x => x.quantity == seasonCntMax) == 1)
            {
                result = seasonCnt.First(x => x.quantity == seasonCntMax).season;
            }
            else
            {
                result = FindNextMonthFromBestSeasons(seasonCnt.Select(x => x.season));
            }

            return result;
        }

        /// <summary>
        /// Найти наиболее подходящий месяц на основе списка сезонов
        /// </summary>
        /// <param name="months">список месяцев</param>
        /// <returns></returns>
        private int FindNextMonthFromBestSeasons(IEnumerable<byte> months)
        {
            var currentDate = DateTime.Now;
            for (int i = 0; i < 11; i++)
            {
                var date = Int32.Parse(currentDate.AddMonths(i).ToString("MM"));
                foreach (var item in months)
                {
                    if (item == date)
                    {
                        return item;
                    }
                }
            }
            throw new NotSupportedException("Среди сезонов имеются недопустимые значения.");
        }

        private string ConvertSeason(int month)
        {
            switch (month)
            {
                case 1:
                    return "Январь";
                case 2:
                    return "Февраль";
                case 3:
                    return "Март";
                case 4:
                    return "Апрель";
                case 5:
                    return "Май";
                case 6:
                    return "Июнь";
                case 7:
                    return "Июль";
                case 8:
                    return "Август";
                case 9:
                    return "Сентябрь";
                case 10:
                    return "Октябрь";
                case 11:
                    return "Ноябрь";
                case 12:
                    return "Декабрь";
                default:
                    return Constants.UnknownBestSeason;
            }
        }
    }
}
