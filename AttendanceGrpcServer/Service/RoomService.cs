using AttendanceGrpcServer.Dto.Room;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;

namespace AttendanceGrpcServer.Service
{
    public class RoomService : IRoomService
    {

        private IRoomRepository roomRepository;
        private IMapper mapper;

        public RoomService(IRoomRepository roomRepository, IMapper mapper)
        {
            this.roomRepository = roomRepository;
            this.mapper = mapper;
        }

        public RoomDTO Add(RoomDTO room)
        {
            Room add = roomRepository.Add(mapper.Map<RoomDTO, Room>(room));
            return mapper.Map<Room, RoomDTO>(add);
        }

        public RoomDTO Delete(int id)
        {
            Room delete = roomRepository.Delete(id);
            return mapper.Map<Room, RoomDTO>(delete);


        }

        public RoomDTO Get(int id)
        {
            Room room = roomRepository.Get(id);
            return mapper.Map<Room, RoomDTO>(room);
        }

        public List<RoomDTO> List()
        {
            List<Room> rooms = roomRepository
                 .List()
                 .ToList();
            return mapper.Map<List<Room>, List<RoomDTO>>(rooms);

        }

        public RoomDTO Update(RoomDTO room)
        {
            Room find = roomRepository.Get(room.Id);
            Room update = null;

            if (find != null)
            {
                mapper.Map<RoomDTO, Room>(room, find);
                roomRepository.Update(find);
            }

            return mapper.Map<Room, RoomDTO>(update);
        }
    }
}
