using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SixLabors.Fonts;
using SixLabors.ImageSharp;
using Stargazer.Captcha;
using Stargazer.Web.Models;

namespace Stargazer.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Captcha()
    {
        Font font = SystemFonts.CreateFont("Arial", 40);
        string captchaText = CaptchaGenerator.GenerateCaptchaText(5); 
        var captchaImage = CaptchaGenerator.GenerateCaptchaImage(captchaText, 200, 100, font, Color.Black, Color.White)
            .AddAntiCrawlingNoise();
        byte[] imageBytes;
        using (var ms = new MemoryStream())
        {
            captchaImage.SaveAsPng(ms);
            imageBytes = ms.ToArray();
        }
        // string base64String = Convert.ToBase64String(imageBytes); 
        return File(imageBytes, "image/png", $"captcha_{Guid.NewGuid()}.png");
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
