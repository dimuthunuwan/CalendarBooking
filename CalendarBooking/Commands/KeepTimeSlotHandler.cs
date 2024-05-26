using CalendarBooking.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBooking.Commands
{
    public class KeepTimeSlotHandler : IRequestHandler<KeepTimeSlotCommand, Unit>
    {
        private readonly BookingContext _context;

        public KeepTimeSlotHandler(BookingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(KeepTimeSlotCommand request, CancellationToken cancellationToken)
        {
            var now = DateTime.Now.Date.Add(request.Time);
            for (var day = 0; day < 365; day++)
            {
                var appointmentTime = now.AddDays(day);
                if (!_context.Appointments.Any(a => a.DateTime == appointmentTime))
                {
                    _context.Appointments.Add(new Appointment { DateTime = appointmentTime });
                    await _context.SaveChangesAsync(cancellationToken);
                    break;
                }
            }

            return Unit.Value;
        }
    }
}
