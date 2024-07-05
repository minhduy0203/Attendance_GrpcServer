using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface IUserRepository
    {
        User GetUser(string email, string password);
    }
}
