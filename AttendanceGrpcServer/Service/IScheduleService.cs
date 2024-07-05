using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface IScheduleService
    {
        ScheduleDTO Get(int id);
        List<ScheduleDTO> List();

        ScheduleDTO Add(Schedule schedule);
        ScheduleDTO Delete(int id);

        ScheduleDTO Update(Schedule schedule);

        List<ScheduleDTO> ListStudentSchedule(int week, int year, int sid);
        List<ScheduleDTO> ListTeacherSchedule(int week, int year, int tid);



    }
}
