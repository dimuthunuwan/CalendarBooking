using CalendarBooking.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalendarBooking.Queries
{
    public class FindFreeTimeSlotHandler : IRequestHandler<FindFreeTimeSlotQuery, DateTime?>
    {
        private readonly BookingContext _context;

        public FindFreeTimeSlotHandler(BookingContext context)
        {
            _context = context;
        }

        public async Task<DateTime?> Handle(FindFreeTimeSlotQuery request, CancellationToken cancellationToken)
        {
            var start = request.Date.Date.AddHours(9);
            var end = request.Date.Date.AddHours(17);
            var reservedTime = request.Date.Date.AddDays(13).AddHours(16);

            var appointments = await _context.Appointments
                .Where(a => a.DateTime >= start && a.DateTime < end && a.DateTime != reservedTime)
                .Select(a => a.DateTime)
                .ToListAsync(cancellationToken);

            for (var time = start; time < end; time = time.AddMinutes(30))
            {
                if (!appointments.Contains(time) && time != reservedTime)
                {
                    return time;
                }
            }

            return null;
        }
    }
}
