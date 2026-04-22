namespace Group5_Competition3.Models;

public class NewsApiArticle
{
    public NewsSource? Source { get; set; }
    public string? Author { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public string? UrlToImage { get; set; }
    public DateTime? PublishedAt { get; set; }
    public string? Content { get; set; }

    public class NewsSource
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
    }
}
