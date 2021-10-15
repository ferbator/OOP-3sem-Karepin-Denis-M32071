using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Objects;
using IsuExtra.Tools;
using Group = IsuExtra.Objects.Group;
using Student = IsuExtra.Objects.Student;

namespace IsuExtra.Services
{
    public class IsuExtraService : IIsuExtraService
    {
        private readonly List<Faculty> _faculties;

        public IsuExtraService(List<Faculty> faculties)
        {
            _faculties = faculties;
        }

        public IsuExtraService()
        {
            _faculties = new List<Faculty>();
        }

        public void GetInfo()
        {
            foreach (Faculty faculty in _faculties)
            {
                faculty.GetInfo();
                Console.WriteLine();
            }
        }

        public Faculty AddFaculty(string name)
        {
            if (_faculties.Any(faculty => faculty.Name == name))
            {
                throw new IsuExtraException("Faculty alreagy added");
            }

            var tmp = new Faculty(name);
            _faculties.Add(tmp);
            return tmp;
        }

        public Teacher AddTeacher(string name)
        {
            var tmp = new Teacher(name);
            return tmp;
        }

        public TrainingGroup AddTrainingGroupToFaculty(string nameOfTrainingGroup, Faculty faculty)
        {
            var tmp = new TrainingGroup(nameOfTrainingGroup);
            faculty.AddTrainingGroup(tmp);
            tmp.AddFaculty(faculty);
            return tmp;
        }

        public void StudentEntryToTrainingGroup(Student student, TrainingGroup ognp)
        {
            if (student.Group.Faculty.Name == ognp.Faculty.Name) throw new IsuExtraException("Trying to enroll on the ognp of your faculty");
            ognp.AddStudentToStream(student);
        }

        public void StudentDeleteInTrainingGroup(Student student, TrainingGroup ognp)
        {
           student.DeleteDataOfOgnp(ognp);
           ognp.DeleteStudentToStream(student);
        }

        public Group AddGroupInFaculty(string name, Faculty faculty)
        {
            if (_faculties.Any(faculty1 => !name.StartsWith(faculty1.Name))) throw new IsuExtraException("Unknown faculty");
            if (name.Length != 5) throw new IsuExtraException("Incorrect group");
            return faculty.AddGroup(name);
        }

        public Student AddStudentToGroup(string name, Group group)
        {
            var student = new Student(name);
            group.PlusStudent(student);
            student.AddGroup(group);
            student.AddPersonalSchedule(group.GroupSchedule);

            return student;
        }

        public List<Stream> GetStreamOfOgnp(TrainingGroup ognp)
        {
            return ognp.GetStreams();
        }

        public List<Student> GetStudentsInStreamOfOgnp(Stream stream)
        {
            return stream.GetStudents();
        }

        public List<Student> GetStudentsNotSignForOgnpInGroup(Group @group)
        {
            return @group.GetStudentsNotSignForOgnp();
        }

        public bool FindTrainingGroupInFaculties(TrainingGroup trainingGroup)
        {
            return _faculties.Any(faculty => faculty.FindTrainingGroup(trainingGroup));
        }
    }
}