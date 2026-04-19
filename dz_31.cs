using System;
using System.Collections.Generic;

public interface IMenuItem
{
    double GetPrice();
    string GetName();
}

public class Dish : IMenuItem
{
    private string name;
    private double price;

    public Dish(string name, double price)
    {
        this.name = name;
        this.price = price;
    }

    public double GetPrice()
    {
        return price;
    }

    public string GetName()
    {
        return name;
    }
}

public class ComboDeal : IMenuItem
{
    private List<IMenuItem> items = new List<IMenuItem>();
    private string name;

    public ComboDeal(string name)
    {
        this.name = name;
    }

    public void Add(IMenuItem item)
    {
        items.Add(item);
    }

    public double GetPrice()
    {
        double sum = 0;
        foreach (var item in items)
            sum += item.GetPrice();
        return sum * 0.9;
    }

    public string GetName()
    {
        return name;
    }

    public List<IMenuItem> GetItems()
    {
        return items;
    }
}

public abstract class DishDecorator : IMenuItem
{
    protected IMenuItem item;

    public DishDecorator(IMenuItem item)
    {
        this.item = item;
    }

    public abstract double GetPrice();
    public abstract string GetName();
}

public class CheeseDecorator : DishDecorator
{
    public CheeseDecorator(IMenuItem item) : base(item) { }

    public override double GetPrice()
    {
        return item.GetPrice() + 50;
    }

    public override string GetName()
    {
        return item.GetName() + " + Сыр";
    }
}

public class MushroomDecorator : DishDecorator
{
    public MushroomDecorator(IMenuItem item) : base(item) { }

    public override double GetPrice()
    {
        return item.GetPrice() + 40;
    }

    public override string GetName()
    {
        return item.GetName() + " + Грибы";
    }
}

public class Program
{
    static void Print(IMenuItem item, string indent = "")
    {
        if (item is ComboDeal combo)
        {
            Console.WriteLine(indent + combo.GetName() + " : " + combo.GetPrice());
            foreach (var i in combo.GetItems())
                Print(i, indent + "  ");
        }
        else
        {
            Console.WriteLine(indent + item.GetName() + " : " + item.GetPrice());
        }
    }

    public static void Main()
    {
        IMenuItem pizza = new Dish("Пицца Маргарита", 300);
        pizza = new CheeseDecorator(pizza);
        pizza = new CheeseDecorator(pizza);

        IMenuItem drink = new Dish("Напиток", 100);

        IMenuItem fries = new Dish("Картофель", 120);
        IMenuItem sauce = new Dish("Соус", 50);

        ComboDeal miniCombo = new ComboDeal("Мини-комбо");
        miniCombo.Add(fries);
        miniCombo.Add(sauce);

        ComboDeal megaCombo = new ComboDeal("Мега-комбо");
        megaCombo.Add(pizza);
        megaCombo.Add(drink);
        megaCombo.Add(miniCombo);

        Print(megaCombo);
    }
}
