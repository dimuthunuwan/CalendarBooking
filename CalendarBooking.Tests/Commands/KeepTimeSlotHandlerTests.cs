using CalendarBooking.Commands;
using CalendarBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarBooking.Tests.Commands
{
    public class KeepTimeSlotHandlerTests
    {
        [Fact]
        public async Task Handle_Should_KeepTimeSlot()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "KeepTimeSlotTest")
                .Options;

            using var context = new BookingContext(options);
            var handler = new KeepTimeSlotHandler(context);

            var time = new TimeSpan(10, 0, 0); // 10:00 AM
            await handler.Handle(new KeepTimeSlotCommand(time), CancellationToken.None);

            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.DateTime.TimeOfDay == time);
            Assert.NotNull(appointment);
        }
    }
}
