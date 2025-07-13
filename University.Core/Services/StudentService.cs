using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using University.Core.DTOs;
using University.Core.Forms;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public void Create(AddStudentForm form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (string.IsNullOrEmpty(form.Name)) throw new Exception("Name is required");
            if (string.IsNullOrEmpty(form.Email)) throw new Exception("Email is required");
            
            var student = new Student(){
                Name=form.Name, 
                Email=form.Email
            };

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();
        }

        public void Delete(int id)
        {
            var studet = _studentRepository.GetById(id);
            if (studet == null) throw new Exception("Student not found");
            _studentRepository.Delete(studet);
            _studentRepository.SaveChanges();
        }

        public List<StudentDTO> GetAll()
        {
            var students = _studentRepository.GetAll();
            return students.Select(s=>new StudentDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Email=s.Email,
            }).ToList();
        }

        public StudentDTO GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            if (student == null) throw new Exception("Student not found");
            return new StudentDTO()
            {
                Id = student.Id,
                Email = student.Email,
                Name = student.Name
            };
        }

        public void Update(int id, UpdateStudentForm form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));
            if (id < 0) throw new ArgumentNullException(nameof(id));
            if (string.IsNullOrEmpty(form.Name)) throw new Exception("Name is required");

            var student = _studentRepository.GetById(id);
            if (student == null) throw new Exception("Student not found");

            student.Name = form.Name;

            _studentRepository.Update(student);
            _studentRepository.SaveChanges();
        }
    }

    public interface IStudentService
    {
        List<StudentDTO> GetAll();
        StudentDTO GetById(int id);
        void Create(AddStudentForm form);
        void Update(int id, UpdateStudentForm form);
        void Delete(int id);
    }
}