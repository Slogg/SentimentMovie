namespace SentimentMovie.Models
{
    public class MovieUpdateModel
    {
        public int MovieId { get; set; }

        public int SentimentId { get; set; }

        public string ShortDescription { get; set; }

        public string SentimentName { get; set; }
    }
}
