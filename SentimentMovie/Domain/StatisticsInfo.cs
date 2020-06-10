using System;
using System.Collections.Generic;
using System.Linq;
using SentimentMovie.DAL;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public class StatisticsInfo : IStatisticsInfo
    {
        private IEnumerable<OpinionDTO> _opinions;
        private IEnumerable<SentimentDTO> _sentiments;
        private IEnumerable<MovieDTO> _movies;

        public StatisticsInfo(ICommonRepository<SentimentDTO> sentimentRepository,
            ICommonRepository<MovieDTO> movieRepository,
            ICommonRepository<OpinionDTO> opinionRepository)
        {
            _sentiments = sentimentRepository.GetAll();
            _movies = movieRepository.GetAll();
            _opinions = opinionRepository.GetAll();
        }

        public StatisticsViewModel GetStatistic()
        {
            var statisticsVM = new StatisticsViewModel
            {
                MovieCnt = _movies.Count(),
                RatingCnt = _opinions.Count(),
                SentimnetCnt = _sentiments.Count(),
                UserOnline = GetUserOnline()
            };
            return statisticsVM;
        }

        private int GetUserOnline()
        {
            var ran = new Random();
            return ran.Next(30000, 50000);
        }
    }
}
