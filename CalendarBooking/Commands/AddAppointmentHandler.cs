using CalendarBooking.Data;
using MediatR;

namespace CalendarBooking.Commands
{
    public class AddAppointmentHandler : IRequestHandler<AddAppointmentCommand, Unit>
    {
        private readonly BookingContext _context;

        public AddAppointmentHandler(BookingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(AddAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment { DateTime = request.DateTime };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
