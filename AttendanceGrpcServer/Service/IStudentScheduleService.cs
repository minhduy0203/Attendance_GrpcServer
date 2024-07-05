using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.StudentSchedules;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
	public interface IStudentScheduleService
	{
		StudentSchedulesDTO Get(int studentId, int scheduleId);
		List<StudentSchedulesDTO> List();

		StudentSchedulesDTO Add(StudentSchedulesDTO studentSchedule);
		StudentSchedulesDTO Delete(int studentId, int scheduleId);

		StudentSchedulesDTO Update(StudentSchedulesDTO studentSchedule);

		Response<StudentSchedulesDTO> UpdateAttendance(int studentId, int scheduleId, bool attended);

		Response<List<StudentSchedulesDTO>> AddListAttendanceStudent(StudentScheduleListDto request);

		List<AttendanceDto> GetListByCourseIdAndStudentId(int sid, int cid);

	}
}
