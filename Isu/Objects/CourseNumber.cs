using Isu.Tools;

namespace Isu.Objects
{
    public class CourseNumber
    {
        public CourseNumber()
        {
            Number = null;
        }

        public CourseNumber(int number)
        {
            if (number > 0 && number < 5)
            {
                Number = number;
            }
            else
            {
                throw new IsuException("There is no such course");
            }
        }

        public int? Number { get; }
    }
}