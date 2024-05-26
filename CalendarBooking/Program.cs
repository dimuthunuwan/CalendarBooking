// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using MediatR;
using CalendarBooking.Commands;
using CalendarBooking.Queries;
using CalendarBooking.Data;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var services = new ServiceCollection();
services.AddDbContext<BookingContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
var serviceProvider = services.BuildServiceProvider();

using (var scope = serviceProvider.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<BookingContext>();
    context.Database.Migrate();  // Apply any pending migrations and create the database if it does not exist
}

var mediator = serviceProvider.GetRequiredService<IMediator>();

while (true)
{
    Console.Write("Enter command: ");
    var command = Console.ReadLine();
    var parts = command.Split(' ');

    if (parts[0].ToUpper() == "ADD" && parts.Length == 3)
    {
        var date = DateTime.ParseExact($"{parts[1]} {parts[2]}", "dd/MM HH:mm", null);
        await mediator.Send(new AddAppointmentCommand(date));
        Console.WriteLine("Appointment added.");
    }
    else if (parts[0].ToUpper() == "DELETE" && parts.Length == 3)
    {
        var date = DateTime.ParseExact($"{parts[1]} {parts[2]}", "dd/MM HH:mm", null);
        await mediator.Send(new DeleteAppointmentCommand(date));
        Console.WriteLine("Appointment deleted.");
    }
    else if (parts[0].ToUpper() == "FIND" && parts.Length == 2)
    {
        var date = DateTime.ParseExact(parts[1], "dd/MM", null);
        var slot = await mediator.Send(new FindFreeTimeSlotQuery(date));
        Console.WriteLine(slot.HasValue ? $"Free slot found: {slot.Value:HH:mm}" : "No free slots found.");
    }
    else if (parts[0].ToUpper() == "KEEP" && parts.Length == 2)
    {
        var time = TimeSpan.Parse(parts[1]);
        await mediator.Send(new KeepTimeSlotCommand(time));
        Console.WriteLine("Time slot kept.");
    }
    else
    {
        Console.WriteLine("Invalid command.");
    }
}



