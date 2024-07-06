using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;
using Grpc.Core;

namespace AttendanceGrpcServer.Services
{
    public class AttendanceService : Attendancer.AttendancerBase
    {
        private IStudentScheduleRepository studentScheduleRepository;
        private IMapper mapper;

        public AttendanceService(IStudentScheduleRepository studentScheduleRepository, IMapper mapper)
        {
            this.studentScheduleRepository = studentScheduleRepository;
            this.mapper = mapper;
        }

        public override Task<AttendanceListResponse> AttendStudents(AttendanceListRequest request, ServerCallContext context)
        {
            List<int> sids = new List<int>();
            List<AttendanceGrpcServer.Models.Status> statuses = new List<AttendanceGrpcServer.Models.Status>();
            foreach (var item in request.Sid)
            {
                sids.Add(item);
            }

            foreach (var item in request.Statuses)
            {
                statuses.Add((AttendanceGrpcServer.Models.Status) item);
            }
            List<StudentSchedule> result = studentScheduleRepository.UpdateListStudentAttendance(sids, request.ScheduleId, statuses);
            List<StudentSchedulesDTO> response = mapper.Map<List<StudentSchedule>, List<StudentSchedulesDTO>>(result);

            return Task.FromResult(new AttendanceListResponse
            {
                Message = "Add attendance successfullly",
            });

        }
    }
}
