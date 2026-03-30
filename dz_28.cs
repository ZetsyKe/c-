using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 1
        Money m1 = new Money(100, "USD");
        Money m2 = new Money(100, "EUR");

        var m3 = m1 + m2;
        Console.WriteLine(m3.Amount + " " + m3.Currency);

        Wallet wallet = new Wallet();
        wallet.BudgetExceeded += () => Console.WriteLine("Превышен бюджет!");

        wallet.AddTransaction("Food", new Transaction(1200));
        wallet.AddTransaction("Food", new Transaction(500));

        //2
        Character hero = new Character();
        Item sword = new Item { Strength = 5 };

        hero = hero + sword;
        Console.WriteLine(hero.Strength);

        hero.OnDeath += () => Console.WriteLine("Персонаж умер");
        hero.TakeDamage(200);

        //3
        ProductBatch b1 = new ProductBatch(10, 100);
        ProductBatch b2 = new ProductBatch(10, 200);

        var b3 = b1 + b2;
        Console.WriteLine(b3.Price);

        //4
        Packet p1 = new Packet("abc");
        Packet p2 = new Packet("abc");

        Console.WriteLine(p1 == p2);

        //5
        TimeSlot t1 = new TimeSlot(1, 5);
        TimeSlot t2 = new TimeSlot(3, 7);

        Console.WriteLine((t1 & t2));

        // 6
        Directory d1 = new Directory();
        d1.Files.Add(new File("a", 10));

        Directory d2 = new Directory();
        d2.Files.Add(new File("b", 20));

        var d3 = d1 + d2;
        Console.WriteLine(d3.Files.Count);

        //7
        List<Func<Packet, Packet>> pipeline = new List<Func<Packet, Packet>>();
        pipeline.Add(p => { p.Data += "1"; return p; });

        Packet p = new Packet("x");
        foreach (var step in pipeline)
            p = step(p);

        Console.WriteLine(p.Data);

        //8
        Stat s = new Stat(10);
        s.OnChanged += x => Console.WriteLine("Новое значение: " + x.Value);

        s = s + new Modifier(5);

        //9
        Price pr = new Price(100);
        Console.WriteLine(pr % 5);

        //`10
        Dispatcher disp = new Dispatcher();
        disp.OnAllCompleted += () => Console.WriteLine("Все уведомления отправлены");

        disp.SendAll();
    }
}

//1
class Money
{
    public double Amount;
    public string Currency;

    public Money(double a, string c)
    {
        Amount = a;
        Currency = c;
    }

    static double Convert(double amount, string from, string to)
    {
        if (from == to) return amount;
        return amount * 2; // простой курс
    }

    public static Money operator +(Money a, Money b)
    {
        double converted = Convert(b.Amount, b.Currency, a.Currency);
        return new Money(a.Amount + converted, a.Currency);
    }

    public static bool operator >(Money a, Money b) => a.Amount > b.Amount;
    public static bool operator <(Money a, Money b) => a.Amount < b.Amount;
}

class Transaction
{
    public double Amount;
    public Transaction(double a) { Amount = a; }
}

class Wallet
{
    public Dictionary<string, List<Transaction>> data = new Dictionary<string, List<Transaction>>();
    public delegate bool TransactionFilter(Transaction t);

    public event Action BudgetExceeded;

    public double Limit = 1500;

    public void AddTransaction(string cat, Transaction t)
    {
        if (!data.ContainsKey(cat))
            data[cat] = new List<Transaction>();

        data[cat].Add(t);

        double sum = data[cat].Sum(x => x.Amount);
        if (sum > Limit)
            BudgetExceeded?.Invoke();
    }
}

//2
class Stats
{
    public int Strength;
}

class Item : Stats { }

class Character : Stats
{
    public event Action OnDeath;

    public static Character operator +(Character c, Item i)
    {
        c.Strength += i.Strength;
        return c;
    }

    public void TakeDamage(int dmg)
    {
        if (dmg > 100)
            OnDeath?.Invoke();
    }
}

//3
class ProductBatch
{
    public int Count;
    public double Price;

    public ProductBatch(int c, double p)
    {
        Count = c;
        Price = p;
    }

    public static ProductBatch operator +(ProductBatch a, ProductBatch b)
    {
        int total = a.Count + b.Count;
        double avg = (a.Price * a.Count + b.Price * b.Count) / total;
        return new ProductBatch(total, avg);
    }
}

//4
class Packet
{
    public string Data;

    public Packet(string d)
    {
        Data = d;
    }

    public static bool operator ==(Packet a, Packet b)
    {
        return a.Data == b.Data;
    }

    public static bool operator !=(Packet a, Packet b)
    {
        return a.Data != b.Data;
    }
}

//5
class TimeSlot
{
    public int Start, End;

    public TimeSlot(int s, int e)
    {
        Start = s;
        End = e;
    }

    public static bool operator &(TimeSlot a, TimeSlot b)
    {
        return !(a.End < b.Start || b.End < a.Start);
    }
}

// 6
class File
{
    public string Name;
    public int Size;

    public File(string n, int s)
    {
        Name = n;
        Size = s;
    }
}
//7
class Directory
{
    public List<File> Files = new List<File>();
    public Dictionary<string, Directory> Dirs = new Dictionary<string, Directory>();

    public static Directory operator +(Directory a, Directory b)
    {
        Directory res = new Directory();
        res.Files.AddRange(a.Files);
        res.Files.AddRange(b.Files);
        return res;
    }

    public static bool operator >(Directory a, Directory b)
    {
        return a.Files.Sum(x => x.Size) > b.Files.Sum(x => x.Size);
    }

    public static bool operator <(Directory a, Directory b)
    {
        return a.Files.Sum(x => x.Size) < b.Files.Sum(x => x.Size);
    }
}

//8
class Modifier
{
    public int Value;
    public Modifier(int v) { Value = v; }
}

class Stat
{
    public int Value;

    public event Action<Stat> OnChanged;

    public Stat(int v)
    {
        Value = v;
    }

    public static Stat operator +(Stat s, Modifier m)
    {
        s.Value += m.Value;
        s.OnChanged?.Invoke(s);
        return s;
    }
}

// 9
class Price
{
    public double Value;

    public Price(double v)
    {
        Value = v;
    }

    public static double operator %(Price p, int percent)
    {
        return p.Value * percent / 100;
    }
}

//10
class Dispatcher
{
    public event Action OnAllCompleted;

    public void SendAll()
    {
        // просто сразу вызываем
        OnAllCompleted?.Invoke();
    }
}
