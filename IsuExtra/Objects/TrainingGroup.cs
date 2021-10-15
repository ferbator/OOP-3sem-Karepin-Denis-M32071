using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Objects.AuxObjects;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class TrainingGroup : IEquatable<TrainingGroup>
    {
        private readonly List<Stream> _streams;

        public TrainingGroup()
        {
            Name = null;
            _streams = new List<Stream>();
        }

        public TrainingGroup(string name)
        {
            Name = name;
            _streams = new List<Stream>();
        }

        public string Name { get; }
        public Faculty Faculty { get; private set; }

        public void AddFaculty(Faculty faculty)
        {
            Faculty = faculty;
        }

        public Stream AddStream(string nameOfStream)
        {
            var tmp = new Stream(nameOfStream);
            _streams.Add(tmp);
            return tmp;
        }

        public void AddStudentToStream(Student student)
        {
            foreach (Stream stream in _streams)
            {
                if (stream.StreamSchedule.TryMergeSchedule(student.Group.GroupSchedule).CountOfLessonsInSchedule() != 0)
                {
                    stream.PlusStudent(student);
                    student.AddTrainingGroup(this);
                    student.AddPersonalSchedule(stream.StreamSchedule.TryMergeSchedule(student.Group.GroupSchedule));
                    break;
                }
            }
        }

        public void DeleteStudentToStream(Student student)
        {
            foreach (Stream stream in _streams.Where(stream => stream.FindStudent(student)))
            {
                stream.DeleteStudent(student);
            }
        }

        public bool FindStudentToStream(Student student)
        {
            return _streams.All(stream => stream.FindStudent(student));
        }

        public List<Stream> GetStreams()
        {
            return _streams.ToList();
        }

        public void GetInfo()
        {
            Console.Write($"{Name}\n");

            foreach (Stream stream in _streams)
            {
                stream.GetInfo();
                Console.WriteLine();
            }
        }

        public bool Equals(TrainingGroup other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Name == other.Name && Equals(Faculty, other.Faculty);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TrainingGroup)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Faculty);
        }
    }
}