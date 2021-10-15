using System;

namespace IsuExtra.Objects.AuxObjects
{
    public class Lesson : IEquatable<Lesson>
    {
        public Lesson(string name, Time time, string audienceOfLes, Teacher teacherForLes)
        {
            Name = name;
            AudienceOfLes = audienceOfLes;
            TeacherForLes = teacherForLes;
            TimeOfLes = time;
        }

        public string Name { get; }
        public Time TimeOfLes { get; }
        public string AudienceOfLes { get; }
        public Teacher TeacherForLes { get; }

        public bool Equals(Lesson other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(TimeOfLes, other.TimeOfLes);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Lesson)obj);
        }

        public override int GetHashCode()
        {
            return TimeOfLes != null ? TimeOfLes.GetHashCode() : 0;
        }
    }
}