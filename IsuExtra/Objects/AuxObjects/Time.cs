using IsuExtra.Tools;

namespace IsuExtra.Objects.AuxObjects
{
    public class Time
    {
        private readonly int _hour;
        private readonly int _minutes;
        private readonly int _day;

        public Time(int day, int hour, int minutes)
        {
            if (hour is >= 24 or < 0 || minutes is >= 60 or < 0 || day is >= 7 or < 0)
            {
                throw new IsuExtraException("Wrong date");
            }
            else
            {
                _day = day;
                _hour = hour;
                _minutes = minutes;
                ParsTime = day.ToString() + "/" + hour.ToString() + ":" + minutes.ToString();
            }
        }

        public string ParsTime { get; }

        public int GetTime()
        {
            return (((_day * 24) + _hour) * 60) + _minutes;
        }

        public int GetDay()
        {
            return _day;
        }
    }
}