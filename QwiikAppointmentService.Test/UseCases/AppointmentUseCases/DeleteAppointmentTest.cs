using Moq;
using Newtonsoft.Json;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Test.UseCases.AppointmentUseCases
{
    public class DeleteAppointmentTest
    {
        [Fact]
        public async Task DeleteAppointment_ShouldPass()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var utcNow = (DateTime.UtcNow.Date + new TimeSpan(DateTime.UtcNow.TimeOfDay.Hours, 0, 0));
            DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
            var mockedCustomerResponse = new Customer()
            {
                PersonId = 2,
                Username = "CustomerTest",
                Email = "customer@test.com",
                FirstName = "Customer",
                LastName = "Test",
                DateOfBirth = new DateTime(2015, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedById = 1,
                LastUpdatedById = 1,
                CreatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                IsActive = true,
                RegistrationDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            };

            var mockedAppointmentResponse = new Appointment()
            {
                AppointmentId = 2,
                CustomerId = 2,
                AppointmentDateTimeStart = utcNow.AddHours(1),
                AppointmentDateTimeEnd = utcNow.AddHours(2),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = utcNow,
                LastUpdatedDate = utcNow,
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.Get(2, default))
                    .ReturnsAsync(mockedAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new DeleteAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);


            var request = new DeleteAppointment(2, 2);

            // act and assert
            await handler.Handle(request, default);
        }

        [Fact]
        public async Task DeleteAppointment_ShouldFail_InvalidCustomer()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync((Customer?)null);


            var handler = new DeleteAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var request = new DeleteAppointment(2, 2);
            var exceptionMessage = "Please enter a valid customer.";

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public async Task UpdateAppointment_ShouldFail_AppointmentDoesNotExist()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var mockedCustomerResponse = new Customer()
            {
                PersonId = 2,
                Username = "CustomerTest",
                Email = "customer@test.com",
                FirstName = "Customer",
                LastName = "Test",
                DateOfBirth = new DateTime(2015, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedById = 1,
                LastUpdatedById = 1,
                CreatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                IsActive = true,
                RegistrationDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            };


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);

            appointmentRepository.Setup(method =>
                method.Get(2, default))
                    .ReturnsAsync((Appointment?)null);

            var handler = new DeleteAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var request = new DeleteAppointment(2, 2);
            var exceptionMessage = "Appointment not found.";

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public async Task DeleteAppointment_ShouldFail_CustomerDoesNotHaveAccess()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var utcNow = (DateTime.UtcNow.Date + new TimeSpan(DateTime.UtcNow.TimeOfDay.Hours, 0, 0));
            DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
            var mockedCustomerResponse = new Customer()
            {
                PersonId = 2,
                Username = "CustomerTest",
                Email = "customer@test.com",
                FirstName = "Customer",
                LastName = "Test",
                DateOfBirth = new DateTime(2015, 12, 12, 0, 0, 0, DateTimeKind.Utc),
                CreatedById = 1,
                LastUpdatedById = 1,
                CreatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                IsActive = true,
                RegistrationDate = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc)
            };

            var mockedAppointmentResponse = new Appointment()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentDateTimeStart = utcNow.AddHours(1),
                AppointmentDateTimeEnd = utcNow.AddHours(2),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = utcNow,
                LastUpdatedDate = utcNow,
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.Get(2, default))
                    .ReturnsAsync(mockedAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new DeleteAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);


            var request = new DeleteAppointment(2, 2);

            // act and assert
            var exceptionMessage = "The customer does not have access to this appointment.";

            // act and assert
            var ex = await Assert.ThrowsAsync<UnauthorisedException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

    }
}
