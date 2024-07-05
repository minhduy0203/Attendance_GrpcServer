using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;

namespace AttendanceGrpcServer.Service
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Login(string username, string password)
        {
            return userRepository.GetUser(username, password);
        }
    }
}
