using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AttendanceGrpcServer.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AttendanceGrpcServer.Service
{
	public class ScheduleService : IScheduleService
	{

		private IScheduleRepository scheduleRepository;
		private IStudentRepository studentRepository;
		private IMapper mapper;

		public ScheduleService(IScheduleRepository scheduleRepository, IMapper mapper, IStudentRepository studentRepository)
		{
			this.scheduleRepository = scheduleRepository;
			this.mapper = mapper;
			this.studentRepository = studentRepository;
		}

		public ScheduleDTO Add(Schedule schedule)
		{
			scheduleRepository.Add(schedule);
			return mapper.Map<Schedule, ScheduleDTO>(schedule);
		}

		public ScheduleDTO Delete(int id)
		{
			Schedule schedule = scheduleRepository.Delete(id);
			return mapper.Map<Schedule, ScheduleDTO>(schedule);
		}

		public ScheduleDTO Get(int id)
		{
			Schedule schedule = scheduleRepository.Get(id);
			return mapper.Map<Schedule, ScheduleDTO>(schedule);
		}

		public List<ScheduleDTO> List()
		{
			List<Schedule> schedules = scheduleRepository.List().ToList();
			return mapper.Map<List<Schedule>, List<ScheduleDTO>>(schedules);
		}

		public List<ScheduleDTO> ListStudentSchedule(int week, int year, int sid)
		{
			DateTime from = ScheduleLogic.GetDateByWeek(week, year);
			DateTime to = from.AddDays(6);
			List<Schedule> schedules = studentRepository
				.List()
				.Include(s => s.StudentCourses)
				.ThenInclude(s => s.Course)
				.Include(s => s.StudentSchedules)
				.ThenInclude(ss => ss.Schedule)
				.ThenInclude(ss => ss.Teacher)
				.Include(s => s.StudentSchedules)
				.ThenInclude(ss => ss.Schedule)
				.ThenInclude(ss => ss.Room)
				.FirstOrDefault(s => s.Id == sid)
				.StudentSchedules
				.Where(ss => ss.Schedule.Date >= from && ss.Schedule.Date <= to)
				.Select(ss => ss.Schedule)
				.ToList();
			;
			return mapper.Map<List<Schedule>, List<ScheduleDTO>>(schedules);

		}

		public List<ScheduleDTO> ListTeacherSchedule(int week, int year, int tid)
		{
			DateTime from = ScheduleLogic.GetDateByWeek(week, year);
			DateTime to = from.AddDays(6);
			List<Schedule> schedules = scheduleRepository
				  .List()
				  .Include(s => s.Teacher)
				  .Include(s => s.Room)
				  .Where(s => s.TeacherId == tid && (s.Date >= from && s.Date <= to))
				  .ToList();
			return mapper.Map<List<Schedule>, List<ScheduleDTO>>(schedules);

		}

		public ScheduleDTO Update(Schedule schedule)
		{
			Schedule update = scheduleRepository.Update(schedule);
			return mapper.Map<Schedule, ScheduleDTO>(update);
		}
	}
}
