using System;
using MyVectorDecimal;

namespace Triangle;

class ITriangle
{
    protected int a;
    protected int b;

    protected int color;

    public ITriangle(int a, int b, int color)
    {
        //if (a < 0 || b < 0)
        //{
        //    throw new ArgumentException("Sides must be greater than 0");
        //}

        if (!IsTriangleCorrect(a, b))
        {
            throw new ArgumentException("Triangle is not correct");
        }

        this.a = a;
        this.b = b;
        this.color = color;
    }

    public int A
    {
        get => a;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Side A must be greater than 0");
            }
            a = value;
        }
    }

    public int B
    {
        get => b;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Side B must be greater than 0");
            }
            b = value;
        }
    }

    public int Color
    {
        get => color;
    }

    public int this[int index]
    {
        get
        {
            switch (index)
            {
                case 0:
                    return A;
                case 1:
                    return B;
                case 2:
                    return Color;
                default:
                    throw new IndexOutOfRangeException("Invalid index. Allowed values: 0, 1, 2");
            }
        }
    }

    public static ITriangle operator ++(ITriangle triangle)
    {
        triangle.A++;
        triangle.B++;

        return triangle;
    }

    public static ITriangle operator --(ITriangle triangle)
    {
        triangle.A--;
        triangle.B--;

        return triangle;
    }

    public static bool operator true(ITriangle triangle)
    {
        return triangle.IsTriangleCorrect();
    }

    public static bool operator false(ITriangle triangle)
    {
        return !triangle.IsTriangleCorrect();
    }

    public static ITriangle operator *(ITriangle triangle, int scalar)
    {
        triangle.A *= scalar;
        triangle.B *= scalar;

        return triangle;
    }

    public override string ToString()
    {
        return $"{A},{B},{Color}";
    }

    public static ITriangle Parse(string input)
    {
        string[] values = input.Split(',');

        if (
            values.Length != 3
            || !int.TryParse(values[0], out int a)
            || !int.TryParse(values[1], out int b)
            || !int.TryParse(values[2], out int color)
        )
        {
            throw new FormatException("Invalid format for ITriangle string representation.");
        }

        return new ITriangle(a, b, color);
    }

    public void PrintSides()
    {
        Console.WriteLine("Sides:");
        Console.WriteLine("  Side A: " + A);
        Console.WriteLine("  Side B: " + B);
        Console.WriteLine("  Side C: " + A);
    }

    public int Perimeter()
    {
        return A * 2 + B;
    }

    public double Area()
    {
        double area = (B / 2) * Math.Sqrt(A * A - (B * B) / 4);
        return Math.Round(area, 2);
    }

    public bool IsTriangleCorrect()
    {
        return A + B > A && A + B > B && A + A > B;
    }

    private static bool IsTriangleCorrect(int a, int b)
    {
        return a + b > a && a + b > b && a + a > b;
    }

    public static void PrintTriangle(ITriangle triangle)
    {
        Console.WriteLine("-----------------------------------------------------------");
        triangle.PrintSides();
        Console.WriteLine("Triangle perimeter: " + triangle.Perimeter());
        Console.WriteLine("Triangle area: " + triangle.Area());
        Console.WriteLine("Triangle color: " + triangle.Color);
    }

    public static void PrintAllTriangles(ITriangle[] triangles)
    {
        foreach (var triangle in triangles)
        {
            PrintTriangle(triangle);
        }
    }
}

public class Task1
{
    public static void Task()
    {
        Triangle.ITriangle triangle = new(3, 4, 5);
        //try
        //{
        //    Triangle.ITriangle triangletest;
        //    Console.WriteLine((triangletest = new(1, 2, 1)) ? "Yes" : "No");
        //}
        //catch (Exception ex)
        //{
        //    Console.WriteLine("Error: " + ex.Message);
        //}

        try
        {
            int invalidValue = triangle[2];
            Console.WriteLine("Field at specified index: " + invalidValue);
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        Console.WriteLine("--------------------------------------------------------------");

        triangle.PrintSides();
        ++triangle;
        Console.WriteLine("After increment: A = " + triangle.A + ", B = " + triangle.B);
        --triangle;
        Console.WriteLine("After decrement: A = " + triangle.A + ", B = " + triangle.B);

        Console.WriteLine("Does triangle exist? " + (triangle ? "Yes" : "No"));

        triangle = triangle * 2;
        Console.WriteLine("After multiplication: A = " + triangle.A + ", B = " + triangle.B);

        Console.WriteLine("--------------------------------------------------------------");
        string triangleString = triangle.ToString();
        Console.WriteLine($"ITriangle as string: {triangleString}");

        // Convert the string back to an ITriangle
        ITriangle parsedTriangle = ITriangle.Parse(triangleString);
        Console.WriteLine(
            $"Parsed ITriangle: A={parsedTriangle.A}, B={parsedTriangle.B}, Color={parsedTriangle.Color}"
        );
    }
}
