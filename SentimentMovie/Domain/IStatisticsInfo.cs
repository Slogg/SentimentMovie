using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public interface IStatisticsInfo
    {
        StatisticsViewModel GetStatistic();
    }
}