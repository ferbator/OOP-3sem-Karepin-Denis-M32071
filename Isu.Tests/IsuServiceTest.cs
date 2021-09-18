using Isu.Objects;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            
            Group testGroup = _isuService.AddGroup("M3201");
            Student testStudent = _isuService.AddStudent(testGroup, "Карепин Денис");
            
            if (!testGroup.Students.Contains(testStudent) || testStudent.Group.Name != testGroup.Name)
                Assert.Fail();
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {// for CapacityForGroupOfStudent = 2
            Assert.Catch<IsuException>(() =>
            {
                Group testGroup = _isuService.AddGroup("M3209");
                _isuService.AddStudent(testGroup, "Долька Лимона");
                _isuService.AddStudent(testGroup, "Миска Каши");
                _isuService.AddStudent(testGroup, "Сердечный приступ");
            });
        }
        
        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                Group testGroup = _isuService.AddGroup("ieuiuifa1214124");

            });
        }
        
        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group testGroupFirst = _isuService.AddGroup("M3210");
            Group testGroupSecond = _isuService.AddGroup("M3201");
            Student testStudent = _isuService.AddStudent(testGroupFirst, "Новый Человек");
            
            _isuService.ChangeStudentGroup(testStudent, testGroupSecond);
            if (testStudent.Group.Name != testGroupSecond.Name || testGroupFirst.Students.Contains(testStudent) || !testGroupSecond.Students.Contains(testStudent))
                Assert.Fail();
        }
    }
}