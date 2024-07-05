using AttendanceGrpcServer.Dto.Course;
using AttendanceGrpcServer.Dto.Schedule;
using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;

namespace AttendanceGrpcServer.Utils
{
    public class Validator
    {

       

        public Validator()
        {
           
        }

        public static void ValidateCourse(CourseDTORequest request, List<Course> courses, List<StudentCourse> studentCourses)
        {

            if(request.StartDate <= DateTime.Now)
            {
                throw new Exception("Course can't be in past");
            }

            foreach (Course course in courses)
            {
                if (ValidateTimeSlot(course.TimeSlot, request.TimeSlot))
                {
                    //foreach (Schedule schedule in course.Schedules)
                    //{

                    //    //Teacher and timeslot

                    //    if (ValidateTeacher(request.TeacherId, schedule.TeacherId))
                    //        throw new Exception("Time slot and Teacher id are duplicated");


                    //    //Room and time slot
                    //    if (ValidateRoom(request.RoomId, schedule.RoomId))
                    //        throw new Exception("Time slot and Room id are duplicated");

                    //}
                    throw new Exception("Timeslot and teacher or room is duplicated");

                }
            }


            //check student and timeslot 
            foreach(StudentCourse studentCourse in studentCourses)
            {
                if (ValidateTimeSlot(studentCourse.Course.TimeSlot, request.TimeSlot))
                    throw new Exception("Timeslot and Student is duplicated");
            }


        }


        public static void ValidateSchedule(ScheduleDTO request , List<Schedule> schedules) 
        {
            
        }

      

        public static bool ValidateGroup(string groupA, string groupB)
        {
            groupA = groupA.Trim();
            groupB = groupB.Trim();
            return groupA.Equals(groupB);
        }

        public static bool ValidateTeacher(int teacherA, int teacherB)
        {
            return teacherA == teacherB;
        }

        public static bool ValidateSubject(int subA, int subB)
        {
            return subA == subB;
        }

        public static bool ValidateRoom(int roomA, int roomB)
        {
            return roomA == roomB;
        }

        //timeA from input , time B from db
        public static bool ValidateTimeSlot(string timeA, string timeB)
        {
            bool result = false;

            timeA = timeA.Trim();
            timeB = timeB.Trim();

            if (timeA.Length != 3 || timeB.Length != 3)
            {
                throw new Exception("Timeslot must contain 3 character");
            }

            if (timeA[0] != 'A' && timeA[0] != 'P')
            {
                throw new Exception("Time slot should start with A or P");
            }

            if (timeA.Length == 3)
            {
                for (int i = 1; i <= 2; i++)
                {
                    if (!timeA[i].ToString().All(char.IsDigit))
                    {
                        throw new Exception("Time slot must contain digit");
                    }
                    else
                    {
                        int number = Int32.Parse(timeA[i].ToString());
                        if (number < 2 || number > 8)
                        {
                            throw new Exception("Digit must range from 2 to 8");
                        }
                    }
                }
            }

            if (timeA[0] == timeB[0])
            {
                if (timeA[1] == timeB[1] || timeA[2] == timeB[2])
                {
                    result = true;
                }

            }
            return result;
        }

    }
}
