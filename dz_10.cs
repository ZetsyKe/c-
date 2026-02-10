using System;

class Program
{
    static void Main()
    {
        string[] names = new string[8]
        {
            "Борщ",
            "Цезарь",
            "Пицца",
            "Паста",
            "Стейк",
            "Суп дня",
            "Чай",
            "Торт"
        };

        int[] prices = new int[8] { 250, 320, 450, 400, 900, 280, 90, 180 };

        byte order = 0;

        while (true)
        {
            Console.WriteLine("\n=== Ресторан: Заказ ===");
            Console.WriteLine("1) Показать меню");
            Console.WriteLine("2) Добавить/убрать блюдо (переключить)");
            Console.WriteLine("3) Показать заказ");
            Console.WriteLine("4) Итоговая сумма");
            Console.WriteLine("5) Очистить заказ");
            Console.WriteLine("0) Выход");
            Console.Write("Выбор: ");

            string choice = Console.ReadLine();

            if (choice == "0") break;

            if (choice == "1")
            {
                PrintMenu(names, prices, order);
            }
            else if (choice == "2")
            {
                PrintMenu(names, prices, order);
                Console.Write("Введите номер блюда (1-8): ");
                int idx = ReadInt();

                if (idx < 1 || idx > 8)
                {
                    Console.WriteLine("Неверный номер.");
                    continue;
                }

                int bit = idx - 1;
                order = ToggleBit(order, bit);

                Console.WriteLine("Готово. Текущий байт заказа: " + order);
            }
            else if (choice == "3")
            {
                PrintOrder(names, prices, order);
                Console.WriteLine("Байт заказа: " + order);
            }
            else if (choice == "4")
            {
                int total = GetTotal(prices, order);
                Console.WriteLine("Итого: " + total + " руб.");
                Console.WriteLine("Байт заказа: " + order);
            }
            else if (choice == "5")
            {
                order = 0;
                Console.WriteLine("Заказ очищен. Байт заказа: " + order);
            }
            else
            {
                Console.WriteLine("Неизвестная команда.");
            }
        }
    }

    static void PrintMenu(string[] names, int[] prices, byte order)
    {
        Console.WriteLine("\n--- Меню (до 8 блюд, 1 байт) ---");
        for (int i = 0; i < 8; i++)
        {
            bool selected = IsBitSet(order, i);
            Console.WriteLine((i + 1) + ") " + names[i] + " - " + prices[i] + " руб. " + (selected ? "[В ЗАКАЗЕ]" : ""));
        }
    }

    static void PrintOrder(string[] names, int[] prices, byte order)
    {
        Console.WriteLine("\n--- Ваш заказ ---");
        bool any = false;

        for (int i = 0; i < 8; i++)
        {
            if (IsBitSet(order, i))
            {
                any = true;
                Console.WriteLine("- " + names[i] + " (" + prices[i] + " руб.)");
            }
        }

        if (!any) Console.WriteLine("(пусто)");
    }

    static int GetTotal(int[] prices, byte order)
    {
        int sum = 0;
        for (int i = 0; i < 8; i++)
        {
            if (IsBitSet(order, i))
                sum += prices[i];
        }
        return sum;
    }

    static bool IsBitSet(byte value, int bitIndex)
    {
        byte mask = (byte)(1 << bitIndex);
        return (value & mask) != 0;
    }

    static byte ToggleBit(byte value, int bitIndex)
    {
        byte mask = (byte)(1 << bitIndex);
        return (byte)(value ^ mask);
    }

    static int ReadInt()
    {
        string s = Console.ReadLine();
        int x;
        if (!int.TryParse(s, out x)) return int.MinValue;
        return x;
    }
}
