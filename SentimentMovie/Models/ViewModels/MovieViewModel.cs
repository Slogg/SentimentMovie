namespace SentimentMovie.Models
{
    public class MovieViewModel
    {
        public int MovieId { get; set; }

        public int SentimentId { get; set; }

        public string SentimentName { get; set; }

        public string SentimentColor { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string ReleaseYear { get; set; }

        public byte RatingIMDB { get; set; }

        public decimal Rating { get; set; }

        public string PosterPath { get; set; }

        public string BestSeason { get; set; }
    }
}
