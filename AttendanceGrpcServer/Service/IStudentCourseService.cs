using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.StudentCourse;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
	public interface IStudentCourseService
	{
		StudentCourseDTO Get(int sid, int cid);
		List<StudentCourseDTO> List();

		StudentCourseDTO Add(StudentCourse sc);
		StudentCourseDTO Delete(int sid, int cid);

		StudentCourseDTO Update(StudentCourse sc);
		List<StudentCourseDTO> ListByStudentId(int id);

	}
}
