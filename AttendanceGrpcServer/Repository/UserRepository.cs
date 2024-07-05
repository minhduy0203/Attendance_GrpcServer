using AttendanceGrpcServer.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly MyDBContext context;

        public UserRepository(MyDBContext context)
        {
            this.context = context;
        }

        public User GetUser(string email, string password)
        {
            return context.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
