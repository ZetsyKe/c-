using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите первое число: ");
        double a = double.Parse(Console.ReadLine());

        Console.Write("Введите операцию (+, -, *, /): ");
        string op = Console.ReadLine();

        Console.Write("Введите второе число: ");
        double b = double.Parse(Console.ReadLine());

        double result;

        switch (op)
        {
            case "+":
                result = a + b;
                break;
            case "-":
                result = a - b;
                break;
            case "*":
                result = a * b;
                break;
            case "/":
                if (b == 0)
                {
                    Console.WriteLine("Ошибка: деление на ноль.");
                    return;
                }
                result = a / b;
                break;
            default:
                Console.WriteLine("Ошибка: неизвестная операция.");
                return;
        }

        Console.WriteLine($"Результат: {result}");
    }
}
