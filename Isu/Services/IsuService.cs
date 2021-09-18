using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Objects;
using Isu.Tools;
using Group = Isu.Objects.Group;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _groups;

        public IsuService()
        {
            _groups = new List<Group>();
        }

        public IsuService(List<Group> groups)
        {
            _groups = groups;
        }

        public void GetInfo()
        {
            foreach (Group group in _groups)
            {
                group.GetInfo();
                Console.WriteLine();
            }
        }

        public Group AddGroup(string name)
        {
            // if (name[0].ToString() + name[1].ToString() != "M3") throw new IsuException("Unknown faculty");
            if (!name.StartsWith("M3")) throw new IsuException("Unknown faculty");
            if (name.Length != 5) throw new IsuException("Incorrect group");

            if (_groups.Any(groupone => groupone.Name == name))
            {
                throw new IsuException("There is already such a group");
            }

            var group = new Group(name);
            _groups.Add(group);

            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, group);
            group.PlusStudent(student);

            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in from @group in _groups from student in @group.Students where student.Id == id select student)
            {
                return student;
            }

            throw new IsuException("The student is absent");
        }

        public Student FindStudent(string name)
        {
            return _groups.SelectMany(@group => @group.Students).FirstOrDefault(student => student.Name == name);
        }

        public List<Student> FindStudents(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Name == groupName)
                {
                    return group.Students;
                }
            }

            return new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            foreach (var @group in _groups.Where(@group => @group.NumberOfCourse == courseNumber))
            {
                return @group.Students;
            }

            return new List<Student>();
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group group in _groups)
            {
                if (group.Name == groupName)
                {
                    return group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> tmp = null;
            tmp.AddRange(_groups.Where(@group => @group.NumberOfCourse == courseNumber));

            return tmp;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group.Students.Remove(student);
            student.ChangeGroup(student, newGroup);
            newGroup.Students.Add(student);
        }
    }
}