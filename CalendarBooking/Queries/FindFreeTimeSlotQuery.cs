using MediatR;

namespace CalendarBooking.Queries
{
    public record FindFreeTimeSlotQuery(DateTime Date) : IRequest<DateTime?>;
}

