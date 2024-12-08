using BookStoreAPI.DTOs.CustomerDTO;
using BookStoreAPI.Models;
using Microsoft.AspNetCore.Http;
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
        public IActionResult create(AddCustomerDTO customer)
        {
            Customer cust = new Customer()
            {
                Email = customer.email,
                UserName = customer.username,
                address = customer.Address,
                PhoneNumber = customer.phonenumber,
                fullname = customer.fullname,
            };
            IdentityResult result = userManager.CreateAsync(cust, customer.password).Result;
            if (result.Succeeded)
            {
                IdentityRole _role = roleManager.FindByNameAsync("customer").Result;
                // roleManager.FindByNameAsync("customer").Result;
                // await _role=  roleManager.FindByNameAsync("customer");
                if (_role != null)
                {
                    IdentityResult roleResult = userManager.AddToRoleAsync(cust, _role.Name).Result;
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
