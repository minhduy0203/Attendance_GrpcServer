using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Dto.StudentSchedules
{
    public class StudentSchedulesDTO
    {
        public int StudentId { get; set; }

        public StudentDTO Student { get; set; }
        public int ScheduleId { get; set; }
        public ScheduleDTO Schedule { get; set; }
        public Status Status { get; set; }
    }
}
