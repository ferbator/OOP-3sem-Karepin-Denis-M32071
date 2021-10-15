using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects.AuxObjects
{
    public class Schedule
    {
        private List<Lesson> _schedule;

        public Schedule(List<Lesson> schedule)
        {
            _schedule = schedule;
        }

        public Schedule()
        {
            _schedule = new List<Lesson>();
        }

        public Schedule TryMergeSchedule(Schedule schedule)
        {
            var tmpSchedule = new Schedule();
            if (_schedule.All(schedule.CheckPossibilityAddLessonInSchedule))
            {
                tmpSchedule._schedule = _schedule.Concat(schedule._schedule).ToList();
            }

            return tmpSchedule;
        }

        public int CountOfLessonsInSchedule()
        {
            return _schedule.Count;
        }

        public void AddLessonInSchedule(Lesson lesson)
        {
            if (!CheckPossibilityAddLessonInSchedule(lesson)) throw new IsuExtraException("Can't add lesson");
            _schedule.Add(lesson);
        }

        public void GetInfo()
        {
            foreach (Lesson lesson in _schedule)
            {
                Console.Write(lesson.Name);
            }
        }

        private bool CheckPossibilityAddLessonInSchedule(Lesson lesson)
        {
            return _schedule.Count == 0 || _schedule.All(les => ((les.TimeOfLes.GetTime() - 90 > 0) &&
                                                                 (les.TimeOfLes.GetTime() + 90 <= lesson.TimeOfLes.GetTime() ||
                                                                  lesson.TimeOfLes.GetTime() + 90 <= les.TimeOfLes.GetTime())));
        }
    }
}