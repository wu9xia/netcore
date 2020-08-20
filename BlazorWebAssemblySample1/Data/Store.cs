using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using stu = BlazorWebAssemblySample1.Model;

namespace BlazorWebAssemblySample1.Data
{
    public class Store
    {
        private List<stu.Student> _students;

        public void SetStudents(List<stu.Student> list)
        {
            _students = list;
        }

        public List<stu.Student> GetStudents()
        {
            return _students;
        }

        public stu.Student GetStudentById(int id)
        {
            var stu = _students?.FirstOrDefault(s => s.Id == id);

            return stu;
        }
    }
}
