using AttendanceGrpcServer.Dto.StudentSchedules;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Dto.Student
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public string? Email { get; set; }

        public ICollection<AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO>? StudentSchedules { get; set; }
    }
}
