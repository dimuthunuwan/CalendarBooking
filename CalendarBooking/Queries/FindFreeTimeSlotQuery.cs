using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CalendarBooking.Data;
using CalendarBooking.Queries;

namespace CalendarBooking.Queries
{
    public record FindFreeTimeSlotQuery(DateTime Date) : IRequest<DateTime?>;
}

