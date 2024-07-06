using AttendanceGrpcServer.Dto.StudentCourse;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Services
{
    public class StudentCourseService : StudentCourser.StudentCourserBase
    {
        private IStudentCourseRepository studentCourseRepository;
        private IMapper mapper;

        public StudentCourseService(IStudentCourseRepository studentCourseRepository, IMapper mapper)
        {
            this.studentCourseRepository = studentCourseRepository;
            this.mapper = mapper;
        }

        public override Task<ListStudentCourse> GetListStudentCourse(StudentCourseRequest request, ServerCallContext context)
        {
            List<StudentCourse> studentCourses = studentCourseRepository.List()
                   .Include(sc => sc.Course)
                 .Where(sc => sc.StudentId == request.Id)
                 .ToList();

            List<StudentCourseResponse> list = mapper.Map<List<StudentCourse>, List<StudentCourseResponse>>(studentCourses);
            ListStudentCourse result = new ListStudentCourse() { Message = "Successfully" };
            result.Result.AddRange(list);
            return Task.FromResult(result);
        }
    }
}
