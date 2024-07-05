using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public class SubjectRepository : ISubjectRepository
    {
        private MyDBContext dBContext;

        public SubjectRepository(MyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Subject Add(Subject subject)
        {
            dBContext.Subjects.Add(subject);
            dBContext.SaveChanges();
            return subject;
        }

        public Subject Delete(int id)
        {
            Subject subject = dBContext.Subjects.FirstOrDefault(s => s.Id == id);
            if (subject != null)
            {
                dBContext.Subjects.Remove(subject);
                dBContext.SaveChanges();
            }
            return subject;

        }

        public Subject Get(int id)
        {
            return dBContext.Subjects.FirstOrDefault(s => s.Id == id);
        }

        public IQueryable<Subject> List()
        {
            return dBContext.Subjects.AsQueryable();
        }

        public Subject Update(Subject subject)
        {
            Subject update = dBContext.Subjects.FirstOrDefault(s => s.Id == subject.Id);
            if (subject != null)
            {
                dBContext.Subjects.Add(subject);
                dBContext.Entry(subject).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
            return update;
        }
    }
}
