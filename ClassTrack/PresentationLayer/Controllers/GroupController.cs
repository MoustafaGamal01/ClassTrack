using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClassTrack.BusinessLogicLayer.Dtos.Group;
using ClassTrack.DataAccessLayer.Models;
using ClassTrack.DataAccessLayer.Repositories.IRepository;

namespace ClassTrack.PresentationLayer.Controllers
{
    [Route("classtrack/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("add")]
        [Authorize("Admin")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var grp = new Group
            {
                Name = group.Name,
                TeacherId = group.TeacherId
            };
                
            await _groupRepository.AddGroup(grp);
            await _groupRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("getall")]
        [Authorize]
        public async Task<IActionResult> ViewGroups()
        {
            List<ViewGroupsDto> grpsDto = new List<ViewGroupsDto>();
            var grps = await _groupRepository.GetAllGroups();
            grpsDto = grps.Select(g => new ViewGroupsDto
            {
                Id = g.Id,
                Name = g.Name
            }).ToList();

            return Ok(grpsDto);
        }

        [HttpDelete]
        [Route("delete/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var grp = await _groupRepository.GetGroupById(id);
            if (grp == null)
            {
                return NotFound();
            }

            await _groupRepository.DeleteGroup(id);
            await _groupRepository.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("update/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grp = await _groupRepository.GetGroupById(id);
            if (grp == null)
            {
                return NotFound();
            }

            if (grp.Name != null) grp.Name = group.Name;
            if (grp.TeacherId != null) grp.TeacherId = group.TeacherId;
            await _groupRepository.UpdateGroup(id, grp);
            await _groupRepository.SaveChanges();
            return Ok();
        }

    }
}
