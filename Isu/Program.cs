using System.Collections.Generic;
using Isu.Objects;
using Isu.Services;

namespace Isu
{
    internal class Program
    {
        private static void Main()
        {
            var isu = new IsuService();
            Group group = isu.AddGroup("M3207");
            Group group2 = isu.AddGroup("M3201");
            Student first = isu.AddStudent(group, "Карепин Денис");
            first.GetInfo();
            isu.ChangeStudentGroup(first, group2);
            first.GetInfo();
            group.GetInfo();
            group2.GetInfo();
        }
    }
}
