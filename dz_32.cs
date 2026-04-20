using System;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
       
        string emailPattern = @"^[\w\.-]+@[\w\.-]+\.\w+$";
        Console.WriteLine(Regex.IsMatch("user@example.com", emailPattern));

        
        string textPhones = "Позвони +7 (999) 123-45-67 и +7 (111) 000-00-00";
        var phones = Regex.Matches(textPhones, @"\+7 \(\d{3}\) \d{3}-\d{2}-\d{2}");
        foreach (Match m in phones)
            Console.WriteLine(m.Value);

       
        string dates = "Сегодня 20.04.2026";
        string newDates = Regex.Replace(dates, @"(\d{2})\.(\d{2})\.(\d{4})", "$3-$2-$1");
        Console.WriteLine(newDates);

        
        string spaces = "Это   текст   с   пробелами";
        Console.WriteLine(Regex.Replace(spaces, @"\s{2,}", " "));

        
        string html = "<h1>Заголовок</h1>";
        var headers = Regex.Matches(html, @"<h1>(.*?)</h1>");
        foreach (Match m in headers)
            Console.WriteLine(m.Groups[1].Value);

       
        string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$";
        Console.WriteLine(Regex.IsMatch("Abcdef12", passwordPattern));

        
        string priceText = "Товар стоит 1500 руб., а скидка 200 руб.";
        var prices = Regex.Matches(priceText, @"\d+(?= руб\.)");
        foreach (Match m in prices)
            Console.WriteLine(m.Value);

        
        string ipPattern = @"^((25[0-5]|2[0-4]\d|1\d\d|\d\d?)\.){3}(25[0-5]|2[0-4]\d|1\d\d|\d\d?)$";
        Console.WriteLine(Regex.IsMatch("192.168.1.1", ipPattern));

        
        string code = "int myVar = 0; int TestVar = 1; string userName;";
        var vars = Regex.Matches(code, @"\b[a-z][a-zA-Z0-9]*\b");
        foreach (Match m in vars)
            Console.WriteLine(m.Value);

        
        string sentence = "Привет, мир! Как дела.";
        var parts = Regex.Split(sentence, @"[,.!]");
        foreach (var p in parts)
            if (!string.IsNullOrWhiteSpace(p))
                Console.WriteLine(p.Trim());
    }
}
