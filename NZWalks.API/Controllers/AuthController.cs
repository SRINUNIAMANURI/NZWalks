
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Model.InPutDto;
using NZWalks.API.Repository;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private  readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Name,
                Email = registerRequestDto.Email
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if(identityResult.Succeeded)
            {
                if (registerRequestDto.Roals != null & registerRequestDto.Roals.Any()) 
                {
                    var roalsResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roals);

                    if (roalsResult.Succeeded)
                    {
                        return Ok("User Created Successfully! Login now");
                    } 
                }
            }

            return BadRequest(identityResult.Errors);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);

            if (user != null)
            {
                var check = await userManager.CheckPasswordAsync(user, loginDto.Password);

                if(check)
                { // need to return token
                    var roles = await userManager.GetRolesAsync(user);
                    if(roles != null)
                    {
                        var token = tokenRepository.CreateJWTToken(user, roles.ToList());

                        return Ok(token);
                    }
                }
            }

            return BadRequest("UserName and PassWord MissMatching");
        }
    }
}
