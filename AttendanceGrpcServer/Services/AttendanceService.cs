using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Services
{
	[Authorize(Roles = "TEACHER")]
	public class AttendanceService : Attendancer.AttendancerBase
    {
        private IStudentScheduleRepository studentScheduleRepository;
        private IScheduleRepository scheduleRepository;
        private IMapper mapper;

		public AttendanceService(IStudentScheduleRepository studentScheduleRepository, IMapper mapper, IScheduleRepository scheduleRepository)
		{
			this.studentScheduleRepository = studentScheduleRepository;
			this.mapper = mapper;
			this.scheduleRepository = scheduleRepository;
		}

		public override Task<AttendanceListResponse> AttendStudents(AttendanceListRequest request, ServerCallContext context)
        {
            List<int> sids = new List<int>();
            List<AttendanceGrpcServer.Models.Status> statuses = new List<AttendanceGrpcServer.Models.Status>();
            foreach (var item in request.Sid)
            {
                sids.Add(item);
            }

            foreach (var item in request.Statuses)
            {
                statuses.Add((AttendanceGrpcServer.Models.Status) item);
            }
            List<StudentSchedule> result = studentScheduleRepository.UpdateListStudentAttendance(sids, request.ScheduleId, statuses);
            List<StudentSchedulesDTO> response = mapper.Map<List<StudentSchedule>, List<StudentSchedulesDTO>>(result);

            return Task.FromResult(new AttendanceListResponse
            {
                Message = "Add attendance successfullly",
            });

        }

		public override Task<ScheduleResponse> GetSchedule(GetScheduleRequest request, ServerCallContext context)
		{
			Schedule schedule = scheduleRepository.List()
				.Include(s => s.StudentSchedules)
				.ThenInclude(ss => ss.Student)
				.FirstOrDefault(s => s.Id == request.Id);
			ScheduleResponse response = mapper.Map<Schedule, ScheduleResponse>(schedule);
			List<StudentSchedulesDTO> list = new List<StudentSchedulesDTO>();
			foreach (StudentSchedule ss in schedule.StudentSchedules)
			{
				list.Add(new StudentSchedulesDTO
				{
					Student = new StudentDto { Code = ss.Student.Code, Name = ss.Student.Name, Id = ss.Student.Id },
					Status = (int)ss.Status,
					ScheduleId = ss.ScheduleId,
					StudentId = ss.StudentId,
				});
			}
			response.StudentSchedules.AddRange(list);
			return Task.FromResult(response);
		}
	}
}
