using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Tools;

namespace IsuExtra.Objects.AuxObjects
{
    public class Schedule
    {
        private const int DurationOfLessonInMinute = 90;
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

        private bool CheckPossibilityAddLessonInSchedule(Lesson lesson)
        {
            return _schedule.Count == 0 || _schedule.All(les => ((les.TimeOfLes.GetTime() - DurationOfLessonInMinute > 0) &&
                                                                 (les.TimeOfLes.GetTime() + DurationOfLessonInMinute <= lesson.TimeOfLes.GetTime() ||
                                                                  lesson.TimeOfLes.GetTime() + DurationOfLessonInMinute <= les.TimeOfLes.GetTime())));
        }
    }
}