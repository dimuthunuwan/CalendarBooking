using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System;
using CalendarBooking.Commands;
using CalendarBooking.Data;

namespace CalendarBooking.Commands
{
    public record KeepTimeSlotCommand(TimeSpan Time) : IRequest<Unit>;
}


