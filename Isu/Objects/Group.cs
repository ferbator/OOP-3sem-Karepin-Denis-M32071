using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Objects
{
    public class Group
    {
        private const int CapacityForGroupOfStudent = 2;
        public Group()
        {
            Name = null;
            Students = new List<Student>();
            NumberOfCourse = null;
        }

        public Group(string name)
        {
            int tmp = int.Parse(name[3].ToString() + name[4].ToString());
            if (tmp < 0 || tmp > 99) throw new IsuException("Incorrect group");
            Name = name;
            Students = new List<Student>(CapacityForGroupOfStudent);
            NumberOfCourse = new CourseNumber((int)(name[2] - '0'));
        }

        public CourseNumber NumberOfCourse { get; }

        public List<Student> Students { get; }
        public string Name { get; }

        public void PlusStudent(Student student)
        {
            if (Students.Count == CapacityForGroupOfStudent) throw new IsuException("The group is full");
            Students.Add(student);
        }

        public void GetInfo()
        {
            Console.Write($"{Name}\n");

            foreach (Student student in Students)
            {
                student.GetInfo();
                Console.WriteLine();
            }
        }
    }
}