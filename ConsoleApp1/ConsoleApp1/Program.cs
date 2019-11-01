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
    }

    public class DB
    {
        public List<Student> students = new List<Student>();

        public void Menu()
        { }

        public void Add()
        { }

        public void Modify()
        { }

        public void Delete()
        { }

        public void Sort()
        { }

        public void ReverseSort()
        { }

        public void Search()
        { }

        public void Average()
        { }

        public void Sum()
        { }
    }

    public static class InputHandle
    {
        public static Student KeyboardInput()
        {

        }

        public static Student FileInput()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DB dataBase = new DB();
        }

        public List<Student> sutdents = new List<Student>()
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
