namespace AttendanceGrpcServer.Dto.Course
{
    public class CourseDTORequest
    {
        public int Id { get; set; }

        public string? Code { get; set; }
        public string? Name { get; set; }
        public int SubjectId { get; set; }

        public List<int> Students { get; set; }
        public int TeacherId { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string TimeSlot { get; set; }


    }
}
