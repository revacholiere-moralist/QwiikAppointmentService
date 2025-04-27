using Microsoft.AspNetCore.Identity;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.Common.Managers
{
    public class IdentityManager
    {
        public UserManager<User> UserManager { get; }
        public RoleManager<Role> RoleManager { get; }

        public IdentityManager(
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            UserManager = userManager;
            RoleManager = roleManager;
        }
    }
}
