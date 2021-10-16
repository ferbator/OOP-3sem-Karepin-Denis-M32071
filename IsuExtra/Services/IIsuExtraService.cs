using System.Collections.Generic;
using IsuExtra.Objects;

namespace IsuExtra.Services
{
    public interface IIsuExtraService
    {
        Faculty AddFaculty(string name);

        TrainingGroup AddTrainingGroupToFaculty(string nameOfTrainingGroup, Faculty faculty);

        void StudentDeleteInTrainingGroup(Student student, TrainingGroup ognp);

        void StudentEntryToTrainingGroup(Student student, TrainingGroup ognp);

        Group AddGroupInFaculty(string name, Faculty faculty);

        Student AddStudentToGroup(string name, Group group);

        List<Stream> GetStreamOfOgnp(TrainingGroup ognp);

        List<Student> GetStudentsInStreamOfOgnp(Stream stream);

        List<Student> GetStudentsNotSignForOgnpInGroup(Group @group);
    }
}