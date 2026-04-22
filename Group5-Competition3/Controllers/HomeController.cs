using System.Diagnostics;
using System.Text.Json;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc;
using Group5_Competition3.Models;

namespace Group5_Competition3.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;

    public HomeController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
        _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Group5-Competition3/1.0");
    }

    public async Task<IActionResult> Index()
    {
        string apiUrl = "https://newsapi.org/v2/top-headlines?country=us&category=business&apiKey=fd4dc9ca9b6145a9b6102df851dcf93e";

        HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

        if (!response.IsSuccessStatusCode)
        {
            ViewData["ErrorMessage"] = "Unable to load articles at this time.";
            return View(new List<Article>());
        }

        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var root = await response.Content.ReadFromJsonAsync<JsonElement>(options);

        var newsArticles = root.TryGetProperty("articles", out var arr)
            ? JsonSerializer.Deserialize<List<NewsApiArticle>>(arr.GetRawText(), options) ?? new()
            : new();

        var articles = newsArticles
            .Select(a => new Article(
                a.Source?.Name ?? string.Empty,
                a.Title ?? string.Empty,
                a.Url ?? string.Empty,
                a.Description ?? string.Empty,
                a.UrlToImage ?? string.Empty))
            .Take(10)
            .ToList();

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