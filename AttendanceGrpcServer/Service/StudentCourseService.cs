using AttendanceGrpcServer.Dto.StudentCourse;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace AttendanceGrpcServer.Service
{
	public class StudentCourseService : IStudentCourseService
	{
		private IStudentCourseRepository studentCourseRepository;
		private IMapper mapper;

		public StudentCourseService(IStudentCourseRepository studentCourseRepository, IMapper mapper)
		{
			this.studentCourseRepository = studentCourseRepository;
			this.mapper = mapper;
		}

		public StudentCourseDTO Add(StudentCourse sc)
		{
			StudentCourse add = studentCourseRepository.Add(sc);
			return mapper.Map<StudentCourse, StudentCourseDTO>(add);
		}

		public StudentCourseDTO Delete(int sid, int cid)
		{
			StudentCourse delete = studentCourseRepository.Delete(sid, cid);
			return mapper.Map<StudentCourse, StudentCourseDTO>(delete);
		}

		public StudentCourseDTO Get(int sid, int cid)
		{
			StudentCourse get = studentCourseRepository.Get(sid, cid);
			return mapper.Map<StudentCourse, StudentCourseDTO>(get);
		}

		public List<StudentCourseDTO> List()
		{
			List<StudentCourse> studentCourses = studentCourseRepository.List().ToList();
			return mapper.Map<List<StudentCourse>, List<StudentCourseDTO>>(studentCourses);

		}

		public StudentCourseDTO Update(StudentCourse sc)
		{
			StudentCourse update = studentCourseRepository.Update(sc);
			return mapper.Map<StudentCourse, StudentCourseDTO>(update);
		}

		public List<StudentCourseDTO> ListByStudentId(int id)
		{
			List<StudentCourse> studentCourses = studentCourseRepository.List()
				.Where(sc => sc.StudentId == id)
				.Include(sc => sc.Course)
				.ToList();
			return mapper.Map<List<StudentCourse>, List<StudentCourseDTO>>(studentCourses);
		}
	}
}
