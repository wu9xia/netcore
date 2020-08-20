using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Model;

namespace WebApplication2.Repository
{
    public interface IStudentRepository
    {
        List<Student> List();

        Student Get(int id);

        bool Add(Student student);

        bool Update(Student student);

        bool Delete(int id);
    }
}
