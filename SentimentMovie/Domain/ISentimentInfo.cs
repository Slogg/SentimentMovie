using System.Collections.Generic;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public interface ISentimentInfo
    {
        IEnumerable<SentimentViewModel> GetInfo();
    }
}