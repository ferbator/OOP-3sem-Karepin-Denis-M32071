using System;
using System.Collections.Generic;
using IsuExtra.Objects.AuxObjects;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Group
    {
        private const int CapacityForGroupOfStudent = 30;
        private readonly List<Student> _students;

        public Group()
        {
            Name = null;
            _students = new List<Student>();
            NumberOfCourse = null;
        }

        public Group(string name)
        {
            int tmp = int.Parse(name[3].ToString() + name[4].ToString());
            if (tmp < 0 || tmp > 99) throw new IsuExtraException("Incorrect group");
            Name = name;
            _students = new List<Student>(CapacityForGroupOfStudent);

            NumberOfCourse = new CourseNumber((int)(name[2] - '0'));
        }

        public CourseNumber NumberOfCourse { get; }
        public Schedule GroupSchedule { get; private set; }
        public string Name { get; }
        public Faculty Faculty { get; private set; }

        public void AddFaculty(Faculty faculty)
        {
            Faculty = faculty;
        }

        public void AddScheduleToGroup(Schedule schedule)
        {
            GroupSchedule = schedule;
        }

        public void PlusStudent(Student student)
        {
            if (_students.Count == CapacityForGroupOfStudent) throw new IsuExtraException("The group is full");
            _students.Add(student);
        }

        public List<Student> GetStudentsNotSignForOgnp()
        {
            var tmp = new List<Student>();
            foreach (Student student in _students)
            {
                if (student.CountOfOgnp() == 0)
                    tmp.Add(student);
            }

            return tmp;
        }

        public void GetInfo()
        {
            Console.Write($"{Name}\n");

            foreach (Student student in _students)
            {
                student.GetInfo();
                Console.WriteLine();
            }
        }
    }
}