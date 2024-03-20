using authentication_autharization.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace authentication_autharization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;
        private readonly AppDbContext appDbContext;
        public AuthenticationController(AppDbContext appDbContext, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
            this.appDbContext = appDbContext;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExist = await userManager.FindByNameAsync(model.Username);
            if (userExist != null) 
                return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Status = "Error", Message = "User Already Exist" });
            
            User user = new User()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);

            if(!result.Succeeded) 
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response() { Status = "Error", Message = "User creatiion Failed" });
            }
            return Ok(new Response() {Status = "Succes",Message = "User created Succesfully" });
        }
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if(user != null && await userManager.CheckPasswordAsync(user,model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                };
                foreach(var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role,userRole));
                }

                var authSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecurityKey"]));
                var token = new JwtSecurityToken(
                    issuer: configuration["JWT:ValidIssuer"],
                    audience: configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(5),
                    claims : authClaims,
                    signingCredentials: new SigningCredentials(authSigninKey, SecurityAlgorithms.HmacSha256)
                    );
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            return Unauthorized();
        }


        /*[HttpPost]
        [Route("upload-img")]
        public async Task<IActionResult> UploadImage([FromForm]FileModel fileModel)
        {
            try
            {
                string path = Path.Combine(@"D:\\Projects\\authentication-autharization\\authentication-autharization\\UploadImg\\", fileModel.FileName);
                using(Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.File.CopyTo(stream);
                }
                Models.Image img = new Models.Image()
                {
                    ImagePath = path,
                    ImageTitle = fileModel.FileName
                };
                appDbContext.Images.Add(img);
                appDbContext.SaveChanges();

                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }*/
    }
}
