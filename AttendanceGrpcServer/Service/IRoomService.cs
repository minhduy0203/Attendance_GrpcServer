using AttendanceGrpcServer.Dto.Room;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;

namespace AttendanceGrpcServer.Service
{
    public interface IRoomService
    {

        RoomDTO Get(int id);
        List<RoomDTO> List();

        RoomDTO Add(RoomDTO room);
        RoomDTO Delete(int id);

        RoomDTO Update(RoomDTO room);


    }
}
