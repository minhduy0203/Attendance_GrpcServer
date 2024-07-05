using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public class CourseRepository : ICourseRepository
    {

        private MyDBContext _dbContext;

        public CourseRepository(MyDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Course Add(Course course)
        {
            _dbContext.Add(course);
            _dbContext.SaveChanges();
            return course;
        }

        public Course Delete(int id)
        {
            Course delete = _dbContext.Courses.FirstOrDefault((c) => c.Id == id);
            if (delete != null)
            {

                _dbContext.Remove(delete);
                _dbContext.SaveChanges();
            }
            return delete;

        }

        public Course Get(int id)
        {
            return _dbContext.Courses.FirstOrDefault((c) => c.Id == id);
        }

        public IQueryable<Course> List()
        {
            return _dbContext.Courses.AsQueryable();
        }

        public Course Update(Course course)
        {
            Course update = _dbContext.Courses.FirstOrDefault((c) => c.Id == course.Id);
            if (update != null)
            {

                _dbContext.Entry(update).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
                _dbContext.Courses.Update(course);
                _dbContext.SaveChanges();

            }
            return update;
        }
    }
}
