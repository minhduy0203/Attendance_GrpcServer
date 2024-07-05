using AttendanceGrpcServer.Dto;
using AttendanceGrpcServer.Service;
using Grpc.Core;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AttendanceGrpcServer.Services
{
    public class UserService : UserLogin.UserLoginBase
    {
        private IUserService userService;
        private IConfiguration configuration;

        public UserService(IUserService userService, IConfiguration configuration)
        {
            this.userService = userService;
            this.configuration = configuration;
        }

        public override Task<Response> Login(LoginRequest request, ServerCallContext context)
        {
            Models.User u = userService.Login(request.Email, request.Password);
            if (u != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, u.Email),
                    new Claim(ClaimTypes.Role,u.Role.Name)
                };

                if (u.Role.Name == "STUDENT")
                {
                    claims.Add(new Claim("Id", u.StudentId.ToString()));
                }
                else if (u.Role.Name == "TEACHER")
                {
                    claims.Add(new Claim("Id", u.TeacherId.ToString()));

                }

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var expiry = DateTime.Now.AddDays(Convert.ToInt32(configuration["Jwt:ExpiryInDays"]));
                var token = new JwtSecurityToken(
                    configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: expiry,
                    signingCredentials: creds
                    );

              return Task.FromResult(new Response
              {
                  Result = new LoginResponse { Token = new JwtSecurityTokenHandler().WriteToken(token) },
                  Message = "Login successfully"
              });

            }
            else
            {
                return Task.FromResult(new Response
                {
                    Result = null,
                    Message = "Login failed"
                });
            }
        }
    }
}
