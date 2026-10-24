using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Lab5_6
{
    public class Program
    {
        private static readonly Random random = new Random();

        public static void Main()
        {
            try
            {
                ShowMainMenu();
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("Ввод завершён.");
            }
        }

        private static void ShowMainMenu()
        {
            int[,] matrix = null;
            int[][] rows = null;

            while (true)
            {
                Console.WriteLine("Лабораторная работа №5–6. Вариант 7");
                Console.WriteLine("1. Двумерный массив");
                Console.WriteLine("2. Рваный массив");
                Console.WriteLine("3. Обработка строки");
                Console.WriteLine("0. Выход");

                int choice = ReadInt("Выберите задание: ", 0, 3);
                switch (choice)
                {
                    case 1:
                        matrix = ShowMatrixMenu(matrix);
                        break;
                    case 2:
                        rows = ShowJaggedMenu(rows);
                        break;
                    case 3:
                        ShowStringMenu();
                        break;
                    case 0:
                        return;
                }
            }
        }

        private static int[,] ShowMatrixMenu(int[,] matrix)
        {
            while (true)
            {
                Console.WriteLine("Двумерный массив");
                PrintArrayMenu("Добавить K строк в конец");
                int choice = ReadInt("Выберите действие: ", 0, 4);

                switch (choice)
                {
                    case 1:
                    case 2:
                        matrix = ReadMatrix(choice == 2);
                        PrintMatrix(matrix);
                        break;
                    case 3:
                        PrintMatrix(matrix);
                        break;
                    case 4:
                        matrix = ReadExtraRows(matrix);
                        break;
                    case 0:
                        return matrix;
                }
            }
        }

        private static int[][] ShowJaggedMenu(int[][] rows)
        {
            while (true)
            {
                Console.WriteLine("Рваный массив");
                PrintArrayMenu("Удалить первую строку с нулём");
                int choice = ReadInt("Выберите действие: ", 0, 4);

                switch (choice)
                {
                    case 1:
                    case 2:
                        rows = ReadJaggedRows(choice == 2);
                        PrintMatrix(rows);
                        break;
                    case 3:
                        PrintMatrix(rows);
                        break;
                    case 4:
                        rows = ShowRemovedRow(rows);
                        break;
                    case 0:
                        return rows;
                }
            }
        }

        private static void PrintArrayMenu(string operation)
        {
            Console.WriteLine("1. Создать массив вручную");
            Console.WriteLine("2. Создать массив случайно");
            Console.WriteLine("3. Вывести массив");
            Console.WriteLine($"4. {operation}");
            Console.WriteLine("0. Назад");
        }

        private static int[,] ReadMatrix(bool isRandom)
        {
            int rowCount = ReadInt("Количество строк (1–100): ", 1, 100);
            int columnCount = ReadInt("Количество столбцов (1–100): ", 1, 100);
            int[,] matrix = new int[rowCount, columnCount];

            for (int row = 0; row < rowCount; row++)
            {
                int[] values = ReadRow(row, columnCount, isRandom);
                for (int column = 0; column < columnCount; column++)
                {
                    matrix[row, column] = values[column];
                }
            }

            return matrix;
        }

        private static int[][] ReadJaggedRows(bool isRandom)
        {
            int rowCount = ReadInt("Количество строк (1–100): ", 1, 100);
            int[][] rows = new int[rowCount][];

            for (int row = 0; row < rowCount; row++)
            {
                int length = ReadInt($"Количество элементов в строке {row + 1} (1–100): ", 1, 100);
                rows[row] = ReadRow(row, length, isRandom);
            }

            return rows;
        }

        private static int[] ReadRow(int row, int length, bool isRandom)
        {
            int[] values = new int[length];
            for (int column = 0; column < length; column++)
            {
                values[column] = isRandom
                    ? random.Next(-9, 10)
                    : ReadInt($"Элемент [{row + 1}, {column + 1}]: ");
            }

            return values;
        }

        private static int[,] ReadExtraRows(int[,] matrix)
        {
            if (matrix == null || matrix.Length == 0)
            {
                Console.WriteLine("Сначала создайте непустой двумерный массив.");
                return matrix;
            }

            int count = ReadInt("Сколько строк K добавить (1–100): ", 1, 100);
            int columnCount = matrix.GetLength(1);
            int[,] extraRows = new int[count, columnCount];

            for (int row = 0; row < count; row++)
            {
                int[] values = ReadRow(matrix.GetLength(0) + row, columnCount, false);
                for (int column = 0; column < columnCount; column++)
                {
                    extraRows[row, column] = values[column];
                }
            }

            int[,] result = AddRows(matrix, extraRows);
            Console.WriteLine("Строки добавлены.");
            PrintMatrix(result);
            return result;
        }

        private static int[,] AddRows(int[,] matrix, int[,] extraRows)
        {
            int rowCount = matrix.GetLength(0);
            int columnCount = matrix.GetLength(1);
            int extraCount = extraRows.GetLength(0);
            int[,] result = new int[rowCount + extraCount, columnCount];

            Array.Copy(matrix, 0, result, 0, matrix.Length);
            Array.Copy(extraRows, 0, result, matrix.Length, extraRows.Length);
            return result;
        }

        private static int[][] ShowRemovedRow(int[][] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                Console.WriteLine("Массив пуст. Сначала создайте его.");
                return rows;
            }

            int index = FindZeroRow(rows);
            if (index == -1)
            {
                Console.WriteLine("Строк с нулевыми элементами нет.");
                return rows;
            }

            int[][] result = RemoveRow(rows, index);
            Console.WriteLine($"Удалена первая строка с нулём: номер {index + 1}.");
            PrintMatrix(result);
            return result;
        }

        private static int FindZeroRow(int[][] rows)
        {
            for (int row = 0; row < rows.Length; row++)
            {
                if (Array.IndexOf(rows[row], 0) >= 0)
                {
                    return row;
                }
            }

            return -1;
        }

        private static int[][] RemoveRow(int[][] rows, int index)
        {
            int[][] result = new int[rows.Length - 1][];
            Array.Copy(rows, 0, result, 0, index);
            Array.Copy(rows, index + 1, result, index, rows.Length - index - 1);
            return result;
        }

        private static void PrintMatrix(int[,] matrix)
        {
            if (matrix == null || matrix.Length == 0)
            {
                Console.WriteLine("Массив пуст. Сначала создайте его.");
                return;
            }

            for (int row = 0; row < matrix.GetLength(0); row++)
            {
                for (int column = 0; column < matrix.GetLength(1); column++)
                {
                    Console.Write($"{matrix[row, column],12} ");
                }

                Console.WriteLine();
            }
        }

        private static void PrintMatrix(int[][] rows)
        {
            if (rows == null || rows.Length == 0)
            {
                Console.WriteLine("Массив пуст. Сначала создайте его.");
                return;
            }

            foreach (int[] row in rows)
            {
                Console.WriteLine(string.Join(" ", row));
            }
        }

        private static void ShowStringMenu()
        {
            while (true)
            {
                Console.WriteLine("Обработка строки");
                Console.WriteLine("1. Ввести строку с клавиатуры");
                Console.WriteLine("2. Выбрать готовую строку");
                Console.WriteLine("0. Назад");
                int choice = ReadInt("Выберите действие: ", 0, 2);
                if (choice == 0)
                {
                    return;
                }

                string source = choice == 1 ? ReadString() : ReadSample();
                if (string.IsNullOrWhiteSpace(source))
                {
                    Console.WriteLine("Строка пустая.");
                }
                else
                {
                    Console.WriteLine($"Исходная строка: {source}");
                    Console.WriteLine($"Результат обработки: {ShiftWords(source)}");
                }
            }
        }

        private static string ReadString()
        {
            Console.Write("Введите строку: ");
            string source = Console.ReadLine();
            if (source == null)
            {
                throw new EndOfStreamException();
            }

            return source;
        }

        private static string ReadSample()
        {
            string[] samples =
            {
                "В лесу родилась елочка. В лесу она росла. Зимой и летом стройная, зеленая была.",
                "abc,  defg; hijkl: mnopqr! stu vwxy?",
                "А я! Дом."
            };

            for (int index = 0; index < samples.Length; index++)
            {
                Console.WriteLine($"{index + 1}. {samples[index]}");
            }

            int choice = ReadInt("Номер строки: ", 1, samples.Length);
            return samples[choice - 1];
        }

        private static string ShiftWords(string source)
        {
            return Regex.Replace(source, @"[^.!?]*[.!?]+|[^.!?]+$", sentence =>
            {
                int number = 0;
                return Regex.Replace(sentence.Value, @"[\p{L}\p{N}_]+", word =>
                {
                    number++;
                    return RotateWord(word.Value, number);
                });
            });
        }

        private static string RotateWord(string word, int shift)
        {
            int offset = shift % word.Length;
            return word.Substring(offset) + word.Substring(0, offset);
        }

        private static int ReadInt(string prompt, int minimum = int.MinValue, int maximum = int.MaxValue)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (input == null)
                {
                    throw new EndOfStreamException();
                }

                if (int.TryParse(input, out int value)
                    && value >= minimum && value <= maximum)
                {
                    return value;
                }

                Console.WriteLine($"Ошибка: введите целое число от {minimum} до {maximum}.");
            }
        }
    }
}
