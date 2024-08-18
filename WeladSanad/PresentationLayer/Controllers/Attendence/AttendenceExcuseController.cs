using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeladSanad.BusinessLogicLayer.Dtos.Attendence;
using WeladSanad.DataAccessLayer.Models.Attendence;
using WeladSanad.DataAccessLayer.Repositories.IRepository.IAttendenceRepos;

namespace WeladSanad.PresentationLayer.Controllers.Attendence
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendenceExcuseController : ControllerBase
    {
        private readonly IAttendExcuseRepository _attTypeRepo;

        public AttendenceExcuseController(IAttendExcuseRepository attendenceTypeService)
        {
            _attTypeRepo = attendenceTypeService;
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAttendenceTypes()
        {
            var attendenceTypes = await _attTypeRepo.GetAttendTypes();
            return Ok(attendenceTypes);
        }

        [HttpGet]
        [Route("GetType/{id}")]
        [Authorize]
        public async Task<IActionResult> GetAttendenceType(int id)
        {
            var attendenceType = await _attTypeRepo.GetAttendType(id);
            return Ok(attendenceType);
        }

        [HttpPost]
        [Route("Add")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddAttendenceType(AttendTypeDto attendenceType)
        {
            if (attendenceType.Type == null) return BadRequest();

            var att = new Attend
            {
                Type = attendenceType.Type,
            };

            await _attTypeRepo.AddAttendType(att);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateAttendenceType(int id, AttendTypeDto attendenceType)
        {
            if (attendenceType.Type == null)
                return BadRequest();

            var att = new Attend
            {
                Type = attendenceType.Type,
            };

            await _attTypeRepo.UpdateAttendType(id, att);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteAttendenceType(int id)
        {
            await _attTypeRepo.DeleteAttendType(id);
            var ok = await _attTypeRepo.SaveChanges();

            if (ok == true) return Ok();
            else return BadRequest();
        }

    }
}
