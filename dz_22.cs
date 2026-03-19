using System;
using System.Linq;

class Program
{
    static void Main()
    {
        // 1
        DateTime now = DateTime.Now;
        Console.WriteLine(now.ToString("dddd, dd MMMM yyyy, HH:mm:ss"));

        // 2
        Console.WriteLine("Введите первую дату (например: 01.01.2024 10:00):");
        DateTime d1 = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Введите вторую дату:");
        DateTime d2 = DateTime.Parse(Console.ReadLine());

        TimeSpan diff = d2 - d1;
        Console.WriteLine($"Дни: {diff.Days}, Часы: {diff.Hours}, Минуты: {diff.Minutes}");

        // 3
        Console.WriteLine("Введите год:");
        int year = int.Parse(Console.ReadLine());

        bool myLeap = (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);
        Console.WriteLine($"Мой метод: {myLeap}");
        Console.WriteLine($"Системный: {DateTime.IsLeapYear(year)}");

        // 4
        Console.WriteLine("Введите дату:");
        DateTime date = DateTime.Parse(Console.ReadLine());

        Console.WriteLine("Введите количество дней:");
        int days = int.Parse(Console.ReadLine());

        DateTime newDate = date.AddDays(days);
        Console.WriteLine($"{newDate} - {newDate.DayOfWeek}");

        // 5
        Console.WriteLine("Введите дату рождения:");
        DateTime birth = DateTime.Parse(Console.ReadLine());

        int age = DateTime.Now.Year - birth.Year;
        if (DateTime.Now < birth.AddYears(age))
            age--;

        Console.WriteLine($"Возраст: {age}");

        // 6
        DateTime today = DateTime.Today;
        DateTime first = new DateTime(today.Year, today.Month, 1);
        DateTime last = first.AddMonths(1).AddDays(-1);

        Console.WriteLine($"Первый день: {first}");
        Console.WriteLine($"Последний день: {last}");

        // 7
        string str = "31.12.2024 23:59";
        DateTime parsed;

        if (DateTime.TryParse(str, out parsed))
            Console.WriteLine(parsed);
        else
            Console.WriteLine("Ошибка формата");

        // 8
        DateTime newYear = new DateTime(DateTime.Now.Year + 1, 1, 1);
        TimeSpan left = newYear - DateTime.Now;

        Console.WriteLine($"До НГ: {left.Days} дней, {left.Hours} часов, {left.Minutes} минут");

        // 9
        Console.WriteLine("Введите год для пятниц 13:");
        int y = int.Parse(Console.ReadLine());

        for (int m = 1; m <= 12; m++)
        {
            DateTime d = new DateTime(y, m, 13);
            if (d.DayOfWeek == DayOfWeek.Friday)
                Console.WriteLine(d.ToShortDateString());
        }

        // 10
        Random rnd = new Random();
        DateTime[] arr = new DateTime[5];

        for (int i = 0; i < 5; i++)
        {
            arr[i] = new DateTime(2020 + rnd.Next(6), rnd.Next(1, 13), rnd.Next(1, 28));
        }

        Array.Sort(arr);

        Console.WriteLine("Отсортированные даты:");
        foreach (var d in arr)
            Console.WriteLine(d);
    }
}
