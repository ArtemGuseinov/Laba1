using System;

namespace Lab3
{
    public class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            double a = 0.1;
            double b = 1.0;
            int n = 10;
            int points = 10;
            double e = 0.0001;

            PrintTable(a, b, n, points, e);
        }

        private static void PrintTable(double a, double b, int n, int points, double e)
        {
            double step = (b - a) / (points - 1);

            Console.WriteLine("Вычисление функции y = cos(x)");
            Console.WriteLine("n = {0}, точность = {1}", n, e);
            Console.WriteLine();

            for (int i = 0; i < points; i++)
            {
                double x = a + i * step;
                double sn = GetSum(x, n);
                double se = GetSumPrecision(x, e);
                double y = Math.Cos(x);

                PrintRow(x, sn, se, y);
            }
        }

        private static void PrintRow(double x, double sn, double se, double y)
        {
            Console.WriteLine("X={0,-10:F2} SN={1,-14:F8} SE={2,-14:F8} Y={3,-14:F8}", x, sn, se, y);
        }

        private static double GetSum(double x, int n)
        {
            double term = 1.0;
            double sum = term;

            for (int i = 1; i <= n; i++)
            {
                term *= -(x * x) / ((2 * i - 1) * (2 * i));
                sum += term;
            }

            return sum;
        }

        private static double GetSumPrecision(double x, double e)
        {
            double term = 1.0;
            double sum = term;
            int i = 0;

            while (Math.Abs(term) >= e)
            {
                i++;
                term *= -(x * x) / ((2 * i - 1) * (2 * i));
                sum += term;
            }

            return sum;
        }
    }
}
