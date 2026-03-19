using System;

class Program
{
    // 1. Делегат для математики
    delegate double MathOperation(double a, double b);

    static double Add(double a, double b)
    {
        return a + b;
    }

    static double Multiply(double a, double b)
    {
        return a * b;
    }

    // 2. Делегат для строк
    delegate string StringModifier(string s);

    static string ToUpperCase(string s)
    {
        return s.ToUpper();
    }

    static string ToReverse(string s)
    {
        char[] arr = s.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    // 3. Делегат для фильтра
    delegate bool IsPositive(int x);

    static bool Check(int x)
    {
        return x > 0;
    }

    static void Filter(int[] numbers, IsPositive checker)
    {
        foreach (int n in numbers)
        {
            if (checker(n))
                Console.Write(n + " ");
        }
        Console.WriteLine();
    }

    // 4. Делегат уведомления
    delegate void Notify(string message);

    class Account
    {
        public double Balance;
        public Notify OnChange;

        public void ChangeBalance(double amount)
        {
            Balance += amount;
            if (OnChange != null)
                OnChange("Баланс изменен: " + Balance);
        }
    }

    static void EmailLogger(string msg)
    {
        Console.WriteLine("Email: " + msg);
    }

    static void SmsLogger(string msg)
    {
        Console.WriteLine("SMS: " + msg);
    }

    // 5. Multicast делегат
    delegate void DisplayInfo();

    static void Info1()
    {
        Console.WriteLine("Сообщение 1");
    }

    static void Info2()
    {
        Console.WriteLine("Сообщение 2");
    }

    static void Info3()
    {
        Console.WriteLine("Сообщение 3");
    }

    static void Main()
    {
        // 1
        MathOperation op;
        op = Add;
        Console.WriteLine(op(2, 3));

        op = Multiply;
        Console.WriteLine(op(2, 3));

        // 2
        StringModifier mod;
        mod = ToUpperCase;
        Console.WriteLine(mod("hello"));

        mod = ToReverse;
        Console.WriteLine(mod("hello"));

        // 3
        int[] nums = { -2, -1, 0, 1, 2, 3 };
        Filter(nums, Check);

        // 4
        Account acc = new Account();
        acc.OnChange += EmailLogger;
        acc.OnChange += SmsLogger;

        acc.ChangeBalance(100);

        // 5
        DisplayInfo d = Info1;
        d += Info2;
        d += Info3;

        d();
    }
}
