using BookStoreAPI.DTOs.AccountDTO;
using BookStoreAPI.DTOs.CustomerDTO;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        SignInManager<IdentityUser> signInManager;
        UserManager<IdentityUser> userManager;
        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }
        [HttpPost]
        public IActionResult LogIn(LoginDTO logindata)
        {
            var result = signInManager.PasswordSignInAsync(logindata.username, logindata.password, false, false).Result;
            if (result.Succeeded)
            {

                var _user = userManager.FindByNameAsync(logindata.username).Result;
                //generate token 
                #region Claims
                List<Claim> userdata = new List<Claim>();
                userdata.Add(new Claim(ClaimTypes.Name, _user.UserName));
                userdata.Add(new Claim(ClaimTypes.NameIdentifier, _user.Id));
                var Roles = userManager.GetRolesAsync(_user).Result;
                foreach (var Role in Roles)
                {
                    userdata.Add(new Claim(ClaimTypes.Role, Role));
                }

                #endregion
                #region generate Secret Key
                string key = "hello it is a new security key to generate token using JWT";
                var secretkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                #endregion
                var signingCredential = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

                #region Generate Token
                var token = new JwtSecurityToken(
                    claims: userdata,

                    signingCredentials: signingCredential,
                    expires: DateTime.Now.AddDays(1)

                    );
                #endregion
                var tokenstring=new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(tokenstring);
            }
            else
            {
                return Unauthorized("Invalid Username Or Password");
            }
        }


        //change Password 

        [HttpPost("changepassword")]
        [Authorize]
        public IActionResult changePassword(ChangePasswordDTO _changePassword)
        {
            if (ModelState.IsValid)
            {
                var cust = userManager.FindByNameAsync(User.Identity.Name).Result;
                if (cust == null) return NotFound();
                var result = userManager.ChangePasswordAsync(cust, _changePassword.old_password, _changePassword.newPassword).Result;
                if (result.Succeeded)
                {
                    return Ok("change Passsword Successfully");
                }
                else { return BadRequest(result.Errors); }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        //logout

        [HttpGet("logout")]
        [Authorize]
        //will excepire Token
        public IActionResult LogOut()
        {
            signInManager.SignOutAsync();
            return Ok();
        }
    }
}
