using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface IStudentRepository
    {
        public IQueryable<Student> List();
        public Student Get(int id);

        public Student Add(Student student);
        public Student Delete(int student);
        public Student Update(Student student);
    }
}
