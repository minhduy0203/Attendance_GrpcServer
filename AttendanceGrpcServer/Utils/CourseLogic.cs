using AttendanceGrpcServer.Dto.Course;
using Microsoft.VisualBasic.FileIO;

namespace AttendanceGrpcServer.Utils
{
    public class CourseLogic
    {
        public static String GetCourseCSV(CourseDTORequest request, string message)
        {
            var header = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}", request.Code, request.Name, request.SubjectId, request.Students.ToString(), request.TeacherId, request.RoomId, request.StartDate, request.EndDate,request.TimeSlot,message);
            return header;
        }

        public static List<CourseDTORequest> GetValidCourses(Stream stream)
        {
            List<CourseDTORequest> result = new List<CourseDTORequest>();
            using (TextFieldParser csvParser = new TextFieldParser(stream))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { "," });
                csvParser.HasFieldsEnclosedInQuotes = true;

                csvParser.ReadLine();

                while (!csvParser.EndOfData)
                {
                    string[] fields = csvParser.ReadFields();
                    string Code = fields[0];
                    string Name = fields[1];
                    string Subject = fields[2];
                    string Student = fields[3];
                    string Teacher = fields[4];
                    string RoomId = fields[5];
                    string StartDate = fields[6];
                    string Endate = fields[7];
                    string TimeSlot = fields[8];

                    CourseDTORequest request = new CourseDTORequest()
                    {
                        Code = Code,
                        Name = Name,
                        SubjectId = Convert.ToInt32(Subject),
                        Students = Student?.Split(',')?.Select(Int32.Parse)?.ToList(),
                        TeacherId = Convert.ToInt32(Teacher),
                        RoomId = Convert.ToInt32(RoomId),
                        StartDate = String.IsNullOrEmpty(StartDate) ? DateTime.Now : Convert.ToDateTime(StartDate),
                        EndDate = String.IsNullOrEmpty(Endate) ? null : Convert.ToDateTime(Endate),
                        TimeSlot = TimeSlot
                };
                    result.Add(request);

                }
            }
            return result;
        }

       
    }
}
