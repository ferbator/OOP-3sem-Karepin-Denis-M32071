using System;

namespace Isu.Objects
{
    public class Student
    {
        private static int _counter;

        public Student()
        {
            Name = string.Empty;
            Groupname = new Group();
            Id = 100000;
        }

        public Student(string name, Group groupname)
        {
            int id = ++_counter;
            Name = name;
            Groupname = groupname;
            Id = 100000 + id;
        }

        public string Name { get; }
        public Group Groupname { get; set; }

        public int Id { get; }

        public void ChangeGroup(Student student, Group newgroup)
        {
            student.Groupname = newgroup;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Группа: {Groupname.Name} ФИ: {Name} ID: {Id}");
        }
    }
}