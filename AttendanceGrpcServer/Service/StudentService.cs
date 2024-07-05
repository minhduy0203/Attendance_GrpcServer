using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;

namespace AttendanceGrpcServer.Service
{
    public class StudentService : IStudentService
    {
        private IStudentRepository studentRepository;
        private IMapper mapper;

        public StudentService(IStudentRepository studentRepository, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.mapper = mapper;
        }

        public StudentDTO Add(StudentDTO student)
        {
            Student add = studentRepository.Add(mapper.Map<StudentDTO, Student>(student));
            return mapper.Map<Student, StudentDTO>(add);
        }

        public StudentDTO Delete(int id)
        {
            Student student = studentRepository.Delete(id);
            return mapper.Map<Student, StudentDTO>(student);

        }

        public StudentDTO Get(int id)
        {
            Student student = studentRepository.Get(id);
            return mapper.Map<Student, StudentDTO>(student);
        }

        public List<StudentDTO> List()
        {
            List<Student> students = studentRepository.List().ToList();
            return mapper.Map<List<Student>, List<StudentDTO>>(students);
        }

        public StudentDTO Update(StudentDTO student)
        {
            Student s = studentRepository.Get(student.Id);
            Student update = null;

            if (s != null)
            {
                mapper.Map<StudentDTO, Student>(student, s);
                update = studentRepository.Update(s);
            }

            return mapper.Map<Student, StudentDTO>(update);

        }
    }
}
