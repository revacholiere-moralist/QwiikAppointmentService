using MediatR;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointment
{
    public class GetAppointmentHandler : IRequestHandler<GetAppointment, AppointmentResponseType>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;

        public GetAppointmentHandler(IAppointmentRepository appointmentRepository,
                                         ICustomerRepository customerRepository)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
        }

        public async Task<AppointmentResponseType> Handle(GetAppointment request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);
            if (customer is null)
            {
                throw new NotFoundException("Please enter a valid customer.");
            }

            var appointment = await _appointmentRepository.Get(request.AppointmentId, cancellationToken);
            if (appointment is null)
            {
                throw new NotFoundException("Please enter a valid appointment.");
            }

            // check if appointment actually belongs to the customer

            if (appointment.CustomerId != request.CustomerId)
            {
                throw new UnauthorisedException("The customer does not have access to this appointment.");
            }

            var response = new AppointmentResponseType
            {
                AppointmentId = appointment.AppointmentId,
                CustomerId = appointment.CustomerId,
                AppointmentStartTime = appointment.AppointmentDateTimeStart,
                AppointmentEndTime = appointment.AppointmentDateTimeEnd
            };

            return response;
        }
    }
}
