using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeladSanad.Dtos;
using WeladSanad.Models;

namespace WeladSanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AttendenceController : ControllerBase
    {
        private readonly IAttendenceRepository _attendenceRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStudentRepository _studentRepository;

        public AttendenceController(IAttendenceRepository attendenceRepository,
            UserManager<ApplicationUser> userManager, IStudentRepository studentRepository)
        {
            _attendenceRepository = attendenceRepository;
            _userManager = userManager;
            _studentRepository = studentRepository;
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddAttendence(StudentAttendDto attendence)
        {
            var user = await _userManager.GetUserAsync(User);

            var std = await _studentRepository.GetStudentById(attendence.StudentId);
            
            if (user.Id != std.Group.TeacherId)
                return Unauthorized();

            var stdAtt = new StudentAttend
            {
                AttendId = attendence.AttendId,
                StudentId = attendence.StudentId,
                Description = attendence.Description,
                Date = attendence.Date
            };

            await _attendenceRepository.AddAttendence(stdAtt);
            var ok = await _attendenceRepository.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        public async Task<IActionResult> UpdateAttendence(int id, StudentAttend attendence)
        {
            var user = await _userManager.GetUserAsync(User);

            var std = await _studentRepository.GetStudentById(attendence.StudentId);

            if (user.Id != std.Group.TeacherId)
                return Unauthorized();

            await _attendenceRepository.UpdateAttendence(id, attendence);
            var ok = await _attendenceRepository.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpGet]
        [Route("Search/{searchMonthYear:alpha}")]
        public async Task<IActionResult> Search(string searchMonthYear)
        {
            var stdAtt = await _attendenceRepository.Search(searchMonthYear);
            if (stdAtt != null) return Ok(stdAtt);
            else return NotFound();
        }
    }
}
