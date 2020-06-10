using AutoMapper;
using SentimentMovie.Models;

namespace SentimentMovie
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MovieDTO, MovieViewModel>()
                .ForMember(b => b.MovieId, op => op.MapFrom(m => m.Id));

            CreateMap<MovieUpdateModel, MovieDTO>()
                .ForMember(b => b.Id, op => op.MapFrom(m => m.MovieId));

            CreateMap<MovieUpdateModel, SentimentDTO>()
                .ForMember(b => b.Id, op => op.MapFrom(m => m.SentimentId))
                .ForMember(b => b.Name, op => op.MapFrom(m => m.SentimentName));

            CreateMap<SentimentDTO, SentimentViewModel>();
        }
    }
}
