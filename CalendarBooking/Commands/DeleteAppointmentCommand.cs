using CalendarBooking.Commands;
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
    public record DeleteAppointmentCommand(DateTime DateTime) : IRequest<Unit>;
}

