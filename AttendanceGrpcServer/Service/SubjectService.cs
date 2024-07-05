using AttendanceGrpcServer.Dto.Subject;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AutoMapper;

namespace AttendanceGrpcServer.Service
{
    public class SubjectService : ISubjectService
    {
        private ISubjectRepository subjectRepository;
        private IMapper mapper;

        public SubjectService(ISubjectRepository subjectRepository, IMapper mapper)
        {
            this.subjectRepository = subjectRepository;
            this.mapper = mapper;
        }

        public SubjectDTO Add(SubjectDTO subject)
        {
            Subject add = subjectRepository.Add(mapper.Map<SubjectDTO, Subject>(subject));
            return mapper.Map<Subject, SubjectDTO>(add);
        }

        public SubjectDTO Delete(int id)
        {
            Subject delete = subjectRepository.Delete(id);
            return mapper.Map<Subject, SubjectDTO>(delete);
        }

        public SubjectDTO Get(int id)
        {
            Subject subject = subjectRepository.Get(id);
            return mapper.Map<Subject, SubjectDTO>(subject);
        }

        public List<SubjectDTO> List()
        {
            List<Subject> subjects = subjectRepository.List().ToList();
            return mapper.Map<List<Subject>, List<SubjectDTO>>(subjects);
        }

        public SubjectDTO Update(SubjectDTO subject)
        {
            Subject find = subjectRepository.Get(subject.Id);
            Subject update = null;
            if (find != null)
            {
                mapper.Map<SubjectDTO, Subject>(subject, find);
                update = subjectRepository.Update(find);
            }

            return mapper.Map<Subject, SubjectDTO>(update);
        }
    }
}
