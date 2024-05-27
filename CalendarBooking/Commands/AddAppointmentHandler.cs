using MediatR;
using Microsoft.EntityFrameworkCore;
using CalendarBooking.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

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
            // Check if the time slot is reserved
            if (IsReservedTimeSlot(request.DateTime))
            {
                throw new InvalidOperationException("This time slot is reserved and cannot be booked.");
            }

            // Check for conflicts with existing appointments
            var conflict = await _context.Appointments.AnyAsync(a => a.DateTime == request.DateTime, cancellationToken);
            if (conflict)
            {
                throw new InvalidOperationException("This time slot is already booked.");
            }

            // Add the new appointment
            var appointment = new Appointment { DateTime = request.DateTime };
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private bool IsReservedTimeSlot(DateTime dateTime)
        {
            // Check if the time slot is between 4 PM to 5 PM on the second day of the third week of any month
            if (dateTime.Hour == 16 && dateTime.Minute == 0)
            {
                var day = dateTime.Day;
                var weekNumber = (day - 1) / 7 + 1;
                if (weekNumber == 3 && dateTime.DayOfWeek == DayOfWeek.Tuesday)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
