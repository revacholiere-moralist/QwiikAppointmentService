using Microsoft.AspNetCore.Identity;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Common.Managers
{
    public class IdentityManager
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }
        public SignInManager<User> SignInManager { get; }
        public IdentityManager(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            SignInManager<User> signInManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            SignInManager = signInManager;
        }
    }
}
