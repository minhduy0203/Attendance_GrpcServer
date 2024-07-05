using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface IStudentService
    {
        StudentDTO Get(int id);
        List<StudentDTO> List();

        StudentDTO Add(StudentDTO student);
        StudentDTO Delete(int id);

        StudentDTO Update(StudentDTO student);
    }
}
