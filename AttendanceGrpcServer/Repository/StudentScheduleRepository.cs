using AttendanceGrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Repository
{
	public class StudentScheduleRepository : IStudentScheduleRepository
	{
		private MyDBContext dBContext;

		public StudentScheduleRepository(MyDBContext dBContext)
		{
			this.dBContext = dBContext;
		}

		public StudentSchedule Add(StudentSchedule studentSchedule)
		{
			dBContext.StudentSchedules.Add(studentSchedule);
			dBContext.SaveChanges();
			return studentSchedule;
		}

		public StudentSchedule Delete(int studentId, int scheduleId)
		{
			StudentSchedule studentSchedule = dBContext.StudentSchedules.FirstOrDefault(ss => ss.StudentId == studentId && ss.ScheduleId == scheduleId);
			if (studentSchedule != null)
			{
				dBContext.StudentSchedules.Remove(studentSchedule);
				dBContext.SaveChanges();
			}
			return studentSchedule;
		}

		public StudentSchedule Get(int studentId, int scheduleId)
		{
			return dBContext.StudentSchedules
				.Include(ss => ss.Schedule)
				.FirstOrDefault(ss => ss.StudentId == studentId && ss.ScheduleId == scheduleId);
		}

		public IQueryable<StudentSchedule> List()
		{
			return dBContext.StudentSchedules.AsQueryable();

		}

		public StudentSchedule Update(StudentSchedule studentSchedule)
		{
			StudentSchedule update = dBContext.StudentSchedules.FirstOrDefault(ss => ss.StudentId == studentSchedule.StudentId && ss.ScheduleId == studentSchedule.ScheduleId);
			if (studentSchedule != null)
			{
				dBContext.StudentSchedules.Add(studentSchedule);
				dBContext.Entry(studentSchedule).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
				dBContext.SaveChanges();
			}
			return studentSchedule;
		}

		public StudentSchedule UpdateStudentAttendance(int studentId, int scheduleId, Status status)
		{
			StudentSchedule update = dBContext.StudentSchedules.FirstOrDefault(ss => ss.StudentId == studentId && ss.ScheduleId == scheduleId);
			if (update != null)
			{
				update.Status = status;
				dBContext.SaveChanges();
			}
			return update;
		}

		public List<StudentSchedule> UpdateListStudentAttendance(List<int> sid, int scheduleId, List<Status> statuses)
		{
			List<StudentSchedule> studentSchedules = new List<StudentSchedule>();
			int size = sid.Count;
			for (int i = 0; i < size; i++)
			{
				StudentSchedule update = dBContext.StudentSchedules.FirstOrDefault(ss => ss.StudentId == sid[i] && ss.ScheduleId == scheduleId);
				if (update != null)
				{
					update.Status = statuses[i];
					dBContext.SaveChanges();
					studentSchedules.Add(update);
				} 

			}

			return studentSchedules;
		}
	}
}
