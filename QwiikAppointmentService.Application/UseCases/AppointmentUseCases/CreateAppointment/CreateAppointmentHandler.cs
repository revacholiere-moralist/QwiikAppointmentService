using MediatR;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.CreateAppointment
{
    public class CreateAppointmentHandler : IRequestHandler<CreateAppointment, AppointmentResponseType>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateAppointmentHandler(IAppointmentRepository appointmentRepository, 
                                         ICustomerRepository customerRepository,
                                         IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentResponseType> Handle(CreateAppointment request, CancellationToken cancellationToken)
        {
            // strip the second part if any
            var appointmentStartTime = request.Request.AppointmentStart.Date + new TimeSpan(request.Request.AppointmentStart.TimeOfDay.Hours, 0, 0);
            DateTime.SpecifyKind(appointmentStartTime, DateTimeKind.Utc);

            if (request.Request.AppointmentStart < DateTime.UtcNow)
            {
                throw new BadRequestException("Appointment start time cannot be earlier than current time.");
            }

            var customer = await _customerRepository.Get(request.Request.CustomerId, cancellationToken);
            if (customer is null)
            {
                throw new NotFoundException("Please enter a valid customer.");
            }

            var existingAppointment = await _appointmentRepository.GetAppointmentByStartTime(appointmentStartTime, cancellationToken);
            if (existingAppointment is not null)
            {
                throw new BadRequestException("Another appointment already exists on the requested time.");
            }

            // valid to add appointment, proceed to insert to db

            // let's assume each appointment will take an hour slot
            var appointmentTimeEnd = appointmentStartTime.AddHours(1);
            DateTime.SpecifyKind(appointmentTimeEnd, DateTimeKind.Utc);
            var appointment = new Appointment
            {
                CustomerId = request.Request.CustomerId,
                AppointmentDateTimeStart = appointmentStartTime,
                AppointmentDateTimeEnd = appointmentTimeEnd,
                CreatedById = request.Request.CustomerId,
                LastUpdatedById = request.Request.CustomerId,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedDate = DateTime.UtcNow,
                IsActive = true
            };

            try
            {
                _appointmentRepository.Create(appointment);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Maybe add logging here
                throw new InternalServerErrorException("Unable to create appointment.");
            }
            

            var insertedAppointment = await _appointmentRepository.Get(appointment.AppointmentId, cancellationToken);
            if (insertedAppointment is null)
            {
                throw new NotFoundException("Unable to retrieve the inserted appointment.");
            }

            var response = new AppointmentResponseType
            {
                AppointmentId = insertedAppointment.AppointmentId,
                CustomerId = insertedAppointment.CustomerId,
                AppointmentStartTime = insertedAppointment.AppointmentDateTimeStart,
                AppointmentEndTime = insertedAppointment.AppointmentDateTimeEnd
            };

            return response;
        }
    }

}
