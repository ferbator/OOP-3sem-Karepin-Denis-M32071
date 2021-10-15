using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Faculty
    {
        private readonly List<Group> _groups;
        private TrainingGroup _ognp;
        public Faculty(string name)
        {
            Name = name;
            _ognp = new TrainingGroup();
            _groups = new List<Group>();
        }

        public string Name { get; }

        public void AddTrainingGroup(TrainingGroup trainingGroup)
        {
            _ognp = trainingGroup;
        }

        public Group AddGroup(string nameOfGroup)
        {
            if (_groups.Any(group => group.Name == nameOfGroup)) throw new IsuExtraException("There is already such a group");
            var group = new Group(nameOfGroup);
            _groups.Add(group);
            group.AddFaculty(this);

            return group;
        }

        public bool FindTrainingGroup(TrainingGroup trainingGroup)
        {
            return trainingGroup == _ognp;
        }

        public void GetInfo()
        {
            Console.Write($"{Name}\n");

            foreach (Group @group in _groups)
            {
                @group.GetInfo();
                Console.WriteLine();
            }

            _ognp.GetInfo();
        }
    }
}