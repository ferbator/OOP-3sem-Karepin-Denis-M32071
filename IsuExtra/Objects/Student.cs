using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Objects.AuxObjects;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Student
    {
        private static int _counter;
        private List<TrainingGroup> _ognp = new List<TrainingGroup>();

        public Student(string name)
        {
            int id = ++_counter;
            Name = name ?? throw new IsuExtraException("Invalid name of student");
            Id = 1000000 + id;
        }

        public string Name { get; }
        public Group Group { get; private set; }
        public Schedule PersonalSchedule { get; private set; }
        public int Id { get; }

        public void AddGroup(Group @group)
        {
            Group = @group;
        }

        public int CountOfOgnp()
        {
            return _ognp.Count;
        }

        public void DeleteDataOfOgnp(TrainingGroup ognp)
        {
            _ognp.Remove(ognp);
            this.AddPersonalSchedule(Group.GroupSchedule);
        }

        public void AddTrainingGroup(TrainingGroup trainingGroup)
        {
            if (_ognp.Count == 2) throw new IsuExtraException("Attempt to add more than two ognp");
            _ognp.Add(trainingGroup);
        }

        public bool FindTrainingGroup(TrainingGroup trainingGroup)
        {
            return _ognp.Any(group => group.Name == trainingGroup.Name);
        }

        public void AddPersonalSchedule(Schedule schedule)
        {
            PersonalSchedule = schedule;
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