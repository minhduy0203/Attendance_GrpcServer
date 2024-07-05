using AttendanceGrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Repository
{
    public class StudentCourseRepository : IStudentCourseRepository
    {
        private MyDBContext dBContext;

        public StudentCourseRepository(MyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public StudentCourse Add(StudentCourse studentCourse)
        {
            dBContext.StudentCourses.Add(studentCourse);
            dBContext.SaveChanges();
            return studentCourse;
        }

        public StudentCourse Delete(int studentId, int courseId)
        {
            StudentCourse sc = dBContext.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
            if (sc != null)
            {
                dBContext.StudentCourses.Remove(sc);
                dBContext.SaveChanges();
            }
            return sc;
        }

        public StudentCourse Get(int studentId, int courseId)
        {
            return dBContext.StudentCourses
                .Include(sc => sc.Student)
                .ThenInclude(s => s.StudentSchedules)
                .ThenInclude(s => s.Schedule)
                .ThenInclude(ss => ss.Course)
                .Include(sc => sc.Course)
                .FirstOrDefault(sc => sc.StudentId == studentId && sc.CourseId == courseId);
        }

        public IQueryable<StudentCourse> List()
        {
            return dBContext.StudentCourses
                .Include(sc => sc.Student)
                .Include(sc => sc.Course)
                .AsQueryable();
        }

        public StudentCourse Update(StudentCourse studentCourse)
        {
            StudentCourse sc = dBContext.StudentCourses.FirstOrDefault(sc => sc.StudentId == studentCourse.StudentId && sc.CourseId == studentCourse.CourseId);
            if (sc != null)
            {
                sc.StudentId = studentCourse.Student.Id;
                sc.CourseId = studentCourse.Course.Id;
            }
            return sc;
        }
    }
}
