using System;

namespace Isu.Objects
{
    public class Student
    {
        private static int _counter;

        public Student()
        {
            Name = string.Empty;
            Group = new Group();
            Id = 100000;
        }

        public Student(string name, Group @group)
        {
            int id = ++_counter;
            Name = name;
            Group = @group;
            Id = 100000 + id;
        }

        public string Name { get; }
        public Group Group { get; private set; }
        public int Id { get; }

        public void ChangeGroup(Student student, Group newgroup)
        {
            student.Group = newgroup;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Группа: {Group.Name} ФИ: {Name} ID: {Id}");
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Student student)) return false;
            return Equals(student);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }

        private bool Equals(Student other)
        {
            return Name == other.Name;
        }
    }
}