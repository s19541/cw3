using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia3.Controllers
{
    [ApiController]
    [Route("api/enrollments")]
    public class EnrollmentsController : ControllerBase
    {
        private readonly Services.IStudentsDbService _dbService;
        public EnrollmentsController(Services.IStudentsDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpPost]
        public IActionResult createEnrollment(Requests.EnrollmentRequest request)
        {
            Models.Enrollment e=_dbService.createEnrollment(request);
            if (e != null)
                return Created("enrollment",e);
            return BadRequest();
        }
   
        [HttpPost("promotions")]
        public IActionResult createPromotion(Requests.PromotionRequestcs request)
        {
            Models.Enrollment e = _dbService.createPromotion(request);
            if (e != null)
                return Created("enrollment", e);
            return NotFound();
        }
    }
}
