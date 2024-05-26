using MediatR;

namespace CalendarBooking.Commands
{
    public record KeepTimeSlotCommand(TimeSpan Time) : IRequest<Unit>;
}


