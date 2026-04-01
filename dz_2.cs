using System;
using System.IO;
using System.Linq;

class FileSystemInspector
{
    static void Main()
    {
        Console.Write("Введите путь к директории: ");
        string path = Console.ReadLine();

        try
        {
            if (!Directory.Exists(path))
                throw new DirectoryNotFoundException("Директория не найдена.");

            DirectoryInfo dir = new DirectoryInfo(path);


            Console.WriteLine("\nИнформация о директории");
            Console.WriteLine($"Полный путь: {dir.FullName}");
            Console.WriteLine($"Дата создания: {dir.CreationTime}");
            Console.WriteLine($"Дата изменения: {dir.LastWriteTime}");


            var files = dir.GetFiles();
            var directories = dir.GetDirectories();

            Console.WriteLine("\nСтатистика ");
            Console.WriteLine($"Количество файлов: {files.Length}");
            Console.WriteLine($"Количество поддиректорий: {directories.Length}");


            Console.WriteLine("\n=== Поддиректории ===");
            foreach (var d in directories)
            {
                Console.WriteLine($"{d.Name} (создана: {d.CreationTime})");
            }


            Console.WriteLine("\nФайлы");
            foreach (var f in files)
            {
                Console.WriteLine($"{f.Name} | {f.Length} байт | {f.Extension}");
            }


            if (files.Length > 0)
            {
                var largest = files.OrderByDescending(f => f.Length).First();
                var smallest = files.OrderBy(f => f.Length).First();

                Console.WriteLine("\n Размеры файлов");
                Console.WriteLine($"Самый большой: {largest.Name} ({largest.Length} байт)");
                Console.WriteLine($"Самый маленький: {smallest.Name} ({smallest.Length} байт)");
            }


            int totalFiles = CountFilesRecursive(dir);
            Console.WriteLine($"\nОбщее количество файлов (включая вложенные): {totalFiles}");


            Console.WriteLine("\n Дерево каталогов ");
            PrintTree(dir, "", true);
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Ошибка: нет доступа к директории.");
        }
        catch (ArgumentException)
        {
            Console.WriteLine("Ошибка: неверный путь.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Неизвестная ошибка: {ex.Message}");
        }
    }

    static int CountFilesRecursive(DirectoryInfo dir)
    {
        int count = 0;

        try
        {
            count += dir.GetFiles().Length;

            foreach (var subDir in dir.GetDirectories())
            {
                count += CountFilesRecursive(subDir);
            }
        }
        catch { }

        return count;
    }

    static void PrintTree(DirectoryInfo dir, string indent, bool last)
    {
        Console.WriteLine(indent + (last ? "└── " : "├── ") + dir.Name);

        indent += last ? "    " : "│   ";

        var subDirs = dir.GetDirectories();
        var files = dir.GetFiles();

        for (int i = 0; i < subDirs.Length; i++)
        {
            PrintTree(subDirs[i], indent, i == subDirs.Length - 1 && files.Length == 0);
        }

        for (int i = 0; i < files.Length; i++)
        {
            Console.WriteLine(indent + (i == files.Length - 1 ? "└── " : "├── ") + files[i].Name);
        }
    }
}
