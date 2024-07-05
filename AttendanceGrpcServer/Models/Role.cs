namespace AttendanceGrpcServer.Models
{
    public class Role
    {
        public int Id { get; set; }
        public String Name { get; set; }

        public ICollection<User>? Users { get; set; }



    }
}
