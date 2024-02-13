namespace MatrixDecimal;

using System;
using MyVectorDecimal;

public class DecimalMatrix
{
    protected decimal[,] DCArray; // Array of elements
    protected uint n,
        m; // Dimensions of the matrix
    protected int codeError; // Error code
    protected static int num_mf; // Number of matrices

    // Constructors
    public DecimalMatrix()
    {
        DCArray = new decimal[1, 1];
        n = m = 1;
        codeError = 0;
        num_mf++;
    }

    public DecimalMatrix(uint rows, uint columns)
    {
        DCArray = new decimal[rows, columns];
        n = rows;
        m = columns;
        codeError = 0;
        num_mf++;
    }

    public DecimalMatrix(uint rows, uint columns, decimal initialValue)
    {
        DCArray = new decimal[rows, columns];
        n = rows;
        m = columns;
        codeError = 0;
        num_mf++;

        // Initialize the elements with the specified value
        for (uint i = 0; i < rows; i++)
        {
            for (uint j = 0; j < columns; j++)
            {
                DCArray[i, j] = initialValue;
            }
        }
    }

    // Destructor
    ~DecimalMatrix()
    {
        Console.WriteLine("Matrix destroyed!");
    }

    // Methods
    public void Input()
    {
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                Console.Write($"Enter element at index ({i}, {j}): ");
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                {
                    DCArray[i, j] = value;
                }
                else
                {
                    codeError = -1;
                    return;
                }
            }
        }
    }

    public void Display()
    {
        Console.WriteLine("Matrix elements:");
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                Console.Write($"{DCArray[i, j]} ");
            }
            Console.WriteLine();
        }
    }

    public void AssignValue(decimal value)
    {
        for (uint i = 0; i < n; i++)
        {
            for (uint j = 0; j < m; j++)
            {
                DCArray[i, j] = value;
            }
        }
    }

    public static int CountMatrices()
    {
        return num_mf;
    }

    // Properties
    public uint Rows
    {
        get { return n; }
    }

    public uint Columns
    {
        get { return m; }
    }

    public int CodeError
    {
        get { return codeError; }
        set { codeError = value; }
    }

    // Indexers
    public decimal this[uint i, uint j]
    {
        get
        {
            if (i < 0 || i >= n || j < 0 || j >= m)
            {
                codeError = -1;
                return 0;
            }

            codeError = 0;
            return DCArray[i, j];
        }
        set
        {
            if (i < 0 || i >= n || j < 0 || j >= m)
            {
                codeError = -1;
                return;
            }

            DCArray[i, j] = value;
            codeError = 0;
        }
    }

    public decimal this[uint k]
    {
        get
        {
            if (k < 0 || k >= n * m)
            {
                codeError = -1;
                return 0;
            }
            codeError = 0;
            return DCArray[k / m, k % m];
        }
        set
        {
            if (k < 0 || k >= n * m)
            {
                codeError = -1;
                return;
            }
            DCArray[k / m, k % m] = value;
            codeError = 0;
        }
    }

    // Overload unary operations
    public static DecimalMatrix operator ++(DecimalMatrix matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.DCArray[i, j]++;
            }
        }

        return matrix;
    }

    public static DecimalMatrix operator --(DecimalMatrix matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.DCArray[i, j]--;
            }
        }

        return matrix;
    }

    public static bool operator true(DecimalMatrix matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                if (matrix.DCArray[i, j] != 0)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public static bool operator false(DecimalMatrix matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                if (matrix.DCArray[i, j] != 0)
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !(DecimalMatrix matrix)
    {
        int zeroCount = 0;

        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                if (matrix[i, j] == 0)
                {
                    zeroCount++;
                }
            }
        }

        if (zeroCount == matrix.Rows * matrix.Columns)
        {
            return false;
        }

        return true;
    }

    public static DecimalMatrix operator ~(DecimalMatrix matrix)
    {
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                matrix.DCArray[i, j] = ~decimal.ToInt32(matrix.DCArray[i, j]);
            }
        }

        return matrix;
    }

    public static DecimalMatrix operator +(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for addition.");

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result[i, j] = matrix1[i, j] + matrix2[i, j];
            }
        }
        return result;
    }

    public static DecimalMatrix operator +(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result[i, j] = matrix[i, j] + scalar;
            }
        }
        return result;
    }

    public static DecimalMatrix operator -(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for subtraction.");

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                result[i, j] = matrix1[i, j] - matrix2[i, j];
            }
        }
        return result;
    }

    public static DecimalMatrix operator -(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result[i, j] = matrix[i, j] - scalar;
            }
        }
        return result;
    }

    public static DecimalMatrix operator *(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.m != matrix2.n)
            throw new ArgumentException(
                "Number of columns in the first matrix must be equal to the number of rows in the second matrix."
            );

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix2.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix2.m; j++)
            {
                decimal sum = 0;
                for (uint k = 0; k < matrix1.m; k++)
                {
                    sum += matrix1[i, k] * matrix2[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }

    public static DecimalMatrix operator *(DecimalMatrix matrix, VectorDecimal vector)
    {
        if (matrix.m != vector.Dimension)
            throw new ArgumentException(
                "Number of columns in the matrix must be equal to the dimension of the vector."
            );

        DecimalMatrix result = new DecimalMatrix(matrix.n, 1);
        for (uint i = 0; i < matrix.n; i++)
        {
            decimal sum = 0;
            for (uint j = 0; j < matrix.m; j++)
            {
                sum += matrix[i, j] * vector[j];
            }
            result[i, 0] = sum;
        }
        return result;
    }

    public static DecimalMatrix operator *(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result[i, j] = matrix[i, j] * scalar;
            }
        }
        return result;
    }

    public static DecimalMatrix operator /(DecimalMatrix matrix, decimal scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Cannot divide by zero.");

        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result[i, j] = matrix[i, j] / scalar;
            }
        }
        return result;
    }

    public static DecimalMatrix operator /(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for division.");

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix2[i, j] == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero.");
                }

                result[i, j] = matrix1[i, j] / matrix2[i, j];
            }
        }
        return result;
    }

    public static DecimalMatrix operator %(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for remainder operation.");

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix2[i, j] == 0)
                {
                    throw new DivideByZeroException("Cannot find remainder by zero.");
                }

                result[i, j] = matrix1[i, j] % matrix2[i, j];
            }
        }
        return result;
    }

    public static DecimalMatrix operator %(DecimalMatrix matrix, uint scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException("Cannot find remainder by zero.");

        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                result[i, j] = matrix[i, j] % scalar;
            }
        }
        return result;
    }

    //Overload bitwise binary operations
    public static DecimalMatrix operator |(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException(
                "Matrix dimensions must be equal for bitwise OR operation."
            );

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);
        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                int intResult = (int)matrix1[i, j] | (int)matrix2[i, j];
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    public static DecimalMatrix operator |(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        int intScalar = (int)scalar;

        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                int intResult = (int)matrix[i, j] | intScalar;
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    public static DecimalMatrix operator ^(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException(
                "Matrix dimensions must be equal for bitwise XOR operation."
            );

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                int intResult = (int)matrix1[i, j] ^ (int)matrix2[i, j];
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    public static DecimalMatrix operator ^(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        int intScalar = (int)scalar;

        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                int intResult = (int)matrix[i, j] ^ intScalar;
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    public static DecimalMatrix operator &(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException(
                "Matrix dimensions must be equal for bitwise AND operation."
            );

        DecimalMatrix result = new DecimalMatrix(matrix1.n, matrix1.m);

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                int intResult = (int)matrix1[i, j] & (int)matrix2[i, j];
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    public static DecimalMatrix operator &(DecimalMatrix matrix, decimal scalar)
    {
        DecimalMatrix result = new DecimalMatrix(matrix.n, matrix.m);
        int intScalar = (int)scalar;

        for (uint i = 0; i < matrix.n; i++)
        {
            for (uint j = 0; j < matrix.m; j++)
            {
                int intResult = (int)matrix[i, j] & intScalar;
                result[i, j] = (decimal)intResult;
            }
        }
        return result;
    }

    // Overload equality and inequality operations
    public static bool operator ==(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (ReferenceEquals(matrix1, null) && ReferenceEquals(matrix2, null))
            return true;

        if (ReferenceEquals(matrix1, null) || ReferenceEquals(matrix2, null))
            return false;

        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            return false;

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (matrix1[i, j] != matrix2[i, j])
                    return false;
            }
        }

        return true;
    }

    public static bool operator !=(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        return !(matrix1 == matrix2);
    }

    // Overload comparison operations
    public static bool operator >(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for comparison.");

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (!(matrix1[i, j] > matrix2[i, j]))
                    return false;
            }
        }

        return true;
    }

    public static bool operator >=(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for comparison.");

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (!(matrix1[i, j] >= matrix2[i, j]))
                    return false;
            }
        }

        return true;
    }

    public static bool operator <(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for comparison.");

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (!(matrix1[i, j] < matrix2[i, j]))
                    return false;
            }
        }

        return true;
    }

    public static bool operator <=(DecimalMatrix matrix1, DecimalMatrix matrix2)
    {
        if (matrix1.n != matrix2.n || matrix1.m != matrix2.m)
            throw new ArgumentException("Matrix dimensions must be equal for comparison.");

        for (uint i = 0; i < matrix1.n; i++)
        {
            for (uint j = 0; j < matrix1.m; j++)
            {
                if (!(matrix1[i, j] <= matrix2[i, j]))
                    return false;
            }
        }

        return true;
    }
}

public class Task3
{
    public static void Task()
    {
        DecimalMatrix mat1 = new DecimalMatrix();
        Console.WriteLine("Default Matrix:");
        mat1.Display();
        Console.WriteLine();

        DecimalMatrix mat2 = new DecimalMatrix(3, 3, 5);
        Console.WriteLine("Parameterized Matrix (all elements initialized to 5):");
        mat2.Display();
        Console.WriteLine();

        DecimalMatrix mat3 = new DecimalMatrix(2, 2);
        Console.WriteLine("Enter elements for Matrix 3:");
        mat3.Input();
        Console.WriteLine("Matrix 3:");
        mat3.Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 after incrementing (++):");
        ++mat3;
        mat3.Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 after decrementing (--):");
        --mat3;
        mat3.Display();
        Console.WriteLine();

        Console.WriteLine("Is Matrix 3 true? " + (mat3 ? "Yes" : "No"));
        Console.WriteLine("Is Matrix 3 false? " + (!mat3 ? "No" : "Yes"));

        Console.WriteLine("Logical NOT (!) Matrix 3:");
        Console.WriteLine(!mat3);
        Console.WriteLine();

        Console.WriteLine("Bitwise NOT (~) Matrix 3:");
        (~mat3).Display();
        Console.WriteLine();

        DecimalMatrix mat4 = new DecimalMatrix(2, 2, 3);
        Console.WriteLine("Matrix 4 (all elements initialized to 3):");
        mat4.Display();

        Console.WriteLine("Matrix 3 + Matrix 4:");
        (mat3 + mat4).Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 - Matrix 4:");
        (mat3 - mat4).Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 * Matrix 4:");
        (mat3 * mat4).Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 / Matrix 4:");
        (mat3 / mat4).Display();
        Console.WriteLine();

        Console.WriteLine("Matrix 3 % Matrix 4:");
        (mat3 % mat4).Display();
        Console.WriteLine();

        Console.WriteLine("Is Matrix 3 equal to Matrix 4? " + (mat3 == mat4));
        Console.WriteLine("Is Matrix 3 not equal to Matrix 4? " + (mat3 != mat4));
        Console.WriteLine("Is Matrix 3 greater than Matrix 4? " + (mat3 > mat4));
        Console.WriteLine("Is Matrix 3 greater than or equal to Matrix 4? " + (mat3 >= mat4));
        Console.WriteLine("Is Matrix 3 less than Matrix 4? " + (mat3 < mat4));
        Console.WriteLine("Is Matrix 3 less than or equal to Matrix 4? " + (mat3 <= mat4));
    }
}
