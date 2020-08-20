using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Model;
using WebApplication2.Repository;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        [HttpGet]
        public List<Student> Get()
        {
            return _studentRepository.List();
        }

        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return _studentRepository.Get(id);
        }

        [HttpPost]
        public Student Post(Student model)
        {
            _studentRepository.Add(model);

            return model;
        }

        [HttpPut]
        public Student Put(Student model)
        {
            _studentRepository.Update(model);

            return model;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _studentRepository.Delete(id);
        }
    }
}
