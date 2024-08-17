using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeladSanad.Dtos.Student;

namespace WeladSanad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGroupRepository _groupRepository;

        public StudentController(IStudentRepository studentRepository, IGroupRepository groupRepository)
        {
            _studentRepository = studentRepository;
            _groupRepository = groupRepository;
        }

        [HttpPost]
        [Route("Add")]
        [Authorize]
        public async Task<IActionResult> AddStudent([FromForm] AddStudentDto student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var std = new Student
            {
                Name = student.Name,
                GroupId = student.GroupId
            };

            await _studentRepository.AddStudent(std);
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("GetAll")]
        [Authorize]
        public async Task<IActionResult> ViewStudents()
        {
            List<ViewStudentsDto> stdsDto = new List<ViewStudentsDto>();
            var stds = await _studentRepository.GetStudents();

            foreach (var item in stds)
            {
                ViewStudentsDto dto = new ViewStudentsDto();
                dto.Id = item.Id;
                dto.Name = item.Name;
                var grp = await _groupRepository.GetGroupById(item.GroupId);
                dto.GroupName = grp.Name;
                stdsDto.Add(dto);
            }

            return Ok(stdsDto);
        }

        [HttpPost]
        [Route("Deactivate/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeactivateStudent(int id)
        {
            var std = await _studentRepository.GetStudentById(id);
            if (std == null)
            {
                return NotFound();
            }
            //await _studentRepository.DeleteStudent(id);
            std.IsDeleted = true;
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("Activate/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> ActivateStudent(int id)
        {
            var std = await _studentRepository.GetStudentById(id);
            if (std == null)
            {
                return NotFound();
            }
            //await _studentRepository.DeleteStudent(id);
            std.IsDeleted = false;
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpPut]
        [Route("Update/{id:int}")]
        [Authorize("Admin")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] UpdateStudentDto studentDto)
        {
            

            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            // Check if both Name and GroupId are null
            if (studentDto.Name == null && studentDto.GroupId == null)
            {
                return BadRequest("Either Name or GroupId must be provided.");
            }

            var std = await _studentRepository.GetStudentById(id);

            if (std == null)
            {
                return NotFound();
            }

            // Update only if values are provided
            if (studentDto.Name != null) std.Name = studentDto.Name;
            if (studentDto.GroupId != null) std.GroupId = studentDto.GroupId;

            await _studentRepository.UpdateStudent(id, std);
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("StudentsInGroup/{groupId:int}")]
        [Authorize]
        public async Task<IActionResult> StudentsInGroup(int groupId)
        {
            var stds = await _studentRepository.GetStudentsByGroupId(groupId);

            var stdinGroup = stds.Select(s => new ViewStudentsDto
            {
                Id = s.Id,
                Name = s.Name,
                GroupName = s.Group.Name
            });

            return Ok(stdinGroup);
        }

        [HttpGet]
        [Route("DeactivatedStudents")]
        [Authorize("Admin")]
        public async Task<IActionResult> DeactivatedStudents()
        {
            var delStudents = await _studentRepository.GetDeletedStudents();

            var stdsDto = delStudents.Select(s => new ViewStudentsDto
            {
                Id=s.Id,
                Name=s.Name,
                GroupName = s.Group.Name
            });

            return Ok(stdsDto);
        }

        [HttpGet]
        [Route("Search/{name:alpha}")]
        [Authorize]
        public async Task<IActionResult> Search(string name)
        {
            var stds = await _studentRepository.Search(name);

            return Ok(stds);
        }
    }
}
