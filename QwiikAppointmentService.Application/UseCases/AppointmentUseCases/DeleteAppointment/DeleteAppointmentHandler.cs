using MediatR;
using QwiikAppointmentService.Application.Exceptions;
using QwiikAppointmentService.Application.Repositories;

namespace QwiikAppointmentService.Application.UseCases.AppointmentUseCases.DeleteAppointment
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointment>
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAppointmentHandler(IAppointmentRepository appointmentRepository,
                                         ICustomerRepository customerRepository,
                                         IUnitOfWork unitOfWork)
        {
            _appointmentRepository = appointmentRepository;
            _customerRepository = customerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAppointment request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.Get(request.CustomerId, cancellationToken);
            if (customer is null)
            {
                throw new NotFoundException("Please enter a valid customer.");
            }

            var existingAppointment = await _appointmentRepository.Get(request.AppointmentId, cancellationToken);
            if (existingAppointment is null)
            {
                throw new NotFoundException("Appointment not found.");
            }

            if (existingAppointment.CustomerId != request.CustomerId)
            {
                throw new UnauthorisedException("The customer does not have access to this appointment.");
            }

            existingAppointment.IsActive = false;
            existingAppointment.LastUpdatedDate = DateTime.UtcNow;
            existingAppointment.LastUpdatedById = request.CustomerId;
            try
            {
                _appointmentRepository.Update(existingAppointment);
                await _unitOfWork.Save(cancellationToken);
            }
            catch (Exception ex)
            {
                // TODO: Maybe add logging here
                throw new InternalServerErrorException("Unable to delete appointment.");
            }
        }
    }
}
