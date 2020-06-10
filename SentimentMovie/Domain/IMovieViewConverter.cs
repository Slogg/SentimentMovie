using System.Collections.Generic;
using SentimentMovie.Models;

namespace SentimentMovie.Domain
{
    public interface IMovieViewConverter
    {
        IEnumerable<MovieViewModel> GetMovieMainView();
        IEnumerable<MovieDTO> GetMovies();
        void UpdateMovie(MovieUpdateModel movieUpdateVM);
        void UpdateSentiment(MovieUpdateModel movieUpdateVM);
    }
}