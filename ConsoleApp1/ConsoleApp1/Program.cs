using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Student
    {
        public string id;
        public string name;
        public DateTime dateOfBirth;
        public string instute;
        public string course;
        public string group;
        public float averagePoints;

        public string GetStudentInfo()
        {
            return $"{id} {name} {dateOfBirth.ToString("dd.MM.yyyy")} {instute} {course} {group} {averagePoints}";
        }

        public void Print()
        {
            Console.WriteLine($"ID: {id} ФИО: {name} Дата рождения: {dateOfBirth.ToString("dd.MM.yyyy")} Институт: {instute} Курс: {course} Группа: {group} Средний балл: {averagePoints}");
        }
    }

    public class DB
    {
        public List<Student> students = new List<Student>();

        public void Menu()
        {
            string input = "";

            Console.WriteLine("\tЗдравствуйте! Введите номер функции или \"stop\" для выхода из программы\n" +
                "\n1. Вывести список студентов" +
                "\n2. Добавление студентов" +
                "\n3. Модификация списка студентов" +
                "\n4. Удаление студентов из списка" +
                "\n5. Сортировка по ФИО" +
                "\n6. Сортировка по дате" +
                "\n7. Обратная сортировка по ФИО" +
                "\n8. Обратная сортировка по дате" +
                "\n9. Поиск студентов по ФИО" +
                "\n10. Поиск студентов по дате рождения" +
                "\n11. Нахождение максимального среднего балла" +
                "\n12. Нахождение минимального среднего балла" +
                "\n13. Нахождение среднего балла" +
                "\n14. Нахождение суммы средних баллов" +
                "\n15. Сохранить данные в файл");

            while (input != "stop")
            {
                Console.Write("\nВведите номер функции: ");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Print();
                        break;
                    case "2":
                        Add(StudentsParseService.GetStudents());
                        break;
                    case "3":
                        Modify(StudentsParseService.GetStudents());
                        break;
                    case "4":
                        Delete(StudentsParseService.GetStudents());
                        break;
                    case "5":
                        Sort((x, y) => x.name.CompareTo(y.name));
                        break;
                    case "6":
                        Sort((x, y) => x.dateOfBirth.CompareTo(y.dateOfBirth));
                        break;
                    case "7":
                        Sort((x, y) => -x.name.CompareTo(y.name));
                        break;
                    case "8":
                        Sort((x, y) => -x.dateOfBirth.CompareTo(y.dateOfBirth));
                        break;
                    case "9":
                        Console.Write("Введите ФИО: ");
                        input = Console.ReadLine();
                        Search(x => x.name == input);
                        break;
                    case "10":
                        Console.Write("Введите дату рождения: ");
                        input = Console.ReadLine();
                        Search(x => x.dateOfBirth == DateTime.Parse(input));
                        break;
                    case "11":
                        Max();
                        break;
                    case "12":
                        Min();
                        break;
                    case "13":
                        Average();
                        break;
                    case "14":
                        Sum();
                        break;
                    case "15":
                        SaveToFile();
                        break;
                    default:
                        Console.WriteLine($"Упс! Функция \"{input}\" не найдена");
                        break;
                }
            }
        }

        void Add(List<Student> students)
        {
            foreach (Student s in students)
                if (!this.students.Any(x => x.id == s.id))
                    this.students.Add(s);
                else
                    Console.WriteLine($"Студент с ID: {s.id} уже существует в базе и не может быть добавлен повторно");

            if (students.Count != 0)
                Console.WriteLine("Информация о студентах успешно добавлена");
        }

        void Modify(List<Student> students)
        {
            foreach (Student s in students)
            {
                int indexOfStudentToModify = this.students.FindIndex(x => x.id == s.id);

                if (indexOfStudentToModify != -1)
                    this.students[indexOfStudentToModify] = s;
            }

            if (students.Count != 0)
                Console.WriteLine("Информация о студентах успешно обновлена");
        }

        void Delete(List<Student> students)
        {
            foreach (Student s in students)
                this.students.RemoveAll(x => x.id == s.id);

            if (students.Count != 0)
                Console.WriteLine("Информация о студентах успешно удалена");
        }

        void Sort(Comparison<Student> comparer)
        {
            students.Sort(comparer);
            Console.WriteLine("Сортировка успешно завершена");
        }

        void Search(Predicate<Student> predicate)
        {
            List<Student> studentsFound = students.FindAll(predicate);

            foreach (Student s in studentsFound)
                s.Print();

            if (studentsFound.Count == 0)
                Console.WriteLine("Информации о студентах не найдено");
        }

        void Average()
        {
            float average = 0;

            foreach (Student s in students)
                average += s.averagePoints;

            average = average / students.Count;
            Console.WriteLine($"Средний балл: {average}");
        }

        void Min()
        {
            float min = 0;
            try
            {
                min = students.Min(x => x.averagePoints);
            }
            catch
            {
                Console.WriteLine("Невозможно выполнить функцию. Список пуст");
            }
            finally
            {
                Console.WriteLine($"Минимальный балл: {min}");
            }
        }

        void Max()
        {
            float max = 0;
            try
            {
                max = students.Max(x => x.averagePoints);
            }
            catch
            {
                Console.WriteLine("Невозможно выполнить функцию. Список пуст");
            }
            finally
            {
                Console.WriteLine($"Максимальный балл: {max}");
            }
        }

        void Sum()
        {
            float sum = 0;

            foreach (Student s in students)
                sum += s.averagePoints;

            Console.WriteLine($"Сумма средних баллов: {sum}");
        }

        void SaveToFile()
        {
            string input;

            Console.Write("Введите путь к файлу: ");

            input = Console.ReadLine();

            File.WriteAllText(input, GetAllStudentsInfo());
            Console.WriteLine("Информация о студентах успешно сохранена");
        }

        string GetAllStudentsInfo()
        {
            string allInfo = "";

            try
            {
                for (int i = 0; i < students.Count - 1; i++)
                    allInfo += students[i].GetStudentInfo() + "\r\n";
                allInfo += students[students.Count - 1].GetStudentInfo();
            }
            catch
            {
                return allInfo;
            }
            return allInfo;
        }

        void Print()
        {
            foreach (Student s in students)
                s.Print();

            if (students.Count == 0)
                Console.WriteLine("Список пуст!");
        }
    }

    public static class StudentsParseService
    {
        public static List<Student> GetStudents()
        {
            Console.WriteLine("\tКак вы хотите ввести данные?" +
                "\n1. С клавиатуры." +
                "\n2. Из файла");

            List<Student> students = new List<Student>();

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    students = ParseStringsToStudents(KeyboardInput());
                    break;
                case "2":
                    students = ParseStringsToStudents(FileInput());
                    break;
                default:
                    Console.WriteLine("Неизвестная команда!");
                    break;
            }

            return students;
        }

        static string KeyboardInput()
        {
            string input;

            Console.WriteLine("Введите данные построчно в следующем формате: " +
                "\n\"ID Фамилия Имя (Отчество) ДД.ММ.ГГГГ Аббревиатура ВУЗа Курс Группа Средний балл\" или \"stop\" для окончания ввода");

            string array = "";

            input = Console.ReadLine();

            while (input != "stop")
            {
                array += input;
                input = Console.ReadLine();

                if (input != "stop")
                    array += "\r\n";
            }

            return array;
        }

        static string FileInput()
        {
            string input;

            Console.Write("Введите путь к файлу: ");

            input = Console.ReadLine();

            string text = "";

            try
            {
                text = File.ReadAllText(input);
            }
            catch
            {
                Console.WriteLine("Файл не найден");
            }
            return text;
        }

        static List<Student> ParseStringsToStudents(string toParse)
        {
            List<Student> studentsParsed = new List<Student>();

            foreach (string row in toParse.Split(new string[] { "\r\n" }, StringSplitOptions.None))
            {
                Student parsedStudent;
                if (TryParseStringsToStudent(row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries), out parsedStudent))
                    studentsParsed.Add(parsedStudent);
            }

            return studentsParsed;
        }

        static bool TryParseStringsToStudent(string[] toParse, out Student student)
        {
            student = new Student();

            try
            {
                student.id = toParse[0];

                int i;

                for (i = 1; i < toParse.Length; i++)
                    if (!toParse[i].Contains("."))
                        student.name += toParse[i] + " ";
                    else
                        break;
                student.name = student.name.Remove(student.name.Length - 1);

                student.dateOfBirth = DateTime.Parse(toParse[i]);
                student.instute = toParse[i + 1];
                student.course = toParse[i + 2];
                student.group = toParse[i + 3];
                student.averagePoints = float.Parse(toParse[i + 4]);
            }
            catch
            {
                Console.WriteLine("Некорректной ввод");
                return false;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DB dataBase = new DB();
            dataBase.Menu();
        }
    }
}