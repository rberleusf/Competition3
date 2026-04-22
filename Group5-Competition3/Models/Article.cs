public class Article
{
    public Article(string newsSourceName, string title, string articleURL, string description, string imageUrl)
    {
        NewsSourceName = newsSourceName;
        Title = title;
        ArticleURL = articleURL;
        Description = description;
        ImageUrl = imageUrl;
    }
    
    public string NewsSourceName { get; set; }
    public string Title { get; set; }
    public string ArticleURL { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}