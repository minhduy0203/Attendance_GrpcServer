namespace AttendanceGrpcServer.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public int? StudentId { get; set; }

        public Student? Student { get; set; }
    }
}
