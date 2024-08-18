using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeladSanad.BusinessLogicLayer.Dtos.Auth;
using WeladSanad.DataAccessLayer.Models;

namespace WeladSanad.PresentationLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtOptions jwtOptions;
        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, JwtOptions jwtOptions)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            this.jwtOptions = jwtOptions;
        }


        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.UserName);
            if (user != null)
            {
                bool isPasswordValid = await _userManager.CheckPasswordAsync(user, userLoginDto.Password);

                if (isPasswordValid)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Issuer = jwtOptions.Issuer,
                        Audience = jwtOptions.Audience,
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)), SecurityAlgorithms.HmacSha256)
                        ,
                        Expires = DateTime.UtcNow.AddMinutes(jwtOptions.LifeTime),
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.NameIdentifier, user.Id)
                        }),
                    };

                    var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                    var accessToken = tokenHandler.WriteToken(securityToken);

                    return Ok(accessToken);
                }
                return BadRequest("Invalid Username or Password");
            }
            return BadRequest("Invalid Username or Password");
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var isFound = await _userManager.FindByNameAsync(userRegisterDto.UserName);
                if (isFound != null)
                {
                    return BadRequest("Username Exists");
                }
                ApplicationUser user = new ApplicationUser();
                user.UserName = userRegisterDto.UserName;
                user.PasswordHash = userRegisterDto.Password;
                user.Name = userRegisterDto.Name;
                var isCreated = await _userManager.CreateAsync(user, userRegisterDto.Password);
                if (isCreated.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, userRegisterDto.Role);
                    await _signInManager.SignInAsync(user, true);
                    return Ok("User Created");
                }
            }
            return BadRequest("Enter Valid Data");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("Logged out");
        }
    }
}
