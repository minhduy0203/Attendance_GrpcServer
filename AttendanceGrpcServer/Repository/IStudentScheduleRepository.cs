using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface IStudentScheduleRepository
    {
        public IQueryable<StudentSchedule> List();
        public StudentSchedule Get(int studentId, int scheduleId);

        public StudentSchedule Add(StudentSchedule studentSchedule);
        public StudentSchedule Delete(int studentId, int scheduleId);
        public StudentSchedule Update(StudentSchedule studentSchedule);

        public StudentSchedule UpdateStudentAttendance(int studentId, int scheduleId, Status status);
        public List<StudentSchedule> UpdateListStudentAttendance(List<int> sid, int scheduleId, List<Status> statuses);

	}
}
