using CalendarBooking.Data;
using CalendarBooking.Queries;
using Microsoft.EntityFrameworkCore;

namespace CalendarBooking.Tests.Queries
{
    public class FindFreeTimeSlotHandlerTests
    {
        [Fact]
        public async Task Handle_Should_FindFreeTimeSlot()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "FindFreeTimeSlotTest")
                .Options;

            using var context = new BookingContext(options);
            var handler = new FindFreeTimeSlotHandler(context);

            var date = new DateTime(2023, 5, 20);
            var freeSlot = await handler.Handle(new FindFreeTimeSlotQuery(date), CancellationToken.None);

            Assert.NotNull(freeSlot);
            Assert.Equal(new DateTime(2023, 5, 20, 9, 0, 0), freeSlot);
        }
    }
}
