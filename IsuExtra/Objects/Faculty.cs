using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class Faculty
    {
        private readonly List<Group> _groups;
        private readonly List<TrainingGroup> _ognp;
        public Faculty(string name)
        {
            Name = name ?? throw new IsuExtraException("Invalid name of faculty");
            _groups = new List<Group>();
            _ognp = new List<TrainingGroup>();
        }

        public string Name { get; }

        public void AddTrainingGroup(TrainingGroup trainingGroup)
        {
            _ognp.Add(trainingGroup);
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
            return _ognp.Contains(trainingGroup);
        }
    }
}