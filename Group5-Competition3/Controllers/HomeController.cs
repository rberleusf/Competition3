using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Group5_Competition3.Models;

namespace Group5_Competition3.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    public async Task<IActionResult> Index()
    {
        string apiUrl = "https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey=fccc77c150454041886544b58f106d86";

        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
        string json = await response.Content.ReadAsStringAsync();

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        // var newsResponse = JsonSerializer.Deserialize<NewsResponse>(json, options);
        var articles = new List<Article>();

        using var doc = JsonDocument.Parse(json);
        var articlesArray = doc.RootElement.GetProperty("articles");

        foreach (var item in articlesArray.EnumerateArray())
        {
            articles.Add(new Article
            {
                NewsSourceName = item.GetProperty("source").GetProperty("name").GetString(),
                Title = item.GetProperty("title").GetString(),
                ArticleURL = item.GetProperty("url").GetString()
            });
        }

        return View(articles);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}