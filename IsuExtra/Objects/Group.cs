using System;
using System.Collections.Generic;
using System.Linq;
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
            // $"{name[3]}{name[4]}"
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
            return _students.Where(student => student.CountOfOgnp() == 0).ToList();
        }
    }
}