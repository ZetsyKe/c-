using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        //1
        HashSet<int> source1 = new HashSet<int> { 1, 2, 3, 4 };
        HashSet<int> source2 = new HashSet<int> { 2, 3, 4, 5 };
        HashSet<int> source3 = new HashSet<int> { 3, 4, 5, 6 };

        HashSet<int> blacklist = new HashSet<int> { 4 };

        HashSet<int> result = new HashSet<int>(source1);
        result.IntersectWith(source2);
        result.IntersectWith(source3);
        result.ExceptWith(blacklist);

        Console.WriteLine("Общие без черного списка:");
        foreach (var x in result)
            Console.Write(x + " ");
        Console.WriteLine();


        // 2
        HashSet<string> versionA = new HashSet<string> { "Login", "Search", "Chat" };
        HashSet<string> versionB = new HashSet<string> { "Login", "Search", "Profile", "Shop" };

        HashSet<string> added = new HashSet<string>(versionB);
        added.ExceptWith(versionA);

        HashSet<string> removed = new HashSet<string>(versionA);
        removed.ExceptWith(versionB);

        Console.WriteLine("Добавленные:");
        foreach (var x in added)
            Console.Write(x + " ");
        Console.WriteLine();

        Console.WriteLine("Удаленные:");
        foreach (var x in removed)
            Console.Write(x + " ");
        Console.WriteLine();


        // 3
        HashSet<string> user1 = new HashSet<string> { "music", "games", "sports" };
        HashSet<string> user2 = new HashSet<string> { "games", "movies", "travel" };

        HashSet<string> unique = new HashSet<string>(user1);
        unique.SymmetricExceptWith(user2);

        Console.WriteLine("Уникальные интересы:");
        foreach (var x in unique)
            Console.Write(x + " ");
    }
}
