using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface IUserService
    {
        User Login(string username, string password);
    }
}
