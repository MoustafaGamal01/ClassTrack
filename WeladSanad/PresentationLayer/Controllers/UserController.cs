using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeladSanad.BusinessLogicLayer.Dtos.User;
using WeladSanad.DataAccessLayer.Models;

namespace WeladSanad.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            var userDto = users.Select(user => new UserDetailsDto
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            });

            return Ok(userDto);
        }

        [HttpPut]
        [Route("Update")]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDto updateUserDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }


            if (updateUserDto.Name != null)  user.Name = updateUserDto.Name;
            if (updateUserDto.UserName != null)  user.UserName = updateUserDto.UserName;
            if (updateUserDto.Role != null) 
            {
                // change the role of the user
                var roles = await _userManager.GetRolesAsync(user);
                await _userManager.RemoveFromRolesAsync(user, roles);
                await _userManager.AddToRoleAsync(user, updateUserDto.Role);
            }
            await _userManager.UpdateAsync(user);
            return Ok("User Updated");
        }

        [HttpPut]
        [Route("ChangePassword")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordDto userPasswordDto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
                    
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if(user.PasswordHash == null 
                    || !_userManager.PasswordHasher.VerifyHashedPassword(user, user.PasswordHash, userPasswordDto.CurrentPassword)
                    .Equals(PasswordVerificationResult.Success))
                {
                    return BadRequest("Old Password is incorrect");
                }

                user.PasswordHash = _userManager
                                    .PasswordHasher.
                                    HashPassword(user, userPasswordDto.NewPassword);
                
                await _userManager.UpdateAsync(user);
                return Ok("Password Changed");
            }
            return BadRequest("Make sure passwords are valid");
        }

        [HttpDelete]
        [Route("Delete/{Username}")]
        [Authorize("Admin")]
        public async Task<IActionResult> Delete(string Username)
        {
            var user = await _userManager.FindByNameAsync(Username);

            if (user == null)
            {
                return NotFound();
            }

            var roles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, roles);

            await _userManager.DeleteAsync(user);
            return Ok("User Deleted");
        }

        [HttpPut]
        [Route("Admin/ChangePassword")]
        [Authorize("Admin")]
        public async Task<IActionResult> AdminChangePassword(AdminChangeUserPassword userPasswordDto)
        {
            var user = await _userManager.FindByNameAsync(userPasswordDto.Username);

            if (user == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                user.PasswordHash = _userManager
                                    .PasswordHasher.
                                    HashPassword(user, userPasswordDto.NewPassword);
                    await _userManager.UpdateAsync(user);
                    return Ok("Password Changed");
            }
            return BadRequest("Passwords do not match");
        }

    }
}
