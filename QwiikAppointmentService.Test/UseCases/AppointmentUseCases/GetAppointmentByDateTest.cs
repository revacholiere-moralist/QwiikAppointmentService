using Moq;
using Newtonsoft.Json;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Test.UseCases.AppointmentUseCases
{
    public class GetAppointmentsByDateTest
    {
        [Fact]
        public async Task GetAppointmentsByDate_ShouldPass_NoCustomerId()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var personRepository = new Mock<IPersonRepository>();

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

            var mockedAppointmentsResponse = new List<Appointment>();

            var mockedAppointmentResponse1 = new Appointment()
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

            var mockedAppointmentResponse2 = new Appointment()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 4, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            mockedAppointmentsResponse.Add(mockedAppointmentResponse1);
            mockedAppointmentsResponse.Add(mockedAppointmentResponse2);

            appointmentRepository.Setup(method =>
                method.GetAppointmentsByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), default))
                    .ReturnsAsync(mockedAppointmentsResponse);


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new GetAppointmentsByDateHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                personRepository.Object);

            var requestObject = new GetAppointmentsByDateRequestType
            {
                AppointmentDateFilterStart = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                AppointmentDateFilterEnd = new DateTime(2025, 4, 27, 23, 59, 59, DateTimeKind.Utc)
            };

            var request = new GetAppointmentsByDate(requestObject);

            // act
            var response = await handler.Handle(request, default);

            // assert
            var expecteds = new List<AppointmentResponseType>();

            var expected1 = new AppointmentResponseType()
            {
                AppointmentId = 2,
                CustomerId = 2,
                AppointmentStartTime = new DateTime(2025, 4, 27, 2, 0, 0, DateTimeKind.Utc),
                AppointmentEndTime = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc)
            };

            var expected2 = new AppointmentResponseType()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentStartTime = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                AppointmentEndTime = new DateTime(2025, 4, 27, 4, 0, 0, DateTimeKind.Utc)
            };

            expecteds.Add(expected1);
            expecteds.Add(expected2);

            Assert.Equal(JsonConvert.SerializeObject(expecteds), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task GetAppointmentsByDate_ShouldPass_HasCustomerId()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var personRepository = new Mock<IPersonRepository>();

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

            var mockedAppointmentsResponse = new List<Appointment>();

            var mockedAppointmentResponse1 = new Appointment()
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

            var mockedAppointmentResponse2 = new Appointment()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 4, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            mockedAppointmentsResponse.Add(mockedAppointmentResponse1);
            mockedAppointmentsResponse.Add(mockedAppointmentResponse2);

            appointmentRepository.Setup(method =>
                method.GetAppointmentsByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), default))
                    .ReturnsAsync(mockedAppointmentsResponse);


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new GetAppointmentsByDateHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                personRepository.Object);

            var requestObject = new GetAppointmentsByDateRequestType
            {
                AppointmentDateFilterStart = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                AppointmentDateFilterEnd = new DateTime(2025, 4, 27, 23, 59, 59, DateTimeKind.Utc),
                CustomerId = 3
            };

            var request = new GetAppointmentsByDate(requestObject);

            // act
            var response = await handler.Handle(request, default);

            // assert
            var expecteds = new List<AppointmentResponseType>();

            var expected2 = new AppointmentResponseType()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentStartTime = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                AppointmentEndTime = new DateTime(2025, 4, 27, 4, 0, 0, DateTimeKind.Utc)
            };

            expecteds.Add(expected2);

            Assert.Equal(JsonConvert.SerializeObject(expecteds), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task GetAppointmentsByDate_ShouldFail_InvalidCustomer()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var personRepository = new Mock<IPersonRepository>();

            var mockedAppointmentsResponse = new List<Appointment>();

            var mockedAppointmentResponse1 = new Appointment()
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

            var mockedAppointmentResponse2 = new Appointment()
            {
                AppointmentId = 2,
                CustomerId = 3,
                AppointmentDateTimeStart = new DateTime(2025, 4, 27, 3, 0, 0, DateTimeKind.Utc),
                AppointmentDateTimeEnd = new DateTime(2025, 4, 27, 4, 0, 0, DateTimeKind.Utc),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            mockedAppointmentsResponse.Add(mockedAppointmentResponse1);
            mockedAppointmentsResponse.Add(mockedAppointmentResponse2);

            appointmentRepository.Setup(method =>
                method.GetAppointmentsByDate(It.IsAny<DateTime>(), It.IsAny<DateTime>(), default))
                    .ReturnsAsync(mockedAppointmentsResponse);


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync((Customer?)null);


            var handler = new GetAppointmentsByDateHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                personRepository.Object);

            var requestObject = new GetAppointmentsByDateRequestType
            {
                AppointmentDateFilterStart = new DateTime(2025, 4, 27, 0, 0, 0, DateTimeKind.Utc),
                AppointmentDateFilterEnd = new DateTime(2025, 4, 27, 23, 59, 59, DateTimeKind.Utc),
                CustomerId = 4
            };

            var request = new GetAppointmentsByDate(requestObject);
            var exceptionMessage = "Please enter a valid customer.";

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

    }
}
