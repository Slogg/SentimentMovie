namespace SentimentMovie.Models
{
    public class MovieDTO
    {
        public int Id { get; set; }

        public int SentimentId { get; set; }

        public string Name {get;set;}

        public string ShortDescription { get; set; }

        public short ReleaseYear { get; set; }

        public string PosterPath { get; set; }
    }
}
