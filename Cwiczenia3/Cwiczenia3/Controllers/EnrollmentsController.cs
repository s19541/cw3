using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Cwiczenia3.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        public IConfiguration Configuration { get; set; }
        private readonly Services.IStudentsDbService _dbService;
        public EnrollmentsController(Services.IStudentsDbService dbService, IConfiguration configuration)
       {
            _dbService = dbService;
            Configuration = configuration;
        }
        [HttpPost]
        public IActionResult createEnrollment(Requests.EnrollmentRequest request)
        {

            return _dbService.createEnrollment(request);
        }
   
        [HttpPost("promotions")]
        public IActionResult createPromotion(Requests.PromotionRequestcs request)
        {
          
            return _dbService.createPromotion(request); 
        }
        [HttpPost]
        public IActionResult login(DTOs.LoginRequestDto request)
        {
            var claims = new[]
{
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Name, "jan123"),
                new Claim(ClaimTypes.Role, "admin"),
                new Claim(ClaimTypes.Role, "student")
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );
            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken=Guid.NewGuid()
            });
        }
    }
}
