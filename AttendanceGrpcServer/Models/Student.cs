namespace AttendanceGrpcServer.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }

        public string? Email { get; set; }

        public ICollection<StudentCourse>? StudentCourses { get; set; }
        public ICollection<StudentSchedule>? StudentSchedules { get; set; }

    }
}
