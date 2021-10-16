using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Objects.AuxObjects;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Stream : IEquatable<Stream>
    {
        private const int CapacityForStreamOfStudent = 30;
        private readonly List<Student> _students;

        public Stream(string name)
        {
            Name = name ?? throw new IsuExtraException("Invalid name of stream");
            _students = new List<Student>(CapacityForStreamOfStudent);
        }

        public string Name { get; }
        public Schedule StreamSchedule { get; private set; }

        public void PlusStudent(Student student)
        {
            if (student == null) throw new IsuExtraException("Null student");
            if (_students.Count == CapacityForStreamOfStudent) throw new IsuExtraException("The group is full");
            _students.Add(student);
        }

        public bool FindStudent(Student student)
        {
            return _students.Any(student1 => Equals(student1, student));
        }

        public void DeleteStudent(Student student)
        {
            if (FindStudent(student))
             _students.Remove(student);
        }

        public void AddScheduleToStreamOfTrainingGroup(Schedule schedule)
        {
            StreamSchedule = schedule;
        }

        public List<Student> GetStudents()
        {
            return _students.ToList();
        }

        public bool Equals(Stream other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Stream)obj);
        }

        public override int GetHashCode()
        {
            return Name != null ? Name.GetHashCode() : 0;
        }
    }
}