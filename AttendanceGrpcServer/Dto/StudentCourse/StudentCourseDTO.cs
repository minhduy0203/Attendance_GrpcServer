using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Student;

namespace AttendanceGrpcServer.Dto.StudentCourse
{
    public class StudentCourseDTO
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }

        public StudentDTO? Student { get; set; }
        public CourseDTO? Course { get; set; }
    }
}
