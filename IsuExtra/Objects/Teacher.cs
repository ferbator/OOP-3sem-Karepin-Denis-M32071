using System;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Teacher
    {
        private static int _counter;
        public Teacher(string name)
        {
            int id = ++_counter;
            Name = name ?? throw new IsuExtraException("Invalid name of teacher");
            Id = 10000 + id;
        }

        public string Name { get; }
        public int Id { get; }
    }
}