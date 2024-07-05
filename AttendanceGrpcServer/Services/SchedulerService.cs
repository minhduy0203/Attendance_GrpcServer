using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AttendanceGrpcServer.Service;
using AttendanceGrpcServer.Utils;
using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AttendanceGrpcServer.Services
{
    public class SchedulerService : Scheduler.SchedulerBase
    {
        private IScheduleRepository scheduleRepository;
        private IStudentRepository studentRepository;
        private IMapper mapper;
        public SchedulerService(IMapper mapper, IScheduleRepository scheduleRepository, IStudentRepository studentRepository)
        {

            this.mapper = mapper;
            this.scheduleRepository = scheduleRepository;
            this.studentRepository = studentRepository;
        }

        public override Task<ListScheduleResponse> GetStudentSchedule(ScheduleRequest request, ServerCallContext context)
        {
            DateTime from = ScheduleLogic.GetDateByWeek(request.Week, request.Year);
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
                .FirstOrDefault(s => s.Id == request.Id)
                .StudentSchedules
                .Where(ss => ss.Schedule.Date >= from && ss.Schedule.Date <= to)
                .Select(ss => ss.Schedule)
                .ToList();
            ;
            List<ScheduleResponse> list = mapper.Map<List<Schedule>, List<ScheduleResponse>>(schedules);

            for (int i = 0; i < list.Count; i++)
            {
                AttendanceGrpcServer.StudentSchedulesDTO ss = new StudentSchedulesDTO { Status = (int)schedules.ElementAt(i).StudentSchedules.ElementAt(0).Status };
                list.ElementAt(i).StudentSchedules.Add(ss);
            }

            ListScheduleResponse response = new ListScheduleResponse();
            response.Result.AddRange(list);
            return Task.FromResult(response);
        }

        public override Task<ListScheduleResponse> GetTeacherSchedule(ScheduleRequest request, ServerCallContext context)
        {
            DateTime from = ScheduleLogic.GetDateByWeek(request.Week, request.Year);
            DateTime to = from.AddDays(6);
            List<Schedule> schedules = scheduleRepository
                  .List()
                  .Include(s => s.Teacher)
                  .Include(s => s.Room)
                  .Where(s => s.TeacherId == request.Id && (s.Date >= from && s.Date <= to))
                  .ToList();

            List<ScheduleResponse> list = mapper.Map<List<Schedule>, List<ScheduleResponse>>(schedules);
            ListScheduleResponse response = new ListScheduleResponse();
            response.Result.AddRange(list);
            return Task.FromResult(response);
        }
    }
}
