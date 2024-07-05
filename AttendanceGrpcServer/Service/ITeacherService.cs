using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Teacher;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface ITeacherService
    {
        TeacherDTO Get(int id);
        List<TeacherDTO> List();

        TeacherDTO Add(TeacherDTO teacher);
        TeacherDTO Delete(int id);

        TeacherDTO Update(TeacherDTO teacher);
    }
}
