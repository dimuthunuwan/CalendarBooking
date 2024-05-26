using System;
using System.Threading;
using System.Threading.Tasks;
using CalendarBooking.Commands;
using CalendarBooking.Data;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CalendarBooking.Tests.Commands
{
    public class DeleteAppointmentHandlerTests
    {
        [Fact]
        public async Task Handle_Should_DeleteAppointment()
        {
            var options = new DbContextOptionsBuilder<BookingContext>()
                .UseInMemoryDatabase(databaseName: "DeleteAppointmentTest")
                .Options;

            using var context = new BookingContext(options);
            var appointmentDate = new DateTime(2023, 5, 20, 14, 0, 0);
            context.Appointments.Add(new Appointment { DateTime = appointmentDate });
            await context.SaveChangesAsync();

            var handler = new DeleteAppointmentHandler(context);
            await handler.Handle(new DeleteAppointmentCommand(appointmentDate), CancellationToken.None);

            var appointment = await context.Appointments.FirstOrDefaultAsync(a => a.DateTime == appointmentDate);
            Assert.Null(appointment);
        }
    }
}
