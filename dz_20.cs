using System;

// 1
class TranslationDictionary
{
    private string[] words = new string[100];
    private string[] translations = new string[100];
    private int count = 0;

    public string this[string key]
    {
        get
        {
            for (int i = 0; i < count; i++)
            {
                if (words[i] == key)
                    return translations[i];
            }
            return "Перевод не найден";
        }
        set
        {
            for (int i = 0; i < count; i++)
            {
                if (words[i] == key)
                {
                    translations[i] = value;
                    return;
                }
            }

            words[count] = key;
            translations[count] = value;
            count++;
        }
    }
}

// 2
struct Euro
{
    public double Value;

    public Euro(double value)
    {
        Value = value;
    }

    public static implicit operator double(Euro e)
    {
        return e.Value;
    }

    public static explicit operator Euro(double d)
    {
        return new Euro(d);
    }

    public static implicit operator Dollar(Euro e)
    {
        return new Dollar(e.Value * 1.1);
    }
}

class Dollar
{
    public double Value;

    public Dollar(double value)
    {
        Value = value;
    }
}

// 3
class Matrix2D
{
    private int[,] data;

    public Matrix2D(int size)
    {
        data = new int[size, size];
    }

    public int this[int row, int col]
    {
        get
        {
            if (row < 0 || row >= data.GetLength(0) || col < 0 || col >= data.GetLength(1))
                throw new IndexOutOfRangeException();
            return data[row, col];
        }
        set
        {
            if (row < 0 || row >= data.GetLength(0) || col < 0 || col >= data.GetLength(1))
                throw new IndexOutOfRangeException();
            data[row, col] = value;
        }
    }

    public static explicit operator int(Matrix2D m)
    {
        int sum = 0;
        for (int i = 0; i < m.data.GetLength(0); i++)
        {
            for (int j = 0; j < m.data.GetLength(1); j++)
            {
                sum += m.data[i, j];
            }
        }
        return sum;
    }
}

// 4
class SensorData
{
    private double[] temp = new double[24];

    public double this[int hour]
    {
        get
        {
            return temp[hour];
        }
        set
        {
            temp[hour] = value;
        }
    }

    public static implicit operator double(SensorData s)
    {
        double sum = 0;
        for (int i = 0; i < 24; i++)
            sum += s.temp[i];
        return sum / 24;
    }

    public static explicit operator string(SensorData s)
    {
        double max = s.temp[0];
        double min = s.temp[0];
        double sum = 0;

        for (int i = 0; i < 24; i++)
        {
            if (s.temp[i] > max) max = s.temp[i];
            if (s.temp[i] < min) min = s.temp[i];
            sum += s.temp[i];
        }

        double avg = sum / 24;
        return $"Макс: {max}, Мин: {min}, Средняя: {avg}";
    }
}

class Program
{
    static void Main()
    {
        // 1
        TranslationDictionary dict = new TranslationDictionary();
        dict["cat"] = "кот";
        dict["dog"] = "собака";
        Console.WriteLine(dict["cat"]);
        Console.WriteLine(dict["bird"]);

        // 2
        Euro e = new Euro(10);
        double x = e;
        Euro e2 = (Euro)20.5;
        Dollar d = e;
        Console.WriteLine(x);
        Console.WriteLine(e2.Value);
        Console.WriteLine(d.Value);

        // 3
        Matrix2D m = new Matrix2D(2);
        m[0, 0] = 1;
        m[0, 1] = 2;
        m[1, 0] = 3;
        m[1, 1] = 4;
        int sum = (int)m;
        Console.WriteLine(sum);

        // 4
        SensorData s = new SensorData();
        for (int i = 0; i < 24; i++)
            s[i] = i;

        double avg = s;
        string report = (string)s;
        Console.WriteLine(avg);
        Console.WriteLine(report);
    }
}
