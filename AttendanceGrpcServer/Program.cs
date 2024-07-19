using AttendanceGrpcServer.Models;
using AttendanceGrpcServer.Repository;
using AttendanceGrpcServer.Service;
using AttendanceGrpcServer.Services;
using AttendanceMananagmentProject.Mappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MyDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("Mystr")));

builder.Services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = false,
           ValidateAudience = false,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["Jwt:Issuer"],
           ValidAudience = builder.Configuration["Jwt:Audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
       };
   });
builder.Services.AddAuthorization(); 
builder.Services.AddGrpc();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddTransient<IRoomRepository, RoomRepository>();

builder.Services.AddTransient<ICourseRepository, CourseRepository>();

builder.Services.AddTransient<IScheduleRepository, ScheduleRepository>();

builder.Services.AddTransient<ITeacherRepository, TeacherRepository>();

builder.Services.AddTransient<IStudentRepository, StudentRepository>();

builder.Services.AddTransient<ISubjectRepository, SubjectRepository>();

builder.Services.AddTransient<IStudentCourseRepository, StudentCourseRepository>();

builder.Services.AddTransient<IStudentScheduleRepository, StudentScheduleRepository>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, AttendanceGrpcServer.Service.UserService>();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapGrpcService<GreeterService>();
app.MapGrpcService<AttendanceGrpcServer.Services.UserService>();
app.MapGrpcService<SchedulerService>();
app.MapGrpcService<AttendanceService>();
app.MapGrpcService<AttendanceGrpcServer.Services.CourseService>();
app.MapGrpcService<AttendanceGrpcServer.Services.StudentCourseService>();

app.Run();
