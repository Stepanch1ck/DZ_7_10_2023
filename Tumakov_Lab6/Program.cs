using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace Tumakov_Lab6
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Тумаков лвбораторная 6: Упражнение 6.1 Подсчет гласных и согласных в файле");
            string filename = "test.txt";

            string file = File.ReadAllText(filename);
            char[] filecontent = file.ToCharArray();
            CountChars(filecontent);
            Console.WriteLine("Нажмите enter");
            Console.ReadKey();
            Console.WriteLine("Упражнение 6.2: Вывод матриц и перемножение");
            int[,] matrix1 = {
                {1, 2},
                {3, 4}
            };

            int[,] matrix2 = {
                {5, 6},
                {7, 8}
            };
            Console.WriteLine("Матрица 1:");
            PrintMatrix(matrix1);

            Console.WriteLine("Матрица 2:");
            PrintMatrix(matrix2);

            Console.WriteLine("Результат умножения матриц:");
            PrintMatrix(MultiplyMatrices(matrix1, matrix2));
            Console.WriteLine("Нажмите enter");
            Console.ReadKey();
            Console.WriteLine("Упражнение 6.3: Средняя температура по месяцам");
            int[,] temperatures = new int[12, 30];
            Random rand = new Random();
            for (int month = 0; month < 12; month++)
            {
                for (int day = 0; day < 30; day++)
                {
                    temperatures[month, day] = rand.Next(-40, 40);
                }
            }
            double[] averageTemperatures = CalculateAverageTemperatures(temperatures);
            Array.Sort(averageTemperatures);
            for (int i = 0; i < averageTemperatures.Length; i++)
            {
                Console.WriteLine($"Средняя температура за месяц {i + 1}: {averageTemperatures[i]}");
            }
            Console.WriteLine("Нажмите enter");
            Console.ReadKey();

            Console.WriteLine("Домашнее задание 6.1: Упражнение 6.1 выполнить с помощью коллекции List<T>.");
            List<char> charList = ReadFileToList(filename);
            CountChars(charList);
            Console.WriteLine("Нажмите enter");
            Console.ReadKey();

            Console.WriteLine("Домашнее задание 6.2: Упражнение 6.2 выполнить с помощью коллекций\r\nLinkedList<LinkedList<T>>. ");
            LinkedList<LinkedList<int>> matrix11 = new LinkedList<LinkedList<int>>();
            LinkedList<LinkedList<int>> matrix22 = new LinkedList<LinkedList<int>>();
            LinkedList<LinkedList<int>> resultMatrix = MultiplyMatrices(matrix11, matrix22);
            for (int i = 0; i < 3; i++)
            {
                LinkedList<int> row = new LinkedList<int>();
                for (int j = 0; j < 3; j++)
                {
                    row.AddLast(rand.Next(-10, 10));
                }
                matrix11.AddLast(row);
            }

            for (int i = 0; i < 3; i++)
            {
                LinkedList<int> row = new LinkedList<int>();
                for (int j = 0; j < 3; j++)
                {
                    row.AddLast(rand.Next(-10, 10));
                }
                matrix22.AddLast(row);
            }
            Console.WriteLine("Матрица 1:");
            PrintMatrix(matrix11);
            Console.WriteLine("Матрица 2:");
            PrintMatrix(matrix22);
            Console.WriteLine("Результат умножения матриц:");
            PrintMatrix(resultMatrix);
            Console.WriteLine("Нажмите enter");
            Console.ReadKey();
            Console.WriteLine("Домашнее задание 6.3 Написать программу для упражнения 6.3, использовав класс\r\nDictionary<TKey, TValue>. В качестве ключей выбрать строки – названия месяцев, а в\r\nкачестве значений – массив значений температур по дням.");
            Dictionary<string, int[]> temperatureData = GenerateRandomTemperatures();

            Dictionary<string, double> averageTemperature = CalculateAverageTemperatures(temperatureData);

            Console.WriteLine("Средние температуры по месяцам:");
            foreach (var i in averageTemperature)
            {
                Console.WriteLine($"{i.Key}: {i.Value:F2}");
            }

            Console.WriteLine("Нажмите enter");
            Console.ReadKey();
        }
        // метод первого задания, на вход массив из символов строки файла
        static void CountChars(char[] chars)
        {
            int vowelsCount = 0;
            int consonantsCount = 0;
            foreach (char c in chars)
            {
                if (char.IsLetter(c))
                {
                    char lowerCase = char.ToLower(c);
                    if ("ауеёиыэяю".Contains(lowerCase))
                    {
                        vowelsCount++;
                    }
                    else
                    {
                        consonantsCount++;
                    }
                }
            }
            Console.WriteLine("Количество гласных букв: {0}", vowelsCount);
            Console.WriteLine("Количество согласных букв: {0}", consonantsCount);
        }
        // Второе задание, метод ввода матриц
        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        // Метод второго задания для перемножения матриц
        static int[,] MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);

            if (columns1 != rows2)
            {
                throw new ArgumentException("Нельзя умножить матрицы с данными размерами.");
            }
            int[,] resultMatrix = new int[rows1, columns2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < columns1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    resultMatrix[i, j] = sum;
                }
            }
            return resultMatrix;

        }
        //Метод для третьего задания для поиска средней температуры для каждого месяца
        static double[] CalculateAverageTemperatures(int[,] temperatures)
        {
            double[] averageTemperatures = new double[12];
            for (int month = 0; month < 12; month++)
            {
                double sum = 0;
                for (int day = 0; day < 30; day++)
                {
                    sum += temperatures[month, day];
                }

                averageTemperatures[month] = sum / 30;
            }
            return averageTemperatures;
        }
        // метод для 4 задания с использованием List
        static List<char> ReadFileToList(string fileName)
        {
            List<char> charList = new List<char>();

            using (StreamReader reader = new StreamReader(fileName))
            {
                int c;
                while ((c = reader.Read()) != -1)
                {
                    charList.Add((char)c);
                }
            }

            return charList;
        }
        // тоже самое что для 1 задания, только теперь с List
        static void CountChars(List<char> charList)
        {
            int vowelCount = 0;
            int consonantCount = 0;

            foreach (char c in charList)
            {
                if (char.IsLetter(c))
                {
                    char lowerCase = char.ToLower(c);
                    if ("ауеёиыэяю".Contains(lowerCase))
                    {
                        vowelCount++;
                    }
                    else
                    {
                        consonantCount++;
                    }
                }
            }
            Console.WriteLine("Количество гласных букв: " + vowelCount);
            Console.WriteLine("Количество согласных букв: " + consonantCount);
        }
        // вывод матрицы с LinkedList
        static void PrintMatrix(LinkedList<LinkedList<int>> matrix)
        {
            foreach (var row in matrix)
            {
                foreach (int value in row)
                {
                    Console.Write(value + "\t");
                }
                Console.WriteLine();
            }
        }
        // Умножение матриц в виде Linkedlist
        static LinkedList<LinkedList<int>> MultiplyMatrices(LinkedList<LinkedList<int>> matrix11, LinkedList<LinkedList<int>> matrix22)
        {
            int rows1 = matrix11.Count;
            int columns1 = matrix11.First.Value.Count;
            int rows2 = matrix22.Count;
            int columns2 = matrix22.First.Value.Count;

            if (columns1 != rows2)
            {
                throw new ArgumentException("Нельзя умножить матрицы с данными размерами.");
            }
            LinkedList<LinkedList<int>> resultMatrix = new LinkedList<LinkedList<int>>();
            for (int i = 0; i < rows1; i++)
            {
                resultMatrix.AddLast(new LinkedList<int>());
            }
            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    int sum = 0;
                    for (int k = 0; k < columns1; k++)
                    {
                        sum += matrix11.ElementAt(i).ElementAt(k) * matrix22.ElementAt(k).ElementAt(j);
                    }
                    resultMatrix.ElementAt(i).AddLast(sum);
                }
            }
            return resultMatrix;
        }
        // Генерация словоря стемпературой
        static Dictionary<string, int[]> GenerateRandomTemperatures()
        {
            Dictionary<string, int[]> temperatureData = new Dictionary<string, int[]>();
            Random rand = new Random();

            string[] months = {
            "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь",
            "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь"
        };

            foreach (string month in months)
            {
                int[] monthTemperatures = new int[30];
                for (int i = 0; i < 30; i++)
                {
                    monthTemperatures[i] = rand.Next(-20, 41);
                }

                temperatureData.Add(month, monthTemperatures);
            }

            return temperatureData;
        }
        // поиск средней температуры
        static Dictionary<string, double> CalculateAverageTemperatures(Dictionary<string, int[]> temperatureData)
        {
            Dictionary<string, double> averageTemperatures = new Dictionary<string, double>();

            foreach (var kvp in temperatureData)
            {
                double sum = 0;
                foreach (int temp in kvp.Value)
                {
                    sum += temp;
                }

                double averageTemp = sum / kvp.Value.Length;
                averageTemperatures.Add(kvp.Key, averageTemp);
            }

            return averageTemperatures;
        }
    }
}
