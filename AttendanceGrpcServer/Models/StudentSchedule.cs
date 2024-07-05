using System.ComponentModel;

namespace AttendanceGrpcServer.Models
{
    public class StudentSchedule
    {
        public int StudentId { get; set; }
        public int ScheduleId { get; set; }

        [DefaultValue(Status.NotYet)]
        public Status Status { get; set; }
        public Student? Student { get; set; }
        public Schedule? Schedule { get; set; }

       
    }
    public enum Status
    {
        NotYet,
        Absent,
        Attended
    }
}
