
namespace HomeWork
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Фибоначчи
            int[] massFIB = new int[8] { 0, 1, 1, 2, 3, 5, 8, 13 };
            //Месяцы
            string[] massMOT = new string[12] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December", };
            //Matrix
            int[,] matrix = new int[3, 3];
            matrix[0, 0] = 2;
            matrix[0, 1] = 3;
            matrix[0, 2] = 4;

            matrix[1, 0] = 4;
            matrix[1, 1] = 9;
            matrix[1, 2] = 16;

            matrix[2, 0] = 8;
            matrix[2, 1] = 27;
            matrix[2, 2] = 64;

            //Jagged array
            double[][] jaggedArray = new double[3][];

            jaggedArray[0] = new double[] { 1.0, 2.0, 3.0, 4.0, 5.0 };

            jaggedArray[1] = new double[] { Math.E, Math.PI };

            jaggedArray[2] = new double[]
            {
            Math.Log10(1),  
            Math.Log10(10),  
            Math.Log10(100), 
            Math.Log10(1000) 
            };

            // массивы для заданий 5 и 6.
            int[] array = { 1, 2, 3, 4, 5 };
            int[] array2 = { 7, 8, 9, 10, 11, 12, 13 };

            //5
            Array.Copy(array, 0, array2, 0, 3);

            Console.WriteLine("array: " + string.Join(", ", array));
            Console.WriteLine("array2: " + string.Join(", ", array2));

            //6

            Array.Resize(ref array, 10);
            Console.WriteLine("array: " + string.Join(", ", array));

        }
    }
}