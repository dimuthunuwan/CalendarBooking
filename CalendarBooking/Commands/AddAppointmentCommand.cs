using MediatR;

namespace CalendarBooking.Commands
{
    public record AddAppointmentCommand(DateTime DateTime) : IRequest<Unit>;
}


