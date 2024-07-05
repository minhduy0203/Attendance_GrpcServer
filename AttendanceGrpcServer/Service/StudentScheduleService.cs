using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Dto.StudentSchedules;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;

namespace AttendanceGrpcServer.Service
{
	public class StudentScheduleService : IStudentScheduleService
	{
		private IStudentScheduleRepository studentScheduleRepository;
		private IMapper mapper;

		public StudentScheduleService(IStudentScheduleRepository studentScheduleRepository, IMapper mapper)
		{
			this.studentScheduleRepository = studentScheduleRepository;
			this.mapper = mapper;
		}

		public StudentSchedulesDTO Add(StudentSchedulesDTO studentSchedule)
		{
			StudentSchedule add = studentScheduleRepository.Add(mapper.Map<StudentSchedulesDTO, StudentSchedule>(studentSchedule));
			return mapper.Map<StudentSchedule, StudentSchedulesDTO>(add);
		}

		public StudentSchedulesDTO Delete(int studentId, int scheduleId)
		{
			StudentSchedule studentSchedule = studentScheduleRepository.Delete(studentId, scheduleId);
			return mapper.Map<StudentSchedule, StudentSchedulesDTO>(studentSchedule);
		}

		public StudentSchedulesDTO Get(int studentId, int scheduleId)
		{
			StudentSchedule studentSchedule = studentScheduleRepository.Get(studentId, scheduleId);
			return mapper.Map<StudentSchedule, StudentSchedulesDTO>(studentSchedule);
		}

		public List<StudentSchedulesDTO> List()
		{
			List<StudentSchedule> studentSchedule = studentScheduleRepository.List().ToList();
			return mapper.Map<List<StudentSchedule>, List<StudentSchedulesDTO>>(studentSchedule);
		}

		public StudentSchedulesDTO Update(StudentSchedulesDTO studentSchedule)
		{
			StudentSchedule update = null;
			StudentSchedule find = studentScheduleRepository.Get(studentSchedule.StudentId, studentSchedule.ScheduleId);
			if (find != null)
			{
				mapper.Map<StudentSchedulesDTO, StudentSchedule>(studentSchedule, find);
				update = studentScheduleRepository.Update(find);
			}


			return mapper.Map<StudentSchedule, StudentSchedulesDTO>(update);
		}

		public Response<StudentSchedulesDTO> UpdateAttendance(int studentId, int scheduleId, bool attended)
		{
			StudentSchedule update;
			StudentSchedule studentSchedule = studentScheduleRepository.Get(studentId, scheduleId);
			try
			{
				if (studentSchedule.Schedule.Date < DateTime.Now)
					throw new Exception("Date in the past");

				if (attended)
				{
					update = studentScheduleRepository.UpdateStudentAttendance(studentId, scheduleId, Status.Attended);

				}
				else
				{
					update = studentScheduleRepository.UpdateStudentAttendance(studentId, scheduleId, Status.Absent);
				}

				StudentSchedulesDTO studentSchedulesDTO = mapper.Map<StudentSchedule, StudentSchedulesDTO>(update);
				return new Response<StudentSchedulesDTO>()
				{
					Data = studentSchedulesDTO,
					Message = "Add attendance successfully"
				};
			}

			catch (Exception ex)
			{
				return new Response<StudentSchedulesDTO>()
				{
					Data = null,
					Message = ex.Message
				};

			}


		}

		public Response<List<StudentSchedulesDTO>> AddListAttendanceStudent(StudentScheduleListDto request)
		{

			List<StudentSchedule> result = studentScheduleRepository.UpdateListStudentAttendance(request.StudentId, request.ScheduleId, request.Statuses);
			List<StudentSchedulesDTO> response = mapper.Map<List<StudentSchedule>, List<StudentSchedulesDTO>>(result);
			return new Response<List<StudentSchedulesDTO>>
			{
				Data = response,
				Message = "Add successfully"
			};

		}

		public List<AttendanceDto> GetListByCourseIdAndStudentId(int sid, int cid)
		{
			List<StudentSchedule> list = studentScheduleRepository.List()
				.Include(ss => ss.Schedule)
				.Include(ss => ss.Student)
				.Where(ss => ss.StudentId == sid && ss.Schedule.CourseId == cid)
				.ToList();
			return mapper.Map<List<StudentSchedule>, List<AttendanceDto>>(list);

		}


	}
}
