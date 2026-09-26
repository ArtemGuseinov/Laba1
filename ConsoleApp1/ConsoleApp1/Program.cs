using System;

namespace laba1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Задача 1: ++ и --
            if (!TryReadInt("n", out var n))
            {
                return;
            }

            if (!TryReadInt("m", out var m))
            {
                return;
            }

            Console.WriteLine("n={0}", n);
            Console.WriteLine("m={0}", m);

            int sum = m + --n;
            Console.WriteLine("m + --n = {0}, m = {1}, n = {2}", sum, m, n);

            bool isLess = m++ < --n;
            Console.WriteLine("m++ < --n = {0}, m = {1}, n = {2}", isLess, m, n);

            bool isGreater = --m > n--;
            Console.WriteLine("--m > n-- = {0}, m = {1}, n = {2}", isGreater, m, n);

            // f(x) = корень 5-й степени из (x³ + x⁴) + ctg(arctg(x²))
            Console.WriteLine();
            if (!TryReadDouble("x", out var x))
            {
                return;
            }

            if (x == 0)
            {
                Console.WriteLine("Нельзя вычислить: ctg(arctg(x^2)) не определён при x = 0");
            }
            else
            {
                double root = GetFifthRoot(Math.Pow(x, 3) + Math.Pow(x, 4));
                double cotangent = 1 / Math.Tan(Math.Atan(x * x));
                Console.WriteLine("Значение выражения = {0}", root + cotangent);
            }

            // Точка в области
            Console.WriteLine();
            if (!TryReadDouble("x1", out var x1))
            {
                return;
            }

            if (!TryReadDouble("y1", out var y1))
            {
                return;
            }

            bool isInsideCircle = x1 * x1 + y1 * y1 <= 4;
            bool isOutsideSquare = Math.Abs(x1) + Math.Abs(y1) >= 2;
            bool isInArea = isInsideCircle && isOutsideSquare;
            Console.WriteLine("Точка принадлежит области: {0}", isInArea);

            // Задача 3: double и float
            Console.WriteLine();
            double a = 1000.0;
            double b = 0.0001;

            double difference = a - b;
            double differenceCubed = Math.Pow(difference, 3);
            double aCubed = Math.Pow(a, 3);
            double numerator = differenceCubed - aCubed;

            double aSquared = Math.Pow(a, 2);
            double bSquared = Math.Pow(b, 2);
            double bCubed = Math.Pow(b, 3);
            double denominator = -bCubed + 3 * a * bSquared - 3 * aSquared * b;

            if (denominator == 0)
            {
                Console.WriteLine("Нельзя вычислить (double): деление на ноль");
            }
            else
            {
                double doubleResult = numerator / denominator;
                Console.WriteLine("Результат double = {0}", doubleResult);
            }

            float floatA = 1000f;
            float floatB = 0.0001f;

            float floatDifference = floatA - floatB;
            float floatDifferenceCubed = (float)Math.Pow(floatDifference, 3);
            float floatACubed = (float)Math.Pow(floatA, 3);
            float floatNumerator = floatDifferenceCubed - floatACubed;

            float floatASquared = (float)Math.Pow(floatA, 2);
            float floatBSquared = (float)Math.Pow(floatB, 2);
            float floatBCubed = (float)Math.Pow(floatB, 3);
            float floatDenominator = -floatBCubed + 3 * floatA * floatBSquared - 3 * floatASquared * floatB;

            if (floatDenominator == 0)
            {
                Console.WriteLine("Нельзя вычислить (float): деление на ноль");
            }
            else
            {
                float floatResult = floatNumerator / floatDenominator;
                Console.WriteLine("Результат float = {0}", floatResult);
            }

            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }

        private static bool TryReadInt(string name, out int value)
        {
            Console.Write("Введите {0}: ", name);
            var buf = Console.ReadLine();
            if (int.TryParse(buf, out value))
            {
                return true;
            }
            Console.WriteLine("Некорректный ввод {0}", name);
            return false;
        }

        private static bool TryReadDouble(string name, out double value)
        {
            Console.Write("Введите {0}: ", name);
            var buf = Console.ReadLine();
            if (double.TryParse(buf, out value))
            {
                return true;
            }
            Console.WriteLine("Некорректный ввод {0}", name);
            return false;
        }

        private static double GetFifthRoot(double value)
        {
            if (value < 0)
            {
                return -Math.Pow(-value, 1.0 / 5);
            }
            return Math.Pow(value, 1.0 / 5);
        }
    }
}