using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Dto.StudentSchedules
{
	public class AttendanceDto
	{
	
		public int ScheduleId { get; set; }
		public ScheduleDTO Schedule { get; set; }
		public Status Status { get; set; }
	}
}
