using AttendanceGrpcServer.Dto.Subject;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Dto.Course
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public string TimeSlot { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int SubjectId { get; set; }
        public SubjectDTO Subject { get; set; }

    }
}
