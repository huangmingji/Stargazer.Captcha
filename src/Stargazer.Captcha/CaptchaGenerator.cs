using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing.Processing;

namespace Stargazer.Captcha;

public static class CaptchaGenerator  
{  
    private static readonly Random random = new Random();  
    private static readonly string[] captchaCharacters = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };  
  
    public static Image<Rgba32> GenerateCaptchaImage(string captchaText, int width, int height, Font font, Color fontColor, Color backgroundColor)  
    {  
        Image<Rgba32> image = new Image<Rgba32>(width, height, backgroundColor);  
        image.Mutate(x => x.DrawText(captchaText, font, fontColor, new PointF(width / 5, height / 3)));  
        return image;  
    }  
  
    public static string GenerateCaptchaText(int length)  
    {  
        string captchaText = "";  
        for (int i = 0; i < length; i++)  
        {  
            captchaText += captchaCharacters[random.Next(captchaCharacters.Length)];  
        }  
        return captchaText;  
    }  
      
    public static Image<Rgba32> AddAntiCrawlingNoise(this Image<Rgba32> captchaImage)  
    {  
        int width = captchaImage.Width;  
        int height = captchaImage.Height;  
      
        for (int i = 0; i < width / 10; i++) // 添加随机噪声点  
        {  
            Rgba32 noiseColor = new Rgba32(random.Next(100, 150), random.Next(100, 150), random.Next(100, 150), 255); 
            int x = random.Next(width);  
            int y = random.Next(height);
            captchaImage[x, y] = noiseColor;  
        }  
      
        for (int i = 0; i < height / 20; i++) // 添加随机噪声线  
        {  
            Rgba32 noiseColor = new Rgba32(random.Next(100, 150), random.Next(100, 150), random.Next(100, 150), 255);  
            PatternPen pen = Pens.DashDot(noiseColor, 5);
            int x1 = random.Next(width);  
            int y1 = random.Next(height);  
            int x2 = random.Next(width);  
            int y2 = random.Next(height);  
            captchaImage.Mutate(x => x.DrawLine(pen, new PointF(x1, y1), new PointF(x2, y2))); // 使用Pen对象绘制线条  
        }  
        return captchaImage;  
    }
}