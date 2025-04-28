using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using QwiikAppointmentService.Application.Common.Managers;
using QwiikAppointmentService.Application.Common.Options;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Domain.Constants;
using QwiikAppointmentService.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Login
{
    public class LoginHandler : IRequestHandler<Login, TokenResponseType>
    {
        private readonly IdentityManager _identityManager;
        private readonly JwtOptions _jwtOptions;
        public LoginHandler(IdentityManager identityManager,
                            IOptions<JwtOptions> jwtOptions)
        {
            _identityManager = identityManager;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<TokenResponseType> Handle(Login request, CancellationToken cancellationToken)
        {
            var isCheckByEmail = !string.IsNullOrWhiteSpace(request.Request.Email);
            var isCheckByUsername = !string.IsNullOrWhiteSpace(request.Request.Username);

            if (!isCheckByEmail && !isCheckByUsername)
            {
                throw new BadRequestException("Please provide either email or username to login.");
            }
            var user = new User()
            {
                Id = Guid.Empty.ToString()
            };
            if (isCheckByEmail)
            {
                user = await _identityManager.UserManager.FindByEmailAsync(request.Request.Email);

                if (user is null && !isCheckByUsername)
                {
                    throw new UnauthorisedException("User not found.");
                }
            }

            else if (isCheckByUsername)
            {
                user = await _identityManager.UserManager.FindByNameAsync(request.Request.Username);

                if (user is null && !isCheckByEmail)
                {
                    throw new UnauthorisedException("User not found.");
                }
            }

            if (user.Id == Guid.Empty.ToString())
            {
                throw new UnauthorisedException("User not found.");
            }

            var result = await _identityManager.SignInManager.CheckPasswordSignInAsync(user, request.Request.Password, false);

            if (!result.Succeeded)
            {
                throw new UnauthorisedException("Login failed.");
            }

            return await GenerateAccessToken(user);
        }

        private async Task<TokenResponseType> GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new(ClaimType.UserId, user.Id),
                new(ClaimType.UserName, user.UserName),
                new(ClaimType.PersonId, user.PersonId.ToString())
            };
            var roles = await _identityManager.UserManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(role => new Claim(ClaimType.Role, role)));


            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.AccessTokenExpiryMinutes);
            var securityToken = new JwtSecurityToken
            (
                issuer: _jwtOptions.ValidIssuer,
                audience: _jwtOptions.ValidAudience,
                claims: claims,
                expires: expires,
                signingCredentials: new SigningCredentials(secret, SecurityAlgorithms.HmacSha256)
            );


            var accessToken = new JwtSecurityTokenHandler().WriteToken(securityToken);


            return new TokenResponseType
            {
                AccessToken = accessToken,
                AccessTokenExpires = expires,
                Type = "Bearer",
                Username = user.UserName,
                UserId = user.Id,
                PersonId = user.PersonId
            };
        }
    }
}
