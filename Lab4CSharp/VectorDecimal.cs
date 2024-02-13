using System;
using MyVectorDecimal;

namespace MyVectorDecimal
{
    public class VectorDecimal
    {
        protected decimal[] ArrayDecimal; // Array of elements
        protected uint num; // Number of elements
        protected int codeError; // Error code
        protected static uint num_vec; // Number of vectors

        public VectorDecimal()
        {
            ArrayDecimal = new decimal[1];
            num = 1;
            codeError = 0;
            num_vec++;
        }

        public VectorDecimal(uint size)
        {
            ArrayDecimal = new decimal[size];
            num = size;
            codeError = 0;
            num_vec++;
        }

        public VectorDecimal(uint size, decimal initValue)
        {
            ArrayDecimal = new decimal[size];
            num = size;
            codeError = 0;

            for (uint i = 0; i < size; i++)
            {
                ArrayDecimal[i] = initValue;
            }

            num_vec++;
        }

        ~VectorDecimal()
        {
            Console.WriteLine($"Destructor: {this}");
        }

        public void Input()
        {
            for (uint i = 0; i < num; i++)
            {
                Console.Write($"Enter element at index {i}: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal value))
                {
                    ArrayDecimal[i] = value;
                }
                else
                {
                    codeError = 1;
                    return;
                }
            }
        }

        public void Display()
        {
            Console.Write("Vector elements: ");
            for (uint i = 0; i < num; i++)
            {
                Console.Write($"{ArrayDecimal[i]} ");
            }
            Console.WriteLine();
        }

        //public void AssignValue(decimal[] values)
        //{
        //    if (values.Length != num)
        //    {
        //        codeError = 1;
        //        return;
        //    }

        //    // Copy values from input array to ArrayDecimal
        //    for (uint i = 0; i < num; i++)
        //    {
        //        ArrayDecimal[i] = values[i];
        //    }
        //}

        public void AssignValue(decimal value)
        {
            //for (uint i = 0; i < num; i++)
            //{
            //    ArrayDecimal[i] = value;
            //}
            Array.Fill(ArrayDecimal, value);
        }

        public static uint CountVectors()
        {
            return num_vec;
        }

        public decimal this[uint index]
        {
            get
            {
                if (index < 0 || index >= num)
                {
                    codeError = 1;
                    return 0;
                }

                codeError = 0;
                return ArrayDecimal[index];
            }
            set
            {
                if (index < 0 || index >= num)
                {
                    codeError = 1;
                    return;
                }

                ArrayDecimal[index] = value;
                codeError = 0;
            }
        }

        public int Dimension
        {
            get { return (int)num; }
        }

        public int CodeError
        {
            get { return codeError; }
            set { codeError = value; }
        }

        public static bool operator true(VectorDecimal vector)
        {
            return vector.num != 0 && !Array.TrueForAll(vector.ArrayDecimal, el => el == 0);
        }

        public static bool operator false(VectorDecimal vector)
        {
            return vector.num == 0 || Array.TrueForAll(vector.ArrayDecimal, el => el == 0);
        }

        public static bool operator !(VectorDecimal vector)
        {
            return vector.num == 0 || !Array.TrueForAll(vector.ArrayDecimal, el => el == 0);
        }

        public static VectorDecimal operator ~(VectorDecimal vector)
        {
            for (uint i = 0; i < vector.num; i++)
            {
                vector.ArrayDecimal[i] = Math.Floor(vector.ArrayDecimal[i]);
            }
            return vector;
        }

        public static VectorDecimal operator ++(VectorDecimal vector)
        {
            for (uint i = 0; i < vector.num; i++)
            {
                vector.ArrayDecimal[i]++;
            }

            return vector;
        }

        public static VectorDecimal operator --(VectorDecimal vector)
        {
            for (uint i = 0; i < vector.num; i++)
            {
                vector.ArrayDecimal[i]--;
            }

            return vector;
        }

        public static VectorDecimal operator +(VectorDecimal vector1, VectorDecimal vector2)
        {
            uint maxLength = Math.Max(vector1.num, vector2.num);
            decimal[] result = new decimal[maxLength];

            for (uint i = 0; i < maxLength; i++)
            {
                decimal value1 = (i < vector1.num) ? vector1.ArrayDecimal[i] : 0;
                decimal value2 = (i < vector2.num) ? vector2.ArrayDecimal[i] : 0;

                result[i] = value1 + value2;
            }

            return new VectorDecimal((uint)maxLength, 0) { ArrayDecimal = result };
        }

        public static VectorDecimal operator +(VectorDecimal vector, decimal scalar)
        {
            decimal[] result = new decimal[vector.num];

            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = vector.ArrayDecimal[i] + scalar;
            }

            return new VectorDecimal(vector.num, 0) { ArrayDecimal = result };
        }

        public static VectorDecimal operator -(VectorDecimal vector1, VectorDecimal vector2)
        {
            uint maxLength = Math.Max(vector1.num, vector2.num);
            decimal[] result = new decimal[maxLength];

            for (uint i = 0; i < maxLength; i++)
            {
                decimal value1 = (i < vector1.num) ? vector1.ArrayDecimal[i] : 0;
                decimal value2 = (i < vector2.num) ? vector2.ArrayDecimal[i] : 0;

                result[i] = value1 - value2;
            }

            return new VectorDecimal(maxLength, 0) { ArrayDecimal = result };
        }

        public static VectorDecimal operator -(VectorDecimal vector, decimal scalar)
        {
            decimal[] result = new decimal[vector.num];

            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = vector.ArrayDecimal[i] - scalar;
            }

            return new VectorDecimal(vector.num, 0) { ArrayDecimal = result };
        }

        public static VectorDecimal operator *(VectorDecimal vector, decimal scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = vector[i] * scalar;
            }
            return result;
        }

        public static VectorDecimal operator *(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
            {
                throw new ArgumentException(
                    "Vector sizes must be equal for element-wise multiplication."
                );
            }

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                result[i] = vector1[i] * vector2[i];
            }
            return result;
        }

        public static VectorDecimal operator /(VectorDecimal vector, decimal scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero.");
            }

            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = vector[i] / scalar;
            }
            return result;
        }

        public static VectorDecimal operator /(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
            {
                throw new ArgumentException(
                    "Vector sizes must be equal for element-wise division."
                );
            }

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                if (vector2[i] == 0)
                {
                    throw new DivideByZeroException("Cannot divide by zero.");
                }
                result[i] = vector1[i] / vector2[i];
            }
            return result;
        }

        public static VectorDecimal operator %(VectorDecimal vector, decimal scalar)
        {
            if (scalar == 0)
            {
                throw new DivideByZeroException("Cannot find remainder by zero.");
            }

            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = vector[i] % scalar;
            }
            return result;
        }

        public static VectorDecimal operator %(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
            {
                throw new ArgumentException(
                    "Vector sizes must be equal for element-wise remainder."
                );
            }

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                if (vector2[i] == 0)
                {
                    throw new DivideByZeroException("Cannot find remainder by zero.");
                }
                result[i] = vector1[i] % vector2[i];
            }
            return result;
        }

        public static VectorDecimal operator |(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException("Vector sizes must be equal for bitwise OR operation.");

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                int intResult = (int)vector1[i] | (int)vector2[i];
                result[i] = (decimal)intResult;
            }
            return result;
        }

        public static VectorDecimal operator |(VectorDecimal vector, byte scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                int intResult = (int)vector[i] | scalar;
                result[i] = (decimal)intResult;
            }
            return result;
        }

        public static VectorDecimal operator ^(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException(
                    "Vector sizes must be equal for bitwise XOR operation."
                );

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                int intResult = (int)vector1[i] ^ (int)vector2[i];
                result[i] = (decimal)intResult;
            }
            return result;
        }

        public static VectorDecimal operator ^(VectorDecimal vector, byte scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = (int)vector[i] ^ scalar;
            }
            return result;
        }

        public static VectorDecimal operator &(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException(
                    "Vector sizes must be equal for bitwise AND operation."
                );

            VectorDecimal result = new VectorDecimal(vector1.num);
            for (uint i = 0; i < result.num; i++)
            {
                int intResult = (int)vector1[i] & (int)vector2[i];
                result[i] = (decimal)intResult;
            }
            return result;
        }

        public static VectorDecimal operator &(VectorDecimal vector, byte scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = (int)vector[i] & scalar;
            }
            return result;
        }

        public static VectorDecimal operator >>(VectorDecimal vector1, VectorDecimal vector2)
        {
            uint maxLength = Math.Max(vector1.num, vector2.num);
            VectorDecimal result = new VectorDecimal((uint)maxLength);

            for (uint i = 0; i < maxLength; i++)
            {
                decimal value1 = (i < vector1.num) ? vector1[i] : 0;
                decimal value2 = (i < vector2.num) ? vector2[i] : 0;
                result[i] = (uint)value1 >> (int)value2;
            }

            return result;
        }

        public static VectorDecimal operator >>(VectorDecimal vector, uint scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = (uint)vector[i] >> (int)scalar;
            }
            return result;
        }

        public static VectorDecimal operator <<(VectorDecimal vector1, VectorDecimal vector2)
        {
            uint maxLength = Math.Max(vector1.num, vector2.num);
            VectorDecimal result = new VectorDecimal((uint)maxLength);

            for (uint i = 0; i < maxLength; i++)
            {
                decimal value1 = (i < vector1.num) ? vector1[i] : 0;
                decimal value2 = (i < vector2.num) ? vector2[i] : 0;
                result[i] = (uint)value1 << (int)value2;
            }

            return result;
        }

        public static VectorDecimal operator <<(VectorDecimal vector, uint scalar)
        {
            VectorDecimal result = new VectorDecimal(vector.num);
            for (uint i = 0; i < vector.num; i++)
            {
                result[i] = (uint)vector[i] << (int)scalar;
            }
            return result;
        }

        public static bool operator ==(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (ReferenceEquals(vector1, null) && ReferenceEquals(vector2, null))
                return true;

            if (ReferenceEquals(vector1, null) || ReferenceEquals(vector2, null))
                return false;

            if (vector1.num != vector2.num)
                return false;

            for (uint i = 0; i < vector1.num; i++)
            {
                if (vector1[i] != vector2[i])
                    return false;
            }

            return true;
        }

        public static bool operator !=(VectorDecimal vector1, VectorDecimal vector2)
        {
            return !(vector1 == vector2);
        }

        public static bool operator >(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException("Vector sizes must be equal for comparison.");

            for (uint i = 0; i < vector1.num; i++)
            {
                if (vector1[i] <= vector2[i])
                    return false;
            }

            return true;
        }

        public static bool operator >=(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException("Vector sizes must be equal for comparison.");

            for (uint i = 0; i < vector1.num; i++)
            {
                if (vector1[i] < vector2[i])
                    return false;
            }

            return true;
        }

        public static bool operator <(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException("Vector sizes must be equal for comparison.");

            for (uint i = 0; i < vector1.num; i++)
            {
                if (vector1[i] >= vector2[i])
                    return false;
            }

            return true;
        }

        public static bool operator <=(VectorDecimal vector1, VectorDecimal vector2)
        {
            if (vector1.num != vector2.num)
                throw new ArgumentException("Vector sizes must be equal for comparison.");

            for (uint i = 0; i < vector1.num; i++)
            {
                if (vector1[i] > vector2[i])
                    return false;
            }

            return true;
        }

        public override string ToString()
        {
            return $"VectorDecimal ({num} elements)";
        }
    }
}

public class Task2
{
    public static void Task()
    {
        try
        {
            // Vectors using different constructors
            VectorDecimal vector1 = new();
            VectorDecimal vector2 = new(3);
            VectorDecimal vector3 = new(3, 42);

            // Number of vectors
            Console.WriteLine($"Number of vectors: {VectorDecimal.CountVectors()}");

            // Inputting elements for the second vector
            vector2.Input();

            // Printing elements of the second vector
            vector2.Display();

            // Setting all elements of the third vector to 99
            vector3.AssignValue(99);

            // Printing elements of the third vector
            vector3.Display();

            // Testing unary operations
            Console.WriteLine("Testing Unary Operations:");
            Console.WriteLine($"Original Vector 1: {vector1[0]}");
            vector1++;
            Console.WriteLine($"After ++ operation: {vector1[0]}");
            vector1--;
            Console.WriteLine($"After -- operation: {vector1[0]}");

            // Testing logical NOT operator
            Console.WriteLine($"Vector 3 is not empty: {!vector3}");

            // Testing bitwise NOT operator
            Console.WriteLine("Testing Bitwise NOT Operator:");
            Console.WriteLine("Original Vector 2:");
            vector2.Display();
            VectorDecimal notVector2 = ~vector2;
            Console.WriteLine("After ~ operation:");
            notVector2.Display();

            // Testing binary operations
            Console.WriteLine("Testing Binary Operations:");

            Console.WriteLine("Vector 2 + Vector 3:");
            VectorDecimal sumVector = vector2 + vector3;
            sumVector.Display();

            Console.WriteLine("Vector 3 - Vector 2:");
            VectorDecimal subVector = vector3 - vector2;
            subVector.Display();

            Console.WriteLine("Vector 2 * Vector 3:");
            VectorDecimal mulVector = vector2 * vector3;
            mulVector.Display();

            Console.WriteLine("Vector 3 / Vector 2:");
            VectorDecimal divVector = vector3 / vector2;
            divVector.Display();

            Console.WriteLine("Vector 3 % Vector 2:");
            VectorDecimal modVector = vector3 % vector2;
            modVector.Display();

            Console.WriteLine("Vector 2 | Vector 3:");
            VectorDecimal orVector = vector2 | vector3;
            orVector.Display();

            Console.WriteLine("Vector 2 ^ Vector 3:");
            VectorDecimal xorVector = vector2 ^ vector3;
            xorVector.Display();

            Console.WriteLine("Vector 2 & Vector 3:");
            VectorDecimal andVector = vector2 & vector3;
            andVector.Display();

            Console.WriteLine("Vector 2 >> 1:");
            VectorDecimal rightShiftVector = vector2 >> 1;
            rightShiftVector.Display();

            Console.WriteLine("Vector 3 << 2:");
            VectorDecimal leftShiftVector = vector3 << 2;
            leftShiftVector.Display();

            // Testing comparison operators
            Console.WriteLine("Testing Comparison Operators:");

            Console.WriteLine($"Vector 2 == Vector 3: {vector2 == vector3}");

            Console.WriteLine($"Vector 2 != Vector 3: {vector2 != vector3}");

            Console.WriteLine($"Vector 2 > Vector 3: {vector2 > vector3}");

            Console.WriteLine($"Vector 2 >= Vector 3: {vector2 >= vector3}");

            Console.WriteLine($"Vector 2 < Vector 3: {vector2 < vector3}");

            Console.WriteLine($"Vector 2 <= Vector 3: {vector2 <= vector3}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
