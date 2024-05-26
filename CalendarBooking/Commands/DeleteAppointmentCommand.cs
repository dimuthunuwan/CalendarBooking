using MediatR;

namespace CalendarBooking.Commands
{
    public record DeleteAppointmentCommand(DateTime DateTime) : IRequest<Unit>;
}

