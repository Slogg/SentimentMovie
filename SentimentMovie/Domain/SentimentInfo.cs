using System.Collections.Generic;
using AutoMapper;
using SentimentMovie.DAL;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public class SentimentInfo : ISentimentInfo
    {
        private readonly IEnumerable<SentimentDTO> _sentiments;
        private readonly IMapper _mapper;

        public SentimentInfo(ICommonRepository<SentimentDTO> sentimentRepository, IMapper mapper)
        {
            _sentiments = sentimentRepository.GetAll();
            _mapper = mapper;
        }

        public IEnumerable<SentimentViewModel> GetInfo()
        {
            var sentiments = _mapper.Map<IEnumerable<SentimentViewModel>>(_sentiments);
            return sentiments;
        }
    }
}
