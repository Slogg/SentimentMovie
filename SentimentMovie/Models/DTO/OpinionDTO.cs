namespace SentimentMovie.Models
{
    public class OpinionDTO
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public byte Rating { get; set; }

        public byte Season { get; set; }

        public string UserName { get; set; }
    }
}
