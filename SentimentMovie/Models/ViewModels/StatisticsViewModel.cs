using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentMovie.Models
{
    public class StatisticsViewModel
    {
        public int MovieCnt { get; set; }

        public int RatingCnt { get; set; }

        public int SentimnetCnt { get; set; }

        public int UserOnline { get; set; }
    }
}
