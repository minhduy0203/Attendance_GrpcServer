using AttendanceGrpcServer;
using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Room;
using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Dto.StudentCourse;
using AttendanceGrpcServer.Dto.StudentSchedules;
using AttendanceGrpcServer.Dto.Subject;
using AttendanceGrpcServer.Dto.Teacher;
using AttendanceGrpcServer.Models;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;

namespace AttendanceMananagmentProject.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Course, CourseDTO>();
            CreateMap<Room, RoomDTO>();
            CreateMap<Teacher, TeacherDTO>();
            //.ForMember(dest => dest.Schedules, opt => opt.MapFrom(source => source.Schedules.Select(s => new ScheduleDTO
            //{
            //    Course = new CourseDTO { Code = s.Course.Code, Name = s.Course.Name },
            //    CourseId = s.CourseId,
            //    Id = s.Id,
            //}

            //    )));


            CreateMap<Student, StudentDTO>().ForMember(dest => dest.StudentSchedules,
                opt => opt.MapFrom(src => src.StudentSchedules.Select(ss => new AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO
                {
                    ScheduleId = ss.ScheduleId,
                    Status = ss.Status,
                    StudentId = ss.StudentId,
                })));
            CreateMap<Schedule, ScheduleDTO>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new CourseDTO
                {
                    Code = src.Course.Code,
                    Name = src.Course.Name,
                    StartDate = src.Course.StartDate,
                    TimeSlot = src.Course.TimeSlot,
                    //Subject = new SubjectDTO { Name = src.Course.Subject.Name}


                }))
                .ForMember(dest => dest.StudentSchedules, opt => opt.MapFrom(src => src.StudentSchedules.Select(ss => new AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO { ScheduleId = ss.ScheduleId, StudentId = ss.StudentId, Student = new StudentDTO { Name = ss.Student.Name, Code = ss.Student.Code, Id = ss.Student.Id, Email = ss.Student.Email }, Status = ss.Status })));
            CreateMap<Subject, SubjectDTO>();
            CreateMap<StudentCourse, StudentCourseDTO>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => new StudentDTO
                {
                    Id = src.StudentId,
                    Code = src.Student.Code,
                    Name = src.Student.Name,
                    Email = src.Student.Email,
                    StudentSchedules = src.Student.StudentSchedules.Select(ss => new AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO { ScheduleId = ss.ScheduleId, Status = ss.Status, Schedule = new ScheduleDTO { Date = ss.Schedule.Date, Slot = ss.Schedule.Slot, } }).ToList()
                }))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new CourseDTO
                {
                    Code = src.Course.Code,
                    Name = src.Course.Name,
                    StartDate = src.Course.StartDate,
                    EndDate = src.Course.EndDate,

                }))
                ;
            CreateMap<StudentSchedule, AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO>();
            CreateMap<StudentSchedule, AttendanceGrpcServer.Dto.StudentSchedules.AttendanceDto>();


            CreateMap<StudentDTO, Student>()
               .ForMember(dest => dest.StudentSchedules, opt => opt.Condition(src => src.StudentSchedules != null && src.StudentSchedules?.Count != 0));

            CreateMap<RoomDTO, Room>();
            CreateMap<TeacherDTO, Teacher>();
            CreateMap<SubjectDTO, Subject>();
            CreateMap<AttendanceGrpcServer.Dto.StudentSchedules.StudentSchedulesDTO, StudentSchedule>();

            //Grpc
            
            CreateMap<Schedule, ScheduleResponse>()
                    .ForMember(dest => dest.StudentSchedules, opt => opt.Ignore())
                    .ForMember(dest => dest.Date, opt => opt.MapFrom(src => Timestamp.FromDateTime(src.Date.ToLocalTime().ToUniversalTime())))
                    .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new CourseDto
                    {
                        Code = src.Course.Code,
                        Name = src.Course.Name,
                        StartDate = Timestamp.FromDateTime(src.Course.StartDate.ToLocalTime().ToUniversalTime()),
                        TimeSlot = src.Course.TimeSlot,
                    }))
                    .ForMember(dest => dest.Room , opt=> opt.MapFrom(src => new RoomDto { Id = src.RoomId , Name = src.Room.Name}))
                    ;

            CreateMap<StudentSchedule, AttendanceGrpcServer.StudentSchedulesDTO>();

            CreateMap<StudentSchedule, AttendanceGrpcServer.AttendanceDto>();
            CreateMap<StudentCourse, StudentCourseResponse>()
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => new CourseDto
				{
					Code = src.Course.Code,
					Name = src.Course.Name,
					StartDate = Timestamp.FromDateTime(src.Course.StartDate.ToLocalTime().ToUniversalTime()),
					TimeSlot = src.Course.TimeSlot,
				}));

        }
    }
}
