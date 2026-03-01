using System;

struct Vector2D
{
    public double X;
    public double Y;

    public Vector2D(double x, double y)
    {
        X = x;
        Y = y;
    }

    public static Vector2D operator +(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X + b.X, a.Y + b.Y);
    }

    public static Vector2D operator -(Vector2D a, Vector2D b)
    {
        return new Vector2D(a.X - b.X, a.Y - b.Y);
    }

    public static Vector2D operator *(Vector2D v, double scalar)
    {
        return new Vector2D(v.X * scalar, v.Y * scalar);
    }

    public static Vector2D operator *(double scalar, Vector2D v)
    {
        return new Vector2D(v.X * scalar, v.Y * scalar);
    }

    public override string ToString()
    {
        return $"({X}; {Y})";
    }
}

class Fraction
{
    public int Numerator;
    public int Denominator;

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException();

        Numerator = numerator;
        Denominator = denominator;
    }

    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator
        );
    }

    public static Fraction operator -(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator
        );
    }

    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Numerator,
            a.Denominator * b.Denominator
        );
    }

    public static Fraction operator /(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator,
            a.Denominator * b.Numerator
        );
    }

    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.Numerator * b.Denominator == b.Numerator * a.Denominator;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return !(a == b);
    }

    public static bool operator <(Fraction a, Fraction b)
    {
        return a.Numerator * b.Denominator < b.Numerator * a.Denominator;
    }

    public static bool operator >(Fraction a, Fraction b)
    {
        return a.Numerator * b.Denominator > b.Numerator * a.Denominator;
    }

    public static bool operator <=(Fraction a, Fraction b)
    {
        return a < b || a == b;
    }

    public static bool operator >=(Fraction a, Fraction b)
    {
        return a > b || a == b;
    }

    public override bool Equals(object obj)
    {
        if (obj is Fraction other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return Numerator.GetHashCode() ^ Denominator.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Numerator}/{Denominator}";
    }
}

class Money
{
    public decimal Amount;
    public string Currency;

    public Money(decimal amount, string currency)
    {
        Amount = amount;
        Currency = currency;
    }

    public static Money operator +(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new ArgumentException("Разные валюты");

        return new Money(a.Amount + b.Amount, a.Currency);
    }

    public static Money operator -(Money a, Money b)
    {
        if (a.Currency != b.Currency)
            throw new ArgumentException("Разные валюты");

        return new Money(a.Amount - b.Amount, a.Currency);
    }

    public static Money operator *(Money m, decimal value)
    {
        return new Money(m.Amount * value, m.Currency);
    }

    public static bool operator ==(Money a, Money b)
    {
        return a.Amount == b.Amount && a.Currency == b.Currency;
    }

    public static bool operator !=(Money a, Money b)
    {
        return !(a == b);
    }

    public override bool Equals(object obj)
    {
        if (obj is Money other)
            return this == other;
        return false;
    }

    public override int GetHashCode()
    {
        return Amount.GetHashCode() ^ Currency.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Amount} {Currency}";
    }
}

class Matrix2x2
{
    public double A11, A12, A21, A22;

    public Matrix2x2(double a11, double a12, double a21, double a22)
    {
        A11 = a11;
        A12 = a12;
        A21 = a21;
        A22 = a22;
    }

    public static Matrix2x2 operator -(Matrix2x2 m)
    {
        return new Matrix2x2(-m.A11, -m.A12, -m.A21, -m.A22);
    }

    public static Matrix2x2 operator +(Matrix2x2 a, Matrix2x2 b)
    {
        return new Matrix2x2(
            a.A11 + b.A11,
            a.A12 + b.A12,
            a.A21 + b.A21,
            a.A22 + b.A22
        );
    }

    public static Matrix2x2 operator *(Matrix2x2 a, Matrix2x2 b)
    {
        return new Matrix2x2(
            a.A11 * b.A11 + a.A12 * b.A21,
            a.A11 * b.A12 + a.A12 * b.A22,
            a.A21 * b.A11 + a.A22 * b.A21,
            a.A21 * b.A12 + a.A22 * b.A22
        );
    }

    public override string ToString()
    {
        return $"[{A11} {A12}; {A21} {A22}]";
    }
}

struct Complex
{
    public double Real;
    public double Imaginary;

    public Complex(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }

    public static Complex operator -(Complex a, Complex b)
    {
        return new Complex(a.Real - b.Real, a.Imaginary - b.Imaginary);
    }

    public static Complex operator *(Complex a, Complex b)
    {
        return new Complex(
            a.Real * b.Real - a.Imaginary * b.Imaginary,
            a.Real * b.Imaginary + a.Imaginary * b.Real
        );
    }

    public override string ToString()
    {
        return $"{Real} + {Imaginary}i";
    }
}

class Program
{
    static void Main()
    {
        Vector2D v1 = new Vector2D(2, 3);
        Vector2D v2 = new Vector2D(1, 4);
        Console.WriteLine(v1 + v2);

        Fraction f1 = new Fraction(1, 2);
        Fraction f2 = new Fraction(3, 4);
        Console.WriteLine(f1 + f2);

        Money m1 = new Money(100, "USD");
        Money m2 = new Money(50, "USD");
        Console.WriteLine(m1 + m2);

        Matrix2x2 m = new Matrix2x2(1, 2, 3, 4);
        Console.WriteLine(-m);

        Complex c1 = new Complex(2, 3);
        Complex c2 = new Complex(1, 1);
        Console.WriteLine(c1 * c2);
    }
}
