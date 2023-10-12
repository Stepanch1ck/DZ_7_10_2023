using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DZ_2
{
    class Grandma
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Disease { get; set; }
        public string Medicine { get; set; }

        public List<string> Diseases
        {
            get
            {
                List<string> diseases = new List<string>();
                diseases.Add(this.Disease);
                return diseases;
            }
        }

        public Grandma(string name, int age, string disease, string medicine)
        {
            this.Name = name;
            this.Age = age;
            this.Disease = disease;
            this.Medicine = medicine;
        }

        public override string ToString()
        {
            return $"{this.Name} {this.Age} {this.Disease} {this.Medicine}";
        }
    }
    class Hospital
    {
        public string Name { get; set; }
        public List<string> Diseases { get; set; }
        public int Capacity { get; set; }
        public int CurrentCount { get; set; }

        public Hospital(string name, List<string> diseases, int capacity)
        {
            this.Name = name;
            this.Diseases = diseases;
            this.Capacity = capacity;
            this.CurrentCount = 0;
        }
        public void AddGrandma(Grandma grandma)
        {
            this.CurrentCount++;
        }

        public List<Grandma> Treats(List<string> diseases, Grandma grandma)
        {
            List<Grandma> treatedGrandmas = new List<Grandma>();
            foreach (string disease in diseases)
            {
                if (this.Diseases.Contains(disease))
                {
                    treatedGrandmas.Add(grandma);
                }
            }
            return treatedGrandmas;
        }

        public double GetFillingPercentage()
        {
            double fillingPercentage = (double)this.CurrentCount / this.Capacity * 100;
            return fillingPercentage;
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.CurrentCount}/{this.Capacity}) - {this.GetFillingPercentage()}%";
        }
    }


    public class Student
    {
        public string FullName { get; set; }
        public int YearOfBirth { get; set; }
        public string Exam { get; set; }
        public int Score { get; set; }
        public Student(string surname, string name, int yearOfBirth, string exam, int score)
        {
            FullName = surname + " " + name;
            YearOfBirth = yearOfBirth;
            Exam = exam;
            Score = score;
        }
        public override string ToString()
        {
            return $"{FullName} - {YearOfBirth} - {Exam} - {Score}";
        }
    }
    class Program
    {
        static void AddStudent(Dictionary<string, Student> students)
        {
            Console.WriteLine("Введите фамилию студента:");
            string surname = Console.ReadLine();
            Console.WriteLine("Введите имя студента:");
            string name = Console.ReadLine();
            Console.WriteLine("Введите год рождения студента:");
            int yearOfBirth = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите экзамен, с которым поступил студент:");
            string exam = Console.ReadLine();
            Console.WriteLine("Введите баллы студента:");
            int score = int.Parse(Console.ReadLine());
            Student student = new Student(surname, name, yearOfBirth, exam, score);
            students.Add(student.FullName, student);
        }
        static void RemoveStudent(Dictionary<string, Student> students)
        {
            Console.WriteLine("Введите фамилию и имя студента, которого хотите удалить:");
            string fullName = Console.ReadLine();
            students.Remove(fullName);
        }
        static void SortStudents(Dictionary<string, Student> students)
        {
            List<Student> sortedStudents = new List<Student>(students.Values);
            sortedStudents.Sort((a, b) => a.Score.CompareTo(b.Score));
            foreach (Student student in sortedStudents)
            {
                Console.WriteLine(student);
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1: перемещать лист с файлами");
            List<string> imagePaths = new List<string>();
            List<string> mixedImagePaths = new List<string>();
            string imagePath = "Images";
            string[] imageFiles = Directory.GetFiles(imagePath);
            imagePaths.AddRange(imageFiles);
            imagePaths.AddRange(imageFiles);
            Random random = new Random();
            mixedImagePaths = imagePaths.OrderBy(x => random.Next()).ToList();
            Console.WriteLine("Изначальный List:      ||   Перемешанный List: ");
            for (int i = 0; i < imagePaths.Count; i++)
            {
                Console.WriteLine($"{i+1} - {Path.GetFileName(imagePaths[i])}  || {i+1} - {Path.GetFileName(imagePaths[i])}");
            }
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();


            Console.WriteLine("Задание 2: создать студентов из группы");
            Dictionary<string, Student> students = new Dictionary<string, Student>();
            StreamReader reader = File.OpenText("students.txt");
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(';', ' ');
                Console.WriteLine(parts[0]);
                Student student = new Student(parts[0], parts[1], int.Parse(parts[2]), parts[3], int.Parse(parts[4]));
                
                students.Add(student.FullName, student);
            }
            
            string choice;
            do
            {
                Console.WriteLine("Что вы хотите сделать?");
                Console.WriteLine("1. Добавить нового студента");
                Console.WriteLine("2. Удалить студента");
                Console.WriteLine("3. Сортировать студентов по баллам");
                Console.WriteLine("4. Выход");
                Console.WriteLine("5. Просмотреть список студентов");
                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        AddStudent(students);
                        break;
                    case "2":
                        RemoveStudent(students);
                        break;
                    case "3":
                        SortStudents(students);
                        break;
                    case "4":
                        break;
                    case "5":
                        foreach (Student student in students.Values)
                        {
                            Console.WriteLine(student);
                        }
                        break;
                    default:
                        Console.WriteLine("Неизвестный выбор");
                        break;
                }
            } while (choice != "4");
            reader.Close();
            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();

            Console.WriteLine("Задание 3: Бабушки и болезни");
            Queue<Grandma> grandmas = new Queue<Grandma>();
            Stack<Hospital> hospitals = new Stack<Hospital>();
            hospitals.Push(new Hospital("Больница №1", new[] { "грипп", "ОРВИ" }.ToList(), 2));
            hospitals.Push(new Hospital("Больница №2", new[] { "коронавирус", "диабет" }.ToList(), 3));
            hospitals.Push(new Hospital("Больница №3", new[] { "сердечно-сосудистые заболевания", "онкология" }.ToList(), 2));
            grandmas.Enqueue(new Grandma("Бабуля 1", 80, "грипп", "кагоцел"));
            grandmas.Enqueue(new Grandma("Бабуля 2", 70, "ОРВИ", "арбидол"));
            grandmas.Enqueue(new Grandma("Бабуля 3", 60, "коронавирус", "фавипиравир"));
            grandmas.Enqueue(new Grandma("Бабуля 4", 50, "диабет", "инсулин"));
            grandmas.Enqueue(new Grandma("Бабуля 5", 40, "сердечно-сосудистые заболевания", "статины"));
            grandmas.Enqueue(new Grandma("Бабуля 6", 30, "онкология", "химио и лучевая терапия"));
            while (grandmas.Count > 0)
            {
                Grandma grandma = grandmas.Dequeue();
                List<string> diseases = grandma.Diseases;
                Hospital hospital = hospitals.FirstOrDefault(h => h.Treats(diseases, grandma).Count >= diseases.Count / 2 && h.CurrentCount < h.Capacity);
                if (hospital != null)
                {
                    hospital.AddGrandma(grandma);
                }
                else
                {
                    Console.WriteLine($"Бабуля {grandma.Name} не может попасть в больницу.");
                }
            }
            foreach (Grandma grandma in grandmas)
            {
                Console.WriteLine(grandma);
            }
            foreach (Hospital hospital in hospitals)
            {
                Console.WriteLine(hospital);
            }

            Console.WriteLine("Нажмите Enter");
            Console.ReadKey();



        }
    }
}

