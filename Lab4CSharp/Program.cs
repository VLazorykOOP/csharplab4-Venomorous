using System;
using MatrixDecimal;
using MyVectorDecimal;
using Triangle;

class Program
{
    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("=========================================================");
            Console.WriteLine("Select a Task:");
            Console.WriteLine("1. Task 1");
            Console.WriteLine("2. Task 2");
            Console.WriteLine("3. Task 3");
            Console.WriteLine("4. Exit");

            Console.Write("Enter your choice: ");
            string? choice = Console.ReadLine();
            Console.WriteLine("---------------------------------------------------------");

            switch (choice)
            {
                case "1":
                    Task1.Task();
                    break;

                case "2":
                    Task2.Task();
                    break;

                case "3":
                    Task3.Task();
                    break;

                case "4":
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please enter a valid option.");
                    break;
            }
        }
    }
}
