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
        [Route("add")]
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
        [Route("viewstudents")]
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
        [Route("delete/{id:int}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var std = await _studentRepository.GetStudentById(id);
            if (std == null)
            {
                return NotFound();
            }
            await _studentRepository.DeleteStudent(id);
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpPost]
        [Route("update/{stdId:int}")]
        public async Task<IActionResult> UpdateStudent(int stdId, [FromBody] UpdateStudentDto studentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if both Name and GroupId are null
            if (studentDto.Name == null && studentDto.GroupId == null)
            {
                return BadRequest("Either Name or GroupId must be provided.");
            }

            var std = await _studentRepository.GetStudentById(stdId);

            if (std == null)
            {
                return NotFound();
            }

            // Update only if values are provided
            if (studentDto.Name != null) std.Name = studentDto.Name;
            if (studentDto.GroupId != null) std.GroupId = studentDto.GroupId;

            await _studentRepository.UpdateStudent(stdId, std);
            await _studentRepository.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("studentsingroup/{groupId:int}")]
        public async Task<IActionResult> StudentsInGroup(int groupId)
        {
            var stds = await _studentRepository.GetStudentsByGroupId(groupId);
            return Ok(stds);
        }
    }
}
