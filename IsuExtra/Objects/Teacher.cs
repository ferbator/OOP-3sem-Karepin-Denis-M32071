using System;

namespace IsuExtra.Objects
{
    public class Teacher
    {
        private static int _counter;
        public Teacher(string name)
        {
            int id = ++_counter;
            Name = name;
            Id = 10000 + id;
        }

        public string Name { get; }
        public int Id { get; }

        public void GetInfo()
        {
            Console.WriteLine($"ФИ: {Name} ID: {Id}");
        }
    }
}