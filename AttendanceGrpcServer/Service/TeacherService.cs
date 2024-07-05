using AttendanceGrpcServer.Dto.Teacher;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;

namespace AttendanceGrpcServer.Service
{
    public class TeacherService : ITeacherService
    {
        private ITeacherRepository teacherRepository;
        private IMapper mapper;

        public TeacherService(ITeacherRepository teacherRepository, IMapper mapper)
        {
            this.teacherRepository = teacherRepository;
            this.mapper = mapper;
        }

        public TeacherDTO Add(TeacherDTO teacher)
        {
            Teacher add = teacherRepository.Add(mapper.Map<TeacherDTO, Teacher>(teacher));
            return mapper.Map<Teacher, TeacherDTO>(add);
        }

        public TeacherDTO Delete(int id)
        {
            Teacher delete = teacherRepository.Delete(id);
            return mapper.Map<Teacher, TeacherDTO>(delete);
        }

        public TeacherDTO Get(int id)
        {
            Teacher teacher = teacherRepository.Get(id);
            return mapper.Map<Teacher, TeacherDTO>(teacher);
        }

        public List<TeacherDTO> List()
        {
            List<Teacher> teachers = teacherRepository.List().ToList();
            return mapper.Map<List<Teacher>, List<TeacherDTO>>(teachers);
        }

        public TeacherDTO Update(TeacherDTO teacher)
        {
            Teacher update = null;
            Teacher find = teacherRepository.Get(teacher.Id);
            if (find != null)
            {
                mapper.Map<TeacherDTO, Teacher>(teacher, find);
                update = teacherRepository.Update(find);

            }
            return mapper.Map<Teacher, TeacherDTO>(update);
        }
    }
}
