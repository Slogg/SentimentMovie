using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using SentimentMovie.DAL;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public class MovieViewConverter : IMovieViewConverter
    {
        private readonly ICommonRepository<MovieDTO> _movieRepository;
        private readonly ICommonRepository<SentimentDTO> _sentimentRepository;
        private readonly IMapper _mapper;
        private readonly IOpinionExecution _opinionExecution;

        public MovieViewConverter(
            ICommonRepository<SentimentDTO> sentimentRepository,
            ICommonRepository<MovieDTO> movieRepository,
            IOpinionExecution opinitonExecution,
            IMapper mapper)
        {
            _sentimentRepository = sentimentRepository;
            _movieRepository = movieRepository;
            _opinionExecution = opinitonExecution;
            _mapper = mapper;
        }

        public IEnumerable<MovieDTO> GetMovies()
        {
            return _movieRepository.GetAll();
        }

        public IEnumerable<MovieViewModel> GetMovieMainView()
        {
            var movies = _movieRepository.GetAll();

            var moviesVM = _mapper.Map<IEnumerable<MovieViewModel>>(movies);
            BindingSentiments(moviesVM);

            var ratings = _opinionExecution.CalculateRating(movies);
            var bestSeasons = _opinionExecution.DetermineBestSeason(movies);
            BindingOpinions(moviesVM, ratings, bestSeasons);
            return moviesVM;
        
        }

        public void UpdateMovie(MovieUpdateModel movieUpdateVM)
        {
            var movie = _mapper.Map<MovieDTO>(movieUpdateVM);
            _movieRepository.Update(movie);
        }

        public void UpdateSentiment(MovieUpdateModel movieUpdateVM)
        {
            var sentiment = _mapper.Map<SentimentDTO>(movieUpdateVM);
            _sentimentRepository.Update(sentiment);
        }

        private void BindingOpinions(IEnumerable<MovieViewModel> movieViewModels,
            IEnumerable<(int movId, decimal rating)> ratings,
            IEnumerable<(int movId, string season)> bestSeasons)
        {
            movieViewModels.ToList().ForEach(x =>
            {
                if (ratings.Any(r => r.movId == x.MovieId))
                {
                    x.Rating = ratings.First(r => r.movId == x.MovieId).rating;
                }

                if (bestSeasons.Any(r => r.movId == x.MovieId))
                {
                    x.BestSeason = bestSeasons.First(r => r.movId == x.MovieId).season;
                }
                else
                {
                    x.BestSeason = Constants.UnknownBestSeason;
                }
            });
        }

        private void BindingSentiments(IEnumerable<MovieViewModel> movieViewModels)
        {
            var sentiments = _sentimentRepository.GetAll();
            movieViewModels.ToList().ForEach(x =>
            {
                var sentiment = sentiments.First(s => s.Id == x.SentimentId);
                x.SentimentName = sentiment.Name;
                x.SentimentColor = sentiment.Color;
            });
        }
    }
}
