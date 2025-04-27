using MediatR;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.UpdateAppointment
{
    public class UpdateAppointmentHandler : IRequestHandler<UpdateAppointment, AppointmentResponseType>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateAppointmentHandler(IAppointmentRepository appointmentRepository,
                                         ICustomerRepository customerRepository,
                                         IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<AppointmentResponseType> Handle(UpdateAppointment request, CancellationToken cancellationToken)
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

            var existingAppointment = await _appointmentRepository.Get(request.Request.AppointmentId, cancellationToken);
            if (existingAppointment is null)
            {
                throw new NotFoundException("Appointment not found.");
            }

            if (existingAppointment.CustomerId != request.Request.CustomerId)
            {
                throw new UnauthorisedException("The customer does not have access to this appointment.");
            }

            var otherExistingAppointment = await _appointmentRepository.GetAppointmentByStartTime(appointmentStartTime, cancellationToken);
            if (otherExistingAppointment is not null)
            {
                throw new BadRequestException("Another appointment already exists on the requested time.");
            }

            // valid to update appointment, proceed to update db
            // let's assume each appointment will take an hour slot
            var appointmentTimeEnd = appointmentStartTime.AddHours(1);
            DateTime.SpecifyKind(appointmentTimeEnd, DateTimeKind.Utc);

            existingAppointment.AppointmentDateTimeStart = appointmentStartTime;
            existingAppointment.AppointmentDateTimeEnd = appointmentTimeEnd;
            existingAppointment.LastUpdatedDate = DateTime.UtcNow;
            existingAppointment.LastUpdatedById = customer.PersonId;

            try
            {
                _appointmentRepository.Update(existingAppointment);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Maybe add logging here
                throw new InternalServerErrorException("Unable to create appointment.");
            }


            var response = new AppointmentResponseType
            {
                AppointmentId = existingAppointment.AppointmentId,
                CustomerId = existingAppointment.CustomerId,
                AppointmentStartTime = existingAppointment.AppointmentDateTimeStart,
                AppointmentEndTime = existingAppointment.AppointmentDateTimeEnd
            };

            return response;
        }
    }
}
