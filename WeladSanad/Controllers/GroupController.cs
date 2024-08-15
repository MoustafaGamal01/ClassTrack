using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WeladSanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;

        public GroupController(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("addgroup")]
        public async Task<IActionResult> AddGroup([FromBody] AddGroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var grp = new Group
            {
                Name = group.Name
            };

            await _groupRepository.AddGroup(grp);
            await _groupRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("viewgroups")]
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

        [HttpPost]
        [Route("deletegroup/{id:int}")]
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

        [HttpPost]
        [Route("updategroup/{grpId:int}")]
        public async Task<IActionResult> UpdateGroup(int grpId, [FromBody] UpdateGroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var grp = await _groupRepository.GetGroupById(grpId);
            if (grp == null)
            {
                return NotFound();
            }

            if(grp.Name != null) grp.Name = group.Name;
            await _groupRepository.UpdateGroup(grpId,grp);
            await _groupRepository.SaveChanges();
            return Ok();
        }

    }
}
