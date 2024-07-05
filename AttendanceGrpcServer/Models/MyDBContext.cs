using Microsoft.EntityFrameworkCore;

namespace AttendanceGrpcServer.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext()
        { }
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<Student> Students { get; set; }
        public DbSet<StudentSchedule> StudentSchedules { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<Room> Rooms { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var conStr = "server=LAPTOP-OP9HDG45\\MSSQL19;database=AttendanceApp;uid=sa;pwd=123;TrustServerCertificate=True";
                optionsBuilder.UseSqlServer(conStr);
            }

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dinh nghia PK cho bang ProductSupplier
            modelBuilder.Entity<StudentCourse>()
                 .HasKey(sc => new { sc.StudentId, sc.CourseId });

            modelBuilder.Entity<StudentSchedule>()
                .HasKey(ss => new { ss.StudentId, ss.ScheduleId });


            Seeding(modelBuilder);
        }

        private void Seeding(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .HasData(
                new Student { Id = 1, Code = "HE172040", Name = "Minh Duy" }
                );
            modelBuilder.Entity<Teacher>()
                .HasData(
                new Teacher { Id = 1, Code = "ChiLP", Name = "Le Phuong Chi" }
                );

            modelBuilder.Entity<Subject>()
                 .HasData(
                new Subject { Id = 1, Name = "C# programming", NumberSlot = 20 }
                );

            modelBuilder.Entity<Room>()
                .HasData(
                new Room { Id = 1, Name = "DE-305" },
                new Room { Id = 2, Name = "BE-405" }
                );

            modelBuilder.Entity<Role>()
                .HasData(
                new Role { Id = 1, Name = "ADMIN" },
                new Role { Id = 2, Name = "TEACHER" },
                new Role { Id = 3, Name = "STUDENT" }
                );
        }
    }
}
