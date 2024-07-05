using AttendanceGrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private MyDBContext dBContext;

        public ScheduleRepository(MyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Schedule Add(Schedule schedule)
        {
            dBContext.Schedules.Add(schedule);
            dBContext.SaveChanges();
            return schedule;
        }

        public Schedule Delete(int id)
        {
            Schedule delete = dBContext.Schedules.FirstOrDefault(r => r.Id == id);
            if (delete != null)
            {
                dBContext.Remove(delete);
                dBContext.SaveChanges();
            }
            return delete;
        }

        public Schedule Get(int id)
        {
            return dBContext.Schedules
                .Include(s => s.Teacher)
                .Include(s => s.StudentSchedules)
                .ThenInclude(ss => ss.Student)
                .Include(s => s.Course)
                .ThenInclude(c => c.Subject)
                .FirstOrDefault(r => r.Id == id);
        }

        public IQueryable<Schedule> List()
        {
            return dBContext.Schedules
                .Include(s => s.Teacher)
                .Include(s => s.Course)
                .ThenInclude(c => c.Subject)
                .AsQueryable();
        }

        public Schedule Update(Schedule schedule)
        {
            Schedule update = dBContext.Schedules.FirstOrDefault(r => r.Id == schedule.Id);
            if (update != null)
            {
                dBContext.Add(schedule);
                dBContext.Entry(schedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                dBContext.SaveChanges();
            }
            return update;
        }
    }
}
