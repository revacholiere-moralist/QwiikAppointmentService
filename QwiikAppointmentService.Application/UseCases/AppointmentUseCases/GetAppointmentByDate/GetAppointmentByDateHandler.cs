using MediatR;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;
using QwiikAppointmentService.Application.UseCases.AppointmentUseCases.Common;
using QwiikAppointmentService.Domain.Entities;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.GetAppointmentsByDate
{
    public class GetAppointmentsByDateHandler : IRequestHandler<GetAppointmentsByDate, List<AppointmentResponseType>>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        public GetAppointmentsByDateHandler(IAppointmentRepository appointmentRepository,
                                         ICustomerRepository customerRepository)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
        }

        public async Task<List<AppointmentResponseType>> Handle(GetAppointmentsByDate request, CancellationToken cancellationToken)
        {
            var response = new List<AppointmentResponseType>();
            var appointments = await _appointmentRepository.GetAppointmentsByDate(request.Request.AppointmentDateFilterStart, request.Request.AppointmentDateFilterEnd, cancellationToken);

            // if this request was made by a customer, check if the customer is valid and get the appointments for that customer only
            if (request.Request.CustomerId.HasValue)
            {
                var customer = await _customerRepository.Get(request.Request.CustomerId.Value, cancellationToken);
                if (customer is null)
                {
                    throw new NotFoundException("Please enter a valid customer.");
                }

                appointments = appointments.Where(x => x.CustomerId == request.Request.CustomerId.Value).ToList();
            }

            foreach (var appointment in appointments)
            {
                var appointmentResponse = new AppointmentResponseType
                {
                    AppointmentId = appointment.AppointmentId,
                    CustomerId = appointment.CustomerId,
                    AppointmentStartTime = appointment.AppointmentDateTimeStart,
                    AppointmentEndTime = appointment.AppointmentDateTimeEnd
                };

                response.Add(appointmentResponse);
            }

            return response;
        }
    }
}
