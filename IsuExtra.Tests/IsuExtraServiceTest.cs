using IsuExtra.Objects;
using IsuExtra.Objects.AuxObjects;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    [TestFixture]
    public class Tests
    {
        private IsuExtraService _isuExtraService;
        
        [SetUp]
        public void Setup()
        {
            _isuExtraService = new IsuExtraService();
        }
        
        [Test]
        public void AddNewOGNP_OGNPHasFaculty()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            TrainingGroup testTrainingGroup = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);

            if (!_isuExtraService.FindTrainingGroupInFaculties(testTrainingGroup) && testTrainingGroup.Faculty.Name != itip.Name)
                Assert.Fail();
        }

        [Test]
        public void RecordingStudentOGNPHisFaculty_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            Group testGroup1 = _isuExtraService.AddGroupInFaculty("M3207", itip);
            Teacher testTeach11 = _isuExtraService.AddTeacher("Евграф_");
            
            var time11 = new Time(0, 10, 00);
            var les11 = new Lesson("Высшая Математика", time11, "442B", testTeach11);
            var scheduleM3207 = new Schedule();
            scheduleM3207.AddLessonInSchedule(les11);
            testGroup1.AddScheduleToGroup(scheduleM3207);
            
            TrainingGroup testTrainingGroup1 = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);
            
            Teacher testTeach1 = _isuExtraService.AddTeacher("Евграф");
            var time1 = new Time(0, 10, 00);
            var les1 = new Lesson("Высшая Математика", time1, "442B", testTeach1);
            var scheduleItip = new Schedule();
            scheduleItip.AddLessonInSchedule(les1);
            Stream testStreamOfTrainingGroup1 = testTrainingGroup1.AddStream("1/01");
            testStreamOfTrainingGroup1.AddScheduleToStreamOfTrainingGroup(scheduleItip);
            
            Student testStudent = _isuExtraService.AddStudentToGroup("Человек", testGroup1);
            
            _isuExtraService.StudentEntryToTrainingGroup(testStudent, testTrainingGroup1);
            });
        }

        [Test]
        public void CheckingStudentRecordForSuitableOgnp_OgnpHasStudent()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            
            Group testGroup1 = _isuExtraService.AddGroupInFaculty("M3207", itip);
            Teacher testTeach11 = _isuExtraService.AddTeacher("Евграф_");
            var time11 = new Time(0, 10, 00);
            var les11 = new Lesson("Высшая Математика", time11, "442B", testTeach11);
            var scheduleM3207 = new Schedule();
            scheduleM3207.AddLessonInSchedule(les11);
            testGroup1.AddScheduleToGroup(scheduleM3207);
            
            TrainingGroup testTrainingGroup1 = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);
            Teacher testTeach1 = _isuExtraService.AddTeacher("Евграф");
            var time1 = new Time(0, 10, 00);
            var les1 = new Lesson("Высшая Математика", time1, "442B", testTeach1);
            var scheduleItip = new Schedule();
            scheduleItip.AddLessonInSchedule(les1);
            Stream testStreamOfTrainingGroup1 = testTrainingGroup1.AddStream("1/01");
            testStreamOfTrainingGroup1.AddScheduleToStreamOfTrainingGroup(scheduleItip);
            
            Faculty neItip = _isuExtraService.AddFaculty("U3");
            
            TrainingGroup testTrainingGroup2 = _isuExtraService.AddTrainingGroupToFaculty("Данные Анализа", neItip);
            Teacher testTeach2 = _isuExtraService.AddTeacher("Степан");
            var time2 = new Time(0, 13, 00);
            var les2 = new Lesson("Высшая Математика", time2, "412B", testTeach2);
            var scheduleNeitip = new Schedule();
            scheduleNeitip.AddLessonInSchedule(les2);
            Stream testStreamOfTrainingGroup2 = testTrainingGroup2.AddStream("1/02");
            testStreamOfTrainingGroup2.AddScheduleToStreamOfTrainingGroup(scheduleNeitip);
            
            Student testStudent = _isuExtraService.AddStudentToGroup("Человек", testGroup1);
            
            _isuExtraService.StudentEntryToTrainingGroup(testStudent, testTrainingGroup2);
            
            if (!testStudent.FindTrainingGroup(testTrainingGroup2) || !testTrainingGroup2.FindStudentToStream(testStudent))
                Assert.Fail();

        }

        [Test]
        public void CheckRemovalStudentFromOgnp_OgnpHasNotStudent()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            
            Group testGroup1 = _isuExtraService.AddGroupInFaculty("M3207", itip);
            Teacher testTeach11 = _isuExtraService.AddTeacher("Евграф_");
            var time11 = new Time(0, 10, 00);
            var les11 = new Lesson("Высшая Математика", time11, "442B", testTeach11);
            var scheduleM3207 = new Schedule();
            scheduleM3207.AddLessonInSchedule(les11);
            testGroup1.AddScheduleToGroup(scheduleM3207);
            
            TrainingGroup testTrainingGroup1 = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);
            Teacher testTeach1 = _isuExtraService.AddTeacher("Евграф");
            var time1 = new Time(0, 10, 00);
            var les1 = new Lesson("Высшая Математика", time1, "442B", testTeach1);
            var scheduleItip = new Schedule();
            scheduleItip.AddLessonInSchedule(les1);
            Stream testStreamOfTrainingGroup1 = testTrainingGroup1.AddStream("1/01");
            testStreamOfTrainingGroup1.AddScheduleToStreamOfTrainingGroup(scheduleItip);
            
            Faculty neItip = _isuExtraService.AddFaculty("U3");
            
            TrainingGroup testTrainingGroup2 = _isuExtraService.AddTrainingGroupToFaculty("Данные Анализа", neItip);
            Teacher testTeach2 = _isuExtraService.AddTeacher("Степан");
            var time2 = new Time(0, 13, 00);
            var les2 = new Lesson("Высшая Математика", time2, "412B", testTeach2);
            var scheduleNeitip = new Schedule();
            scheduleNeitip.AddLessonInSchedule(les2);
            Stream testStreamOfTrainingGroup2 = testTrainingGroup2.AddStream("1/02");
            testStreamOfTrainingGroup2.AddScheduleToStreamOfTrainingGroup(scheduleNeitip);
            
            Student testStudent = _isuExtraService.AddStudentToGroup("Человек", testGroup1);
            
            _isuExtraService.StudentEntryToTrainingGroup(testStudent, testTrainingGroup2);
            
            _isuExtraService.StudentDeleteInTrainingGroup(testStudent, testTrainingGroup2);
            
            if (testStudent.FindTrainingGroup(testTrainingGroup2) || testTrainingGroup2.FindStudentToStream(testStudent))
                Assert.Fail();
        }

        [Test]
        public void ReceivingStreamsInOgnp_InReceivedStreamThereIsRealStream()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            TrainingGroup testTrainingGroup1 = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);
            Teacher testTeach1 = _isuExtraService.AddTeacher("Евграф");
            var time1 = new Time(0, 10, 00);
            var les1 = new Lesson("Высшая Математика", time1, "442B", testTeach1);
            var scheduleItip = new Schedule();
            scheduleItip.AddLessonInSchedule(les1);
            Stream testStreamOfTrainingGroup1 = testTrainingGroup1.AddStream("1/01");
            testStreamOfTrainingGroup1.AddScheduleToStreamOfTrainingGroup(scheduleItip);
            
            if (!_isuExtraService.GetStreamOfOgnp(testTrainingGroup1).Contains(testStreamOfTrainingGroup1))
                Assert.Fail();
        }

        [Test]
        public void GettingListOfStudentsCertainGroup_InReceivedStreamThereIsRealStudent()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            
            Group testGroup1 = _isuExtraService.AddGroupInFaculty("M3207", itip);
            Teacher testTeach11 = _isuExtraService.AddTeacher("Евграф_");
            var time11 = new Time(0, 10, 00);
            var les11 = new Lesson("Высшая Математика", time11, "442B", testTeach11);
            var scheduleM3207 = new Schedule();
            scheduleM3207.AddLessonInSchedule(les11);
            testGroup1.AddScheduleToGroup(scheduleM3207);
            
            TrainingGroup testTrainingGroup1 = _isuExtraService.AddTrainingGroupToFaculty("Анализ Данных", itip);
            Teacher testTeach1 = _isuExtraService.AddTeacher("Евграф");
            var time1 = new Time(0, 10, 00);
            var les1 = new Lesson("Высшая Математика", time1, "442B", testTeach1);
            var scheduleItip = new Schedule();
            scheduleItip.AddLessonInSchedule(les1);
            Stream testStreamOfTrainingGroup1 = testTrainingGroup1.AddStream("1/01");
            testStreamOfTrainingGroup1.AddScheduleToStreamOfTrainingGroup(scheduleItip);
            
            Faculty neItip = _isuExtraService.AddFaculty("U3");
            
            TrainingGroup testTrainingGroup2 = _isuExtraService.AddTrainingGroupToFaculty("Данные Анализа", neItip);
            Teacher testTeach2 = _isuExtraService.AddTeacher("Степан");
            var time2 = new Time(0, 13, 00);
            var les2 = new Lesson("Высшая Математика", time2, "412B", testTeach2);
            var scheduleNeitip = new Schedule();
            scheduleNeitip.AddLessonInSchedule(les2);
            Stream testStreamOfTrainingGroup2 = testTrainingGroup2.AddStream("1/02");
            testStreamOfTrainingGroup2.AddScheduleToStreamOfTrainingGroup(scheduleNeitip);
            
            Student testStudent = _isuExtraService.AddStudentToGroup("Человек", testGroup1);
            
            _isuExtraService.StudentEntryToTrainingGroup(testStudent, testTrainingGroup2);
            
            if (!_isuExtraService.GetStudentsInStreamOfOgnp(testStreamOfTrainingGroup2).Contains(testStudent))
                Assert.Fail();
        }

        [Test]
        public void GettingListStudentsNotSignForOgnp_InReceivedListThereIsNotSignStudent()
        {
            Faculty itip = _isuExtraService.AddFaculty("M3");
            
            Group testGroup1 = _isuExtraService.AddGroupInFaculty("M3207", itip);
            Teacher testTeach11 = _isuExtraService.AddTeacher("Евграф_");
            var time11 = new Time(0, 10, 00);
            var les11 = new Lesson("Высшая Математика", time11, "442B", testTeach11);
            var scheduleM3207 = new Schedule();
            scheduleM3207.AddLessonInSchedule(les11);
            testGroup1.AddScheduleToGroup(scheduleM3207);
            
            Faculty neItip = _isuExtraService.AddFaculty("U3");
            
            TrainingGroup testTrainingGroup2 = _isuExtraService.AddTrainingGroupToFaculty("Данные Анализа", neItip);
            Teacher testTeach2 = _isuExtraService.AddTeacher("Степан");
            var time2 = new Time(0, 13, 00);
            var les2 = new Lesson("Высшая Математика", time2, "412B", testTeach2);
            var scheduleNeitip = new Schedule();
            scheduleNeitip.AddLessonInSchedule(les2);
            Stream testStreamOfTrainingGroup2 = testTrainingGroup2.AddStream("1/02");
            testStreamOfTrainingGroup2.AddScheduleToStreamOfTrainingGroup(scheduleNeitip);
            
            Student testStudent1 = _isuExtraService.AddStudentToGroup("Человек1", testGroup1);
            Student testStudent2 = _isuExtraService.AddStudentToGroup("Человек2", testGroup1);
            Student testStudent3 = _isuExtraService.AddStudentToGroup("Человек3", testGroup1);
            
            _isuExtraService.StudentEntryToTrainingGroup(testStudent1, testTrainingGroup2);
            _isuExtraService.StudentEntryToTrainingGroup(testStudent3, testTrainingGroup2);
            if (!_isuExtraService.GetStudentsNotSignForOgnpInGroup(testGroup1).Contains(testStudent2))
                Assert.Fail();
        }
    }
}