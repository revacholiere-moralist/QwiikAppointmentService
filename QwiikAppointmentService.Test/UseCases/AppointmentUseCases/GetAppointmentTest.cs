using Moq;
using Newtonsoft.Json;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Test.UseCases.AppointmentUseCases
{
    public class GetAppointmentTest
    {
        [Fact]
        public async Task GetAppointment_ShouldPass()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

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
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 2, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedAppointmentResponse);


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new GetAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object);

            var request = new GetAppointment(2, 2);

            // act
            var response = await handler.Handle(request, default);

            // assert
            var expected = new AppointmentResponseType()
            {
                AppointmentId = 2,
                CustomerId = 2,
                AppointmentStartTime = new DateTime(2025, 4, 27, 2, 0, 0, DateTimeKind.Utc),
                AppointmentEndTime = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc)
            };

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task GetAppointment_ShouldFail_InvalidCustomer()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

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
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 2, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync((Customer?)null);

            var exceptionMessage = "Please enter a valid customer.";

            var handler = new GetAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object);

            var request = new GetAppointment(2, 2);

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);

        }

        [Fact]
        public async Task GetAppointment_ShouldFail_AppointmentNotFound()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

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

            appointmentRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync((Appointment?)null);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);

            var exceptionMessage = "Please enter a valid appointment.";

            var handler = new GetAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object);

            var request = new GetAppointment(2, 2);

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);

        }

        [Fact]
        public async Task GetAppointment_ShouldFail_UnauthorisedCustomer()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();

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
                CustomerId = 1,
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 2, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);

            var exceptionMessage = "The customer does not have access to this appointment.";

            var handler = new GetAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object);

            var request = new GetAppointment(2, 2);

            // act and assert
            var ex = await Assert.ThrowsAsync<UnauthorisedException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);

        }

    }
}
