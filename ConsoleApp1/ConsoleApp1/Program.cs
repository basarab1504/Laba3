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
        public string dateOfBirth;
        public string instute;
        public string course;
        public string group;
        public float averagePoints;

        public string GetStudentInfo()
        {
            return $"{id} {name} {dateOfBirth} {instute} {course} {group} {averagePoints}";
        }

        public void Print()
        {
            Console.WriteLine($"ID: {id} ФИО: {name} Дата рождения: {dateOfBirth} Институт: {instute} Курс: {course} Группа: {group} Средний балл: {averagePoints}");
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

                switch(input)
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
                        Search(x => x.dateOfBirth == input);
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

            Console.WriteLine("Информация о студентах успешно добавлена");
        }

        void Modify(List<Student> students)
        {
            foreach(Student s in students)
            {
                int indexOfStudentToModify = this.students.FindIndex(x => x.id == s.id);

                if (indexOfStudentToModify != -1)
                    this.students[indexOfStudentToModify] = s;
            }
            Console.WriteLine("Информация о студентах успешно обновлена");
        }

        void Delete(List<Student> students)
        {
            foreach (Student s in students)
                this.students.RemoveAll(x => x.id == s.id);

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

            if(studentsFound.Count == 0)
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
            float min = students.Min(x => x.averagePoints);
            Console.WriteLine($"Минимальный балл: {min}");
        }

        void Max()
        {
            float max = students.Max(x => x.averagePoints);
            Console.WriteLine($"Максимальный балл: {max}");
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

            for(int i = 0; i < students.Count - 1; i++)
                allInfo += students[i].GetStudentInfo() + "\r\n";
            allInfo += students[students.Count - 1].GetStudentInfo();

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

            switch(input)
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

            Console.WriteLine("Введите данные построчно в формате \"ID Фамилия Имя Отчество ДД.ММ.ГГГГ Аббревиатура ВУЗа Курс Группа Средний балл\" или \"stop\" для окончания ввода");

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

            return File.ReadAllText(input);
        }

        static List<Student> ParseStringsToStudents(string toParse)
        {
            List<Student> studentsParsed = new List<Student>();

            foreach (string row in toParse.Split(new string[] { "\r\n" }, StringSplitOptions.None))
                    studentsParsed.Add(ParseStringsToStudent(row.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)));

            return studentsParsed;
        }

        static Student ParseStringsToStudent(string[] toParse)
        {
            Student student = new Student();

            if (toParse.Length != 9)
                return student;

            student.id = toParse[0];
            student.name = toParse[1] + " " + toParse[2] + " " + toParse[3];
            student.dateOfBirth = toParse[4];
            student.instute = toParse[5];
            student.course = toParse[6];
            student.group = toParse[7];
            student.averagePoints = float.Parse(toParse[8]);

            return student;
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
