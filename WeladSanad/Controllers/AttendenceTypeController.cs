using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeladSanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceTypeController : ControllerBase
    {
        private readonly IAttendTypeRepository _attTypeRepo;

        public AttendenceTypeController(IAttendTypeRepository attendenceTypeService)
        {
            _attTypeRepo = attendenceTypeService;
        }

        [HttpGet]
        [Route("getall")]
        public async Task<IActionResult> GetAttendenceTypes()
        {
            var attendenceTypes = await _attTypeRepo.GetAttendTypes();
            return Ok(attendenceTypes);
        }

        [HttpGet]
        [Route("gettype/{id}")]
        public async Task<IActionResult> GetAttendenceType(int id)
        {
            var attendenceType = await _attTypeRepo.GetAttendType(id);
            return Ok(attendenceType);
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddAttendenceType(Attend attendenceType)
        {
            await _attTypeRepo.AddAttendType(attendenceType);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> UpdateAttendenceType(Attend attendenceType)
        {
            await _attTypeRepo.UpdateAttendType(attendenceType.Id, attendenceType);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAttendenceType(int id)
        {
            await _attTypeRepo.DeleteAttendType(id);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

    }
}
