public class Article
{
    public Article(string newsSourceName, string title, string articleURL)
    {
        NewsSourceName = newsSourceName;
        Title = title;
        ArticleURL = articleURL;    
    }
    
    public string NewsSourceName { get; set; }
    public string Title { get; set; }
    public string ArticleURL { get; set; }
}