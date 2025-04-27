using MediatR;
using QwiikAppointmentService.Application.Common.Managers;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Common;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.UseCases.AuthenticationUseCases.Register
{
    public class RegisterUserHandler : IRequestHandler<RegisterUser, UserResponseType>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IdentityManager _identityManager;
        public RegisterUserHandler(IPersonRepository personRepository,
                                    ICustomerRepository customerRepository,
                                    IUnitOfWork unitOfWork,
                                    IdentityManager identityManager)
        {
            _personRepository = personRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
            _identityManager = identityManager;
        }

        public async Task<UserResponseType> Handle(RegisterUser request, CancellationToken cancellationToken)
        {
            // register to aspnetuser
            var user = new User
            {
                FirstName = request.Request.FirstName,
                LastName = request.Request.LastName,
                UserName = request.Request.Username,
                Email = request.Request.Email
            };
            user.EmailConfirmed = true;
            var result = await _identityManager.UserManager.CreateAsync(user, request.Request.Password);

            var errors = result.Succeeded
               ? Array.Empty<string>()
               : result.Errors.Select(x => x.Description).ToArray();
            if (errors.Length > 0)
            {
                throw new BadRequestException("Error occurred when adding customer", errors);
            }

            await _identityManager.UserManager.AddToRoleAsync(user, "CUSTOMER");

            var response = new UserResponseType
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.UserName,
                Email = user.Email
            };

            // register to main person and customer table
            var customer = new Customer()
            {
                Username = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = request.Request.DateOfBirth,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                RegistrationDate = DateTime.UtcNow,
                IsActive = true
            };

            _personRepository.Create(customer);
            _customerRepository.Create(customer);

            await _unitOfWork.Save(cancellationToken);
            return response;
        }
    }
}
