namespace AttendanceGrpcServer.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public ICollection<Schedule>? Schedules { get; set; }


    }
}
