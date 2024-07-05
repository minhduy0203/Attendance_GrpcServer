using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Room;
using AttendanceGrpcServer.Dto.StudentSchedules;
using AttendanceGrpcServer.Dto.Teacher;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Dto.Schedule
{
    public class ScheduleDTO
    {
        public int Id { get; set; }
        public int Slot { get; set; }
        public DateTime Date { get; set; }

        public int CourseId { get; set; }
        public CourseDTO Course { get; set; }
        public int TeacherId { get; set; }
        public TeacherDTO Teacher { get; set; }

        public int RoomId { get; set; }

        public RoomDTO Room { get; set; }
        public ICollection<AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO>? StudentSchedules { get; set; }
    }
}
