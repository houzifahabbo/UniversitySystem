using Microsoft.Extensions.Logging;
using University.Core.DTOs;
using University.Core.Exceptions;
using University.Core.Forms;
using University.Core.Validations;
using University.Data.Entities;
using University.Data.Repositories;

namespace University.Core.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ILogger<StudentService> _logger;
        public StudentService(IStudentRepository studentRepository, ILogger<StudentService> logger)
        {
            _studentRepository = studentRepository;
            _logger = logger;
        }
        public void Create(AddStudentForm form)
        {
            if (form == null) throw new ArgumentNullException(nameof(form));

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var student = new Student()
            {
                Name = form.Name,
                Email = form.Email
            };

            _studentRepository.Add(student);
            _studentRepository.SaveChanges();
            _logger.LogInformation("Student Created");

        }

        public void Delete(int id)
        {
            var studet = _studentRepository.GetById(id);
            if (studet == null) throw new NotFoundException("Student not found");
            _studentRepository.Delete(studet);
            _studentRepository.SaveChanges();
            _logger.LogInformation($"Student with id {id} Deleted");
        }

        public List<StudentDTO> GetAll()
        {
            var students = _studentRepository.GetAll();
            _logger.LogInformation("Queried All Students");

            return students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
            }).ToList();
        }

        public StudentDTO GetById(int id)
        {
            var student = _studentRepository.GetById(id);
            _logger.LogInformation($"Queried Course with id {id}");
            if (student == null) throw new NotFoundException("Student not found");
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

            var validation = FormValidator.Validate(form);
            if (!validation.IsValid)
                throw new BusinessException(validation.Errors);

            var student = _studentRepository.GetById(id);
            if (student == null) throw new NotFoundException("Student not found");

            student.Name = form.Name;

            _studentRepository.Update(student);
            _studentRepository.SaveChanges();
            _logger.LogInformation($"Updated Student with id {id}");

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