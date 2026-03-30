using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        // 1
        SmartStack<int> s1 = new SmartStack<int>(5);
        s1.Push(1);
        s1.Push(2);

        SmartStack<int> s2 = new SmartStack<int>(5);
        s2.Push(3);
        s2.Push(4);

        var s3 = s1 + s2;

        foreach (var x in s3.GetReverse())
            Console.Write(x + " ");
        Console.WriteLine();

        // 2
        Polynomial p1 = new Polynomial(new List<int> { 1, 2, 0 });
        Polynomial p2 = new Polynomial(new List<int> { 0, 1, 3 });

        var sum = p1 + p2;

        foreach (var (coef, power) in sum.GetNonZeroCoefficients())
            Console.WriteLine($"coef={coef}, power={power}");

        // 3
        SafeDictionary<int, string> d1 = new SafeDictionary<int, string>();
        d1.Add(1, "a");
        d1.Add(2, "b");

        SafeDictionary<int, string> d2 = new SafeDictionary<int, string>();
        d2.Add(2, "b");

        var d3 = d1 - d2;

        foreach (var k in d3.GetSortedKeys())
            Console.WriteLine(k);

        //4
        Folder root = new Folder("root");
        root = root + "file1.txt";
        root / "sub" + "file2.txt";

        foreach (var f in root.GetAllFiles())
            Console.WriteLine(f);

        // 5
        Tensor2D t = new Tensor2D(3, 3);
        t[0, 0] = 5;

        if (t)
            Console.WriteLine("Есть элементы");

        foreach (var v in t.GetSpiral())
            Console.Write(v + " ");
    }
}

// SmartStack
class SmartStack<T>
{
    private List<T> data = new List<T>();
    private int limit;

    public SmartStack(int limit)
    {
        this.limit = limit;
    }

    public void Push(T item)
    {
        if (data.Count >= limit)
            throw new InvalidOperationException();
        data.Add(item);
    }

    public IEnumerable<T> GetReverse()
    {
        for (int i = data.Count - 1; i >= 0; i--)
            yield return data[i];
    }

    public static SmartStack<T> operator +(SmartStack<T> a, SmartStack<T> b)
    {
        SmartStack<T> result = new SmartStack<T>(a.limit);

        foreach (var x in a.data)
            result.Push(x);

        foreach (var x in b.data)
            result.Push(x);

        return result;
    }
}

//Polynomial
class Polynomial
{
    public List<int> coef;

    public Polynomial(List<int> coef)
    {
        this.coef = coef;
    }

    public static Polynomial operator +(Polynomial a, Polynomial b)
    {
        int n = Math.Max(a.coef.Count, b.coef.Count);
        List<int> res = new List<int>();

        for (int i = 0; i < n; i++)
        {
            int x = i < a.coef.Count ? a.coef[i] : 0;
            int y = i < b.coef.Count ? b.coef[i] : 0;
            res.Add(x + y);
        }

        return new Polynomial(res);
    }

    public static Polynomial operator -(Polynomial a, Polynomial b)
    {
        int n = Math.Max(a.coef.Count, b.coef.Count);
        List<int> res = new List<int>();

        for (int i = 0; i < n; i++)
        {
            int x = i < a.coef.Count ? a.coef[i] : 0;
            int y = i < b.coef.Count ? b.coef[i] : 0;
            res.Add(x - y);
        }

        return new Polynomial(res);
    }

    public static Polynomial operator *(Polynomial a, int k)
    {
        return new Polynomial(a.coef.Select(x => x * k).ToList());
    }

    public IEnumerable<(int, int)> GetNonZeroCoefficients()
    {
        for (int i = 0; i < coef.Count; i++)
            if (coef[i] != 0)
                yield return (coef[i], i);
    }
}

//SafeDictionary
class SafeDictionary<TKey, TValue>
{
    private Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>();

    public void Add(TKey key, TValue value)
    {
        dict[key] = value;
    }

    public static SafeDictionary<TKey, TValue> operator -(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
    {
        SafeDictionary<TKey, TValue> res = new SafeDictionary<TKey, TValue>();

        foreach (var kv in a.dict)
        {
            if (!b.dict.ContainsKey(kv.Key))
                res.Add(kv.Key, kv.Value);
        }

        return res;
    }

    public IEnumerable<TKey> GetSortedKeys()
    {
        var keys = dict.Keys.ToList();
        keys.Sort();
        return keys;
    }

    public static bool operator ==(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
    {
        return a.dict.SequenceEqual(b.dict);
    }

    public static bool operator !=(SafeDictionary<TKey, TValue> a, SafeDictionary<TKey, TValue> b)
    {
        return !(a == b);
    }
}

//Folde
class Folder
{
    public string Name;
    public List<string> files = new List<string>();
    public List<Folder> folders = new List<Folder>();

    public Folder(string name)
    {
        Name = name;
    }

    public static Folder operator +(Folder f, string file)
    {
        f.files.Add(file);
        return f;
    }

    public static Folder operator /(Folder f, string name)
    {
        var found = f.folders.FirstOrDefault(x => x.Name == name);
        if (found == null)
        {
            found = new Folder(name);
            f.folders.Add(found);
        }
        return found;
    }

    public IEnumerable<string> GetAllFiles()
    {
        foreach (var file in files)
            yield return file;

        foreach (var folder in folders)
            foreach (var f in folder.GetAllFiles())
                yield return f;
    }
}

//Tensor2D
class Tensor2D
{
    private Dictionary<(int, int), double> data = new Dictionary<(int, int), double>();
    private int rows, cols;

    public Tensor2D(int r, int c)
    {
        rows = r;
        cols = c;
    }

    public double this[int r, int c]
    {
        get
        {
            return data.ContainsKey((r, c)) ? data[(r, c)] : 0;
        }
        set
        {
            if (value != 0)
                data[(r, c)] = value;
        }
    }

    public static Tensor2D operator *(Tensor2D a, Tensor2D b)
    {
        Tensor2D res = new Tensor2D(a.rows, b.cols);

        for (int i = 0; i < a.rows; i++)
            for (int j = 0; j < b.cols; j++)
                for (int k = 0; k < a.cols; k++)
                    res[i, j] += a[i, k] * b[k, j];

        return res;
    }

    public IEnumerable<double> GetSpiral()
    {
        int top = 0, bottom = rows - 1, left = 0, right = cols - 1;

        while (top <= bottom && left <= right)
        {
            for (int i = left; i <= right; i++) yield return this[top, i];
            top++;

            for (int i = top; i <= bottom; i++) yield return this[i, right];
            right--;

            if (top <= bottom)
                for (int i = right; i >= left; i--) yield return this[bottom, i];
            bottom--;

            if (left <= right)
                for (int i = bottom; i >= top; i--) yield return this[i, left];
            left++;
        }
    }

    public static bool operator true(Tensor2D t)
    {
        return t.data.Count > 0;
    }

    public static bool operator false(Tensor2D t)
    {
        return t.data.Count == 0;
    }
}
