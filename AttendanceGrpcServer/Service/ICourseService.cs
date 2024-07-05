using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Room;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface ICourseService
    {
        CourseDTO Get(int id);
        List<CourseDTO> List();

        CourseDTO Add(Course course);
        CourseDTO Delete(int id);

        CourseDTO Update(Course course);
        Response<CourseDTO> AddCourse(CourseDTORequest request);

        String AddByCSV(List<CourseDTORequest> coursesList);
    }
}
