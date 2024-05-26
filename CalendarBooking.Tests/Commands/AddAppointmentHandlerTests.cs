using CalendarBooking.Commands;
using CalendarBooking.Data;
using Microsoft.EntityFrameworkCore;

namespace CalendarBooking.Tests.Commands
{
    public class AddAppointmentHandlerTests
    {
        [Fact]
        public async Task Handle_Should_AddAppointment()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "AddAppointmentTest")
                .Options;

            using var context = new BookingContext(options);
            var handler = new AddAppointmentHandler(context);

            var appointmentDate = new DateTime(2023, 5, 20, 14, 0, 0);
            await handler.Handle(new AddAppointmentCommand(appointmentDate), CancellationToken.None);

            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.DateTime == appointmentDate);
            Assert.NotNull(appointment);
        }
    }
}
