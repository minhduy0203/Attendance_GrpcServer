using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore.Storage;
using AttendanceGrpcServer.Utils;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Google.Protobuf;

namespace AttendanceGrpcServer.Services
{
	public class CourseService : Courser.CourserBase
	{
		private ICourseRepository courseRepository;
		private IMapper mapper;
		private ISubjectRepository subjectRepository;
		private IScheduleRepository scheduleRepository;
		private IStudentScheduleRepository studentScheduleRepository;
		private IStudentCourseRepository studentCourseRepository;
		private MyDBContext context;
		public CourseService(ICourseRepository courseRepository, IMapper mapper, ISubjectRepository subjectRepository, IScheduleRepository scheduleRepository, IStudentScheduleRepository studentScheduleRepository, IStudentCourseRepository studentCourseRepository, MyDBContext context)
		{
			this.courseRepository = courseRepository;
			this.mapper = mapper;
			this.subjectRepository = subjectRepository;
			this.scheduleRepository = scheduleRepository;
			this.studentScheduleRepository = studentScheduleRepository;
			this.studentCourseRepository = studentCourseRepository;
			this.context = context;
		}

		public override Task<ImportCourseResponse> ImportCourse(ImportCourseRequest request, ServerCallContext context)
		{
			Stream stream = new MemoryStream(request.Data.ToByteArray());
			List<CourseDTORequest> requests = CourseLogic.GetValidCourses(stream);
			// return message
			var csv = new StringBuilder();
			var header = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Code", "Name", "Subject", "Students", "Teacher", "RoomId", "StartDate", "EndDate", "TimeSlot", "Message");
			csv.AppendLine(header);
			foreach (CourseDTORequest courseDto in requests)
			{
				//Courses that have same teacherid or roomid with request
				List<Course> courses = courseRepository
					.List()
					.Include(c => c.Schedules)
					.Where(c => c.Schedules.Any(s => s.TeacherId == courseDto.TeacherId || s.RoomId == courseDto.RoomId))
					.ToList();

				//Courses of all students in requests;
				List<StudentCourse> studentCourses = studentCourseRepository
					.List()
					.Where(sc => courseDto.Students.Any(number => number == sc.StudentId))
					.Include(sc => sc.Course)
					.ToList();
				;


				try
				{
					//validate course
					Validator.ValidateCourse(courseDto, courses, studentCourses);

					//create course
					Course course = new Course()
					{
						SubjectId = courseDto.SubjectId,
						StartDate = courseDto.StartDate,
						EndDate = courseDto.EndDate,
						TimeSlot = courseDto.TimeSlot,
						Code = courseDto.Code,
						Name = courseDto.Name,
					};

					Course add = courseRepository.Add(course);


					//create studentcourse

					foreach (int sid in courseDto.Students)
					{
						StudentCourse sc = new StudentCourse { StudentId = sid, CourseId = add.Id };
						studentCourseRepository.Add(sc);
					}

					//create schedule Timeslot A24
					int daysUntilMonday = ((int)DayOfWeek.Monday - (int)course.StartDate.DayOfWeek + 7) % 7;
					DateTime current = course.StartDate.AddDays(daysUntilMonday);

					Subject subject = subjectRepository.Get(course.SubjectId);
					int totalWeek = (int)Math.Ceiling(subject.NumberSlot / 2.0);
					DateTime endDate = current.AddDays(totalWeek * 7);
					course.EndDate = endDate;

					List<Schedule> schedules = new List<Schedule>();
					int remainSlot = subject.NumberSlot;


					while (current.Subtract(endDate).TotalDays <= 0)
					{
						if (remainSlot == 0)
						{
							break;
						}
						Schedule s = new Schedule();
						int dayofweek = (int)current.DayOfWeek + 1;
						int timeslot = courseDto.TimeSlot.IndexOf(dayofweek.ToString());
						if (timeslot >= 1)
						{
							if (courseDto.TimeSlot.StartsWith("A"))
							{

							}
							else
							{
								timeslot = timeslot + 2;
							}

							s.Slot = timeslot;
							s.Date = current;
							s.CourseId = add.Id;
							s.TeacherId = courseDto.TeacherId;
							s.RoomId = courseDto.RoomId;
							Schedule scheduleAdd = scheduleRepository.Add(s);
							//create attendance

							foreach (int sid in courseDto.Students)
							{
								StudentSchedule studentSchedule = new StudentSchedule { ScheduleId = scheduleAdd.Id, StudentId = sid };
								studentScheduleRepository.Add(studentSchedule);

							}
							remainSlot--;

						}
						current = current.AddDays(1);
					}

					csv.AppendLine(CourseLogic.GetCourseCSV(courseDto, "Add successfully"));

				}
				catch (Exception ex)
				{
					csv.AppendLine(CourseLogic.GetCourseCSV(courseDto, ex.Message));
					continue;

				}
			}


			return Task.FromResult(new ImportCourseResponse
			{
				FileName = "CourseAdd",
				Data = ByteString.CopyFromUtf8(csv.ToString())
			});
		}



	}
}

