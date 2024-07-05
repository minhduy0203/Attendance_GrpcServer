namespace AttendanceGrpcServer.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public string? Name { get; set; }

        public string TimeSlot { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int SubjectId { get; set; }
        public Subject Subject { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }
        public ICollection<StudentCourse>? StudentCourses { get; set; }


    }
}
