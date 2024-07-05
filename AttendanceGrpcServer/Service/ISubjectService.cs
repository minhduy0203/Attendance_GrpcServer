using AttendanceGrpcServer.Dto.Subject;
using AttendanceGrpcServer.Dto.Teacher;
using AttendanceGrpcServer.Models;

namespace AttendanceGrpcServer.Service
{
    public interface ISubjectService
    {
        SubjectDTO Get(int id);
        List<SubjectDTO> List();

        SubjectDTO Add(SubjectDTO subject);
        SubjectDTO Delete(int id);

        SubjectDTO Update(SubjectDTO subject);
    }
}
