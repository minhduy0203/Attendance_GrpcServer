using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public class TeacherRepository : ITeacherRepository
    {
        private MyDBContext dBContext;

        public TeacherRepository(MyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Teacher Add(Teacher teacher)
        {
            dBContext.Add(teacher);
            dBContext.SaveChanges();
            return teacher;
        }

        public Teacher Delete(int id)
        {
            Teacher teacher = dBContext.Teachers.FirstOrDefault(t => t.Id == id);
            if (teacher != null)
            {
                dBContext.Teachers.Remove(teacher);
                dBContext.SaveChanges();
            }
            return teacher;
        }

        public Teacher Get(int id)
        {
            return dBContext.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public IQueryable<Teacher> List()
        {
            return dBContext.Teachers.AsQueryable();
        }

        public Teacher Update(Teacher teacher)
        {
            dBContext.Entry<Teacher>(teacher).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return teacher;
        }
    }
}
