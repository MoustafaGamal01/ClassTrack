using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeladSanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceRepository _attendenceRepository;

        public AttendenceController(IAttendenceRepository attendenceRepository)
        {
            _attendenceRepository = attendenceRepository;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAttendence(StudentAttend attendence)
        {
            await _attendenceRepository.AddAttendence(attendence);
            var ok = await _attendenceRepository.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }
    }
}
