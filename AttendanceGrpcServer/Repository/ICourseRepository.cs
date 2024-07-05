using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface ICourseRepository
    {
        public IQueryable<Course> List();
        public Course Get(int id);

        public Course Add(Course course);
        public Course Delete(int id);
        public Course Update(Course course);    
    }
}
