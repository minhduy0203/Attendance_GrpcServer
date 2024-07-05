using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface IScheduleRepository
    {

        public IQueryable<Schedule> List();
        public Schedule Get(int id);

        public Schedule Add(Schedule schedule);
        public Schedule Delete(int id);
        public Schedule Update(Schedule schedule);
    }
}
