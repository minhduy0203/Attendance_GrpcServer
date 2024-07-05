namespace AttendanceGrpcServer.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int Slot { get; set; }
        public DateTime Date { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public ICollection<StudentSchedule>? StudentSchedules { get; set; }


    }
}
