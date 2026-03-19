using System;
using System.Collections.Generic;


class Transaction
{
    public double Amount;
    public string Category;

    public Transaction(double amount, string category)
    {
        Amount = amount;
        Category = category;
    }
}

delegate bool TransactionFilter(Transaction t);


class Money
{
    public double Amount;
    public string Currency;

    public Money(double amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    static double Rate(string from, string to)
    {
        if (from == to) return 1;
        if (from == "USD" && to == "EUR") return 0.9;
        if (from == "EUR" && to == "USD") return 1.1;
        return 1;
    }

    public static Money operator +(Money a, Money b)
    {
        double converted = b.Amount * Rate(b.Currency, a.Currency);
        return new Money(a.Amount + converted, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        double converted = b.Amount * Rate(b.Currency, a.Currency);
        return new Money(a.Amount - converted, a.Currency);
    }

    public static bool operator >(Money a, Money b)
    {
        return a.Amount > b.Amount;
    }

    public static bool operator <(Money a, Money b)
    {
        return a.Amount < b.Amount;
    }
}

class Wallet
{
    public Dictionary<string, List<Transaction>> data = new Dictionary<string, List<Transaction>>();

    public double Limit = 1000;

    public event Action<string> BudgetExceeded;

    public void Add(Transaction t)
    {
        if (!data.ContainsKey(t.Category))
            data[t.Category] = new List<Transaction>();

        data[t.Category].Add(t);

        double sum = 0;
        foreach (var tr in data[t.Category])
            sum += tr.Amount;

        if (sum > Limit && BudgetExceeded != null)
            BudgetExceeded(t.Category);
    }

    public void Filter(TransactionFilter f)
    {
        foreach (var list in data.Values)
        {
            foreach (var t in list)
            {
                if (f(t))
                    Console.WriteLine($"{t.Category}: {t.Amount}");
            }
        }
    }
}

class Program
{
    static void Main()
    {
        Wallet w = new Wallet();

        w.BudgetExceeded += (cat) =>
        {
            Console.WriteLine("Превышен бюджет в категории: " + cat);
        };

        w.Add(new Transaction(500, "Food"));
        w.Add(new Transaction(700, "Food")); // вызовет событие

        w.Filter(t => t.Amount > 600);

        Money m1 = new Money(100, "USD");
        Money m2 = new Money(100, "EUR");

        Money result = m1 + m2;
        Console.WriteLine(result.Amount + " " + result.Currency);
    }
}
