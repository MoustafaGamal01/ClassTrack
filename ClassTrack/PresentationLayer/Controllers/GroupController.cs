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
        private readonly IStudentRepository _studentRepository;

        public GroupController(IGroupRepository groupRepository, IStudentRepository studentRepository)
        {
            _groupRepository = groupRepository;
            _studentRepository = studentRepository;
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
            foreach (var grp in grps)
            {
                var stds = await  _studentRepository.GetStudentsByGroupId(grp.Id);
                ViewGroupsDto grpDto = new ViewGroupsDto();
                grpDto.Name = grp.Name;
                grpDto.Id = grp.Id;
                if (grp.TeacherId == null)
                {
                    grpDto.TeacherName = "No teacher assigned";
                }
                else
                {
                    grpDto.TeacherName = grp.Teacher.Name;
                }
                grpDto.StudentsCount = stds.Count;

                grpsDto.Add(grpDto);
            }
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
        public async Task<IActionResult> UpdateGroup(int id, [FromBody] UpdateGroupDto groupDto)
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

            if (groupDto.Name != null) grp.Name = groupDto.Name;
            if (groupDto.TeacherId != null) grp.TeacherId = groupDto.TeacherId;
            else grp.TeacherId = grp.TeacherId;
            await _groupRepository.UpdateGroup(id, grp);
            await _groupRepository.SaveChanges();
            return Ok();
        }

    }
}
