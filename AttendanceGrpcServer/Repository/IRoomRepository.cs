using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public interface IRoomRepository
    {
        public IQueryable<Room> List();
        public Room Get(int id);

        public Room Add(Room room);
        public Room Delete(int id);
        public Room Update(Room room);
    }
}
