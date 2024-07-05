using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private MyDBContext dBContext;

        public RoomRepository(MyDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Room Add(Room room)
        {
            dBContext.Rooms.Add(room);
            dBContext.SaveChanges();
            return room;
        }

        public Room Delete(int id)
        {
            Room room = dBContext.Rooms.FirstOrDefault(r => r.Id == id);
            if (room != null)
            {
                dBContext.Remove(room);
                dBContext.SaveChanges();
            }
            return room;

        }

        public Room Get(int id)
        {
            return dBContext.Rooms.FirstOrDefault(r => r.Id == id);

        }

        public IQueryable<Room> List()
        {
            return dBContext.Rooms.AsQueryable();
        }

        public Room Update(Room room)
        {
            dBContext.Entry<Room>(room).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            dBContext.SaveChanges();
            return room;
        }
    }
}
