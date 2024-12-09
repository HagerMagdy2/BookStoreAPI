using BookStoreAPI.DTOs.AdminDTOs;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        UserManager<IdentityUser> userManager;
        RoleManager<IdentityRole> roleManager;
        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        [HttpPost]
        public IActionResult create(AddAdminDTO admin)
        {
            Admin admin1 = new Admin()
            {
                Email = admin.email,
                UserName = admin.username,
                PhoneNumber = admin.phonenumber,
            };
            IdentityResult result = userManager.CreateAsync(admin1, admin.password).Result;
            if (result.Succeeded)
            {
                IdentityRole _role = roleManager.FindByNameAsync("admin").Result;
                // roleManager.FindByNameAsync("customer").Result;
                // await _role=  roleManager.FindByNameAsync("customer");
                if (_role != null)
                {
                    IdentityResult roleResult = userManager.AddToRoleAsync(admin1, _role.Name).Result;
                    if (roleResult.Succeeded)
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }

                }
                return BadRequest();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
