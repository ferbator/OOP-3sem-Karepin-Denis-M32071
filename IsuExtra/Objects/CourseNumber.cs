using System;
using System.Collections.Generic;
using IsuExtra.Tools;

namespace IsuExtra.Objects
{
    public class CourseNumber
    {
        public CourseNumber(int courseNumber)
        {
            if (courseNumber is <= 0 or >= 5) throw new IsuExtraException("There is no such course");
            Number = courseNumber;
        }

        public int Number { get; }

        public void GetInfo()
        {
            Console.Write($"{Number}\n");
        }
    }
}