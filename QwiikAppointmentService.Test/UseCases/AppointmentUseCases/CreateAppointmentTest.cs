using Moq;
using Newtonsoft.Json;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Test.UseCases.AppointmentUseCases
{
    public class CreateAppointmentTest
    {
        [Fact]
        public async Task CreateAppointment_ShouldPass()
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
                AppointmentId = 0,
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
                method.GetAppointmentByStartTime(It.IsAny<DateTime>(), default))
                    .ReturnsAsync((Appointment?)null);

            appointmentRepository.Setup(method =>
                method.Get(0, default))
                    .ReturnsAsync(mockedAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new CreateAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var requestObject = new CreateAppointmentRequestType
            {
                CustomerId = 2,
                AppointmentStart = (DateTime.UtcNow.Date + DateTime.UtcNow.TimeOfDay).AddHours(1)
            };


            var request = new CreateAppointment(requestObject);

            // act
            var response = await handler.Handle(request, default);

            // assert

            var expected = new AppointmentResponseType()
            {
                AppointmentId = 0,
                CustomerId = 2,
                AppointmentStartTime = utcNow.AddHours(1),
                AppointmentEndTime = utcNow.AddHours(2)
            };

            Assert.Equal(JsonConvert.SerializeObject(expected), JsonConvert.SerializeObject(response));
        }

        [Fact]
        public async Task CreateAppointment_ShouldFail_StartTimeEarlierThanNow()
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


            appointmentRepository.Setup(method =>
                method.GetAppointmentByStartTime(It.IsAny<DateTime>(), default))
                    .ReturnsAsync((Appointment?)null);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new CreateAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var requestObject = new CreateAppointmentRequestType
            {
                CustomerId = 2,
                AppointmentStart = utcNow.AddHours(-1)
            };


            var request = new CreateAppointment(requestObject);
            var exceptionMessage = "Appointment start time cannot be earlier than current time.";

            // act and assert
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public async Task CreateAppointment_ShouldFail_InvalidCustomer()
        {
            // arrange
            var appointmentRepository = new Mock<IAppointmentRepository>();
            var customerRepository = new Mock<ICustomerRepository>();
            var unitOfWork = new Mock<IUnitOfWork>();

            var utcNow = (DateTime.UtcNow.Date + new TimeSpan(DateTime.UtcNow.TimeOfDay.Hours, 0, 0));
            DateTime.SpecifyKind(utcNow, DateTimeKind.Utc);
            appointmentRepository.Setup(method =>
                method.GetAppointmentByStartTime(It.IsAny<DateTime>(), default))
                    .ReturnsAsync((Appointment?)null);


            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync((Customer?)null);


            var handler = new CreateAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var requestObject = new CreateAppointmentRequestType
            {
                CustomerId = 2,
                AppointmentStart = utcNow.AddHours(1)
            };


            var request = new CreateAppointment(requestObject);
            var exceptionMessage = "Please enter a valid customer.";

            // act and assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }

        [Fact]
        public async Task CreateAppointment_ShouldFail_AnotherAppointmentExistsInTheTimeSlot()
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

            var mockedExistingAppointmentResponse = new Appointment()
            {
                AppointmentId = 0,
                CustomerId = 2,
                AppointmentDateTimeStart = utcNow.AddHours(1),
                AppointmentDateTimeEnd = utcNow.AddHours(2),
                CreatedById = 2,
                LastUpdatedById = 2,
                CreatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                LastUpdatedDate = new DateTime(2025, 4, 26, 2, 0, 0, DateTimeKind.Utc),
                IsActive = true
            };

            appointmentRepository.Setup(method =>
                method.GetAppointmentByStartTime(It.IsAny<DateTime>(), default))
                    .ReturnsAsync(mockedExistingAppointmentResponse);

            customerRepository.Setup(method =>
                method.Get(It.IsAny<int>(), default))
                    .ReturnsAsync(mockedCustomerResponse);


            var handler = new CreateAppointmentHandler(
                appointmentRepository.Object,
                customerRepository.Object,
                unitOfWork.Object);

            var requestObject = new CreateAppointmentRequestType
            {
                CustomerId = 2,
                AppointmentStart = utcNow.AddHours(1)
            };


            var request = new CreateAppointment(requestObject);
            var exceptionMessage = "Another appointment already exists on the requested time.";

            // act and assert
            var ex = await Assert.ThrowsAsync<BadRequestException>(() => handler.Handle(request, default));
            Assert.Equal(exceptionMessage, ex.Message);
        }
    }
}
