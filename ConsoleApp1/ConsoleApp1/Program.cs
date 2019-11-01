using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Student
    {
        public int id;
        public string name;
        public string dateOfBirth;
        public string instute;
        public string course;
        public string group;
        public float averagePoints;

        public void Print()
        {
            Console.WriteLine($"ФИО: {name} Дата рождения: {dateOfBirth} Институт: {instute} Курс: {course} Группа: {group} Средний балл: {averagePoints}");
        }
    }

    public class DB
    {
        public List<Student> students = new List<Student>();

        public void Menu()
        {
            string input = "";

            Console.WriteLine("\tЗдравствуйте" +
                "\nВыберите функцию:" +
                "\nВывести список студентов" +
                "\nДобавление студентов" +
                "\nМодификация списка студентов" +
                "\nУдаление студентов из списка" +
                "\nСортировка по ФИО" +
                "\nСортировка по дате" +
                "\nОбратная сортировка по ФИО" +
                "\nОбратная сортировка по дате" +
                "\nПоиск студентов по ФИО" +
                "\nПоиск студентов по дате рождения" +
                "\nНахождение максимального среднего балла" +
                "\nНахождение минимального среднего балла" +
                "\nНахождение среднего балла" +
                "\nНахождение суммы средних баллов");

            while(input != "stop")
            {
                input = Console.ReadLine();

                switch(input)
                {
                    case "1":
                        Print();
                        break;
                }
            }
        }

        public void Add(List<Student> students)
        { }

        public void Modify(List<Student> students)
        { }

        public void Delete(List<Student> students)
        { }

        public void Sort(Comparer<Student> comparer)
        {
            students.Sort(comparer);
        }

        public void Search()
        { 
        
        }

        public void Average()
        { }

        public void Sum()
        { }

        public void SaveToFile()
        { }

        void Print()
        {
            foreach (Student s in students)
                s.Print();
        }
    }

    public static class InputHandleService
    {
        public static List<Student> InputHandle()
        {
            return null;
        }

        //static List<Student> KeyboardInput()
        //{

        //}

        //static List<Student> FileInput()
        //{

        //}
    }

    class Program
    {
        static void Main(string[] args)
        {
            DB dataBase = new DB();
            dataBase.students = students;
            dataBase.Menu();
        }

        public static List<Student> students = new List<Student>()
            {
                new Student()
                {
                    name = "Иванов Иван Иванович",
                    dateOfBirth = "01.01.2001",
                    instute = "МИСиС",
                    course = "1",
                    group = "БИВТ-19-5",
                    averagePoints = 90
                },
                new Student()
                {
                    name = "Петров Петр Петрович",
                    dateOfBirth = "02.02.2002",
                    instute = "МИСиС",
                    course = "1",
                    group = "БИВТ-19-6",
                    averagePoints = 80
                }
            };
    }
}
