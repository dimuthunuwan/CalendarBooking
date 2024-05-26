using CalendarBooking.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBooking.Commands
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, Unit>
    {
        private readonly BookingContext _context;

        public DeleteAppointmentHandler(BookingContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments.FirstOrDefaultAsync(a => a.DateTime == request.DateTime, cancellationToken);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return Unit.Value;
        }
    }
}
