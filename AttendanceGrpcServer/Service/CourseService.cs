using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Student;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AttendanceGrpcServer.Utils;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Text;
using System.Transactions;

namespace AttendanceGrpcServer.Service
{
    public class CourseService : ICourseService
    {
        private ICourseRepository courseRepository;
        private IMapper mapper;
        private ISubjectRepository subjectRepository;
        private IScheduleRepository scheduleRepository;
        private IStudentScheduleRepository studentScheduleRepository;
        private IStudentCourseRepository studentCourseRepository;
        private MyDBContext context;

        public CourseService(ICourseRepository courseRepository, IMapper mapper, ISubjectRepository subjectRepository, IScheduleRepository scheduleRepository, IStudentScheduleRepository studentScheduleRepository, MyDBContext context, IStudentCourseRepository studentCourseRepository)
        {
            this.courseRepository = courseRepository;
            this.mapper = mapper;
            this.subjectRepository = subjectRepository;
            this.scheduleRepository = scheduleRepository;
            this.studentScheduleRepository = studentScheduleRepository;
            this.context = context;
            this.studentCourseRepository = studentCourseRepository;
        }

        public CourseDTO Add(Course course)
        {
            //add course
            Course add = courseRepository.Add(course);
            return mapper.Map<Course, CourseDTO>(add);
        }


        public Response<CourseDTO> AddCourse(CourseDTORequest request)
        {
            using (IDbContextTransaction transaction = context.Database.BeginTransaction())
            {


                //Courses that have same teacherid or roomid with request
                List<Course> courses = courseRepository
                    .List()
                    .Include(c => c.Schedules)
                    .Where(c => c.Schedules.Any(s => s.TeacherId == request.TeacherId || s.RoomId == request.RoomId))
                    .ToList();

                //Courses of all students in requests;
                List<StudentCourse> studentCourses = studentCourseRepository
                    .List()
                    .Where(sc => request.Students.Any(number => number == sc.StudentId))
                    .Include(sc => sc.Course)
                    .ToList();
                ;



                try
                {
                    //validate course
                    Validator.ValidateCourse(request, courses, studentCourses);

                    //create course
                    Course course = new Course()
                    {
                        SubjectId = request.SubjectId,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        TimeSlot = request.TimeSlot,
                        Code = request.Code,
                        Name = request.Name,
                    };

                    Course add = courseRepository.Add(course);
                    //throw new Exception();

                    //create studentcourse

                    foreach (int sid in request.Students)
                    {
                        StudentCourse sc = new StudentCourse { StudentId = sid, CourseId = add.Id };
                        studentCourseRepository.Add(sc);
                    }

                    //create schedule Timeslot A24
                    int daysUntilMonday = ((int)DayOfWeek.Monday - (int)course.StartDate.DayOfWeek + 7) % 7;
                    DateTime current = course.StartDate.AddDays(daysUntilMonday);

                    Subject subject = subjectRepository.Get(course.SubjectId);
                    int totalWeek = (int)Math.Ceiling(subject.NumberSlot / 2.0);
                    DateTime endDate = current.AddDays(totalWeek * 7);
                    course.EndDate = endDate;

                    List<Schedule> schedules = new List<Schedule>();
                    int remainSlot = subject.NumberSlot;


                    while (current.Subtract(endDate).TotalDays <= 0)
                    {
                        if (remainSlot == 0)
                        {
                            break;
                        }
                        Schedule s = new Schedule();
                        int dayofweek = (int)current.DayOfWeek + 1;
                        int timeslot = request.TimeSlot.IndexOf(dayofweek.ToString());
                        if (timeslot >= 1)
                        {
                            if (request.TimeSlot.StartsWith("A"))
                            {

                            }
                            else
                            {
                                timeslot = timeslot + 2;
                            }

                            s.Slot = timeslot;
                            s.Date = current;
                            s.CourseId = add.Id;
                            s.TeacherId = request.TeacherId;
                            s.RoomId = request.RoomId;
                            Schedule scheduleAdd = scheduleRepository.Add(s);
                            //create attendance

                            foreach (int sid in request.Students)
                            {
                                StudentSchedule studentSchedule = new StudentSchedule { ScheduleId = scheduleAdd.Id, StudentId = sid };
                                studentScheduleRepository.Add(studentSchedule);

                            }
                            remainSlot--;

                        }
                        current = current.AddDays(1);
                    }
                    transaction.Commit();
                    return new Response<CourseDTO> { Data = mapper.Map<Course, CourseDTO>(course), Message = "Add Sucessfully" };

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new Response<CourseDTO> { Data = null, Message = ex.Message };

                }
            }

        }

        public CourseDTO Delete(int id)
        {
            Course course = courseRepository.Delete(id);
            return mapper.Map<Course, CourseDTO>(course);

        }

        public CourseDTO Get(int id)
        {
            Course course = courseRepository.Get(id);
            return mapper.Map<Course, CourseDTO>(course);

        }

        public List<CourseDTO> List()
        {
            List<Course> courses = courseRepository.List().ToList();
            return mapper.Map<List<Course>, List<CourseDTO>>(courses);
        }

        public CourseDTO Update(Course course)
        {
            Course update = courseRepository.Update(course);
            return mapper.Map<Course, CourseDTO>(update);
        }

        public String AddByCSV(List<CourseDTORequest> coursesList)
        {
            // return message
            var csv = new StringBuilder();
            var header = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", "Code", "Name", "Subject", "Students", "Teacher", "RoomId", "StartDate", "EndDate", "TimeSlot", "Message");
            csv.AppendLine(header);
            foreach (CourseDTORequest request in coursesList)
            {
                //Courses that have same teacherid or roomid with request
                List<Course> courses = courseRepository
                    .List()
                    .Include(c => c.Schedules)
                    .Where(c => c.Schedules.Any(s => s.TeacherId == request.TeacherId || s.RoomId == request.RoomId))
                    .ToList();

                //Courses of all students in requests;
                List<StudentCourse> studentCourses = studentCourseRepository
                    .List()
                    .Where(sc => request.Students.Any(number => number == sc.StudentId))
                    .Include(sc => sc.Course)
                    .ToList();
                ;



                try
                {
                    //validate course
                    Validator.ValidateCourse(request, courses, studentCourses);

                    //create course
                    Course course = new Course()
                    {
                        SubjectId = request.SubjectId,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        TimeSlot = request.TimeSlot,
                        Code = request.Code,
                        Name = request.Name,
                    };

                    Course add = courseRepository.Add(course);


                    //create studentcourse

                    foreach (int sid in request.Students)
                    {
                        StudentCourse sc = new StudentCourse { StudentId = sid, CourseId = add.Id };
                        studentCourseRepository.Add(sc);
                    }

                    //create schedule Timeslot A24
                    int daysUntilMonday = ((int)DayOfWeek.Monday - (int)course.StartDate.DayOfWeek + 7) % 7;
                    DateTime current = course.StartDate.AddDays(daysUntilMonday);

                    Subject subject = subjectRepository.Get(course.SubjectId);
                    int totalWeek = (int)Math.Ceiling(subject.NumberSlot / 2.0);
                    DateTime endDate = current.AddDays(totalWeek * 7);
                    course.EndDate = endDate;

                    List<Schedule> schedules = new List<Schedule>();
                    int remainSlot = subject.NumberSlot;


                    while (current.Subtract(endDate).TotalDays <= 0)
                    {
                        if (remainSlot == 0)
                        {
                            break;
                        }
                        Schedule s = new Schedule();
                        int dayofweek = (int)current.DayOfWeek + 1;
                        int timeslot = request.TimeSlot.IndexOf(dayofweek.ToString());
                        if (timeslot >= 1)
                        {
                            if (request.TimeSlot.StartsWith("A"))
                            {

                            }
                            else
                            {
                                timeslot = timeslot + 2;
                            }

                            s.Slot = timeslot;
                            s.Date = current;
                            s.CourseId = add.Id;
                            s.TeacherId = request.TeacherId;
                            s.RoomId = request.RoomId;
                            Schedule scheduleAdd = scheduleRepository.Add(s);
                            //create attendance

                            foreach (int sid in request.Students)
                            {
                                StudentSchedule studentSchedule = new StudentSchedule { ScheduleId = scheduleAdd.Id, StudentId = sid };
                                studentScheduleRepository.Add(studentSchedule);

                            }
                            remainSlot--;

                        }
                        current = current.AddDays(1);
                    }

                    csv.AppendLine(CourseLogic.GetCourseCSV(request, "Add successfully"));

                }
                catch (Exception ex)
                {
                    csv.AppendLine(CourseLogic.GetCourseCSV(request, ex.Message));
                    continue;

                }
            }


            return csv.ToString();
        }

       
    }
}
