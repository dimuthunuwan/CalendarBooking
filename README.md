# CalendarBooking

## Description
A simple console application to manage calendar bookings, demonstrating database development, C# programming, dependency injection, and unit testing.

## Setup
1. Clone the repository.
2. Run `dotnet restore` to install dependencies.
3. Update the database connection string in `BookingContext`.
4. Run `dotnet run` to start the application.
5. Use the following commands:
   - `ADD DD/MM hh:mm` to add an appointment.
   - `DELETE DD/MM hh:mm` to remove an appointment.
   - `FIND DD/MM` to find a free timeslot for the day.
   - `KEEP hh:mm` to keep a timeslot for any day.

## Areas of Improvement
If I had more time, I would:
1. Add more comprehensive unit tests.
2. Implement logging to track application events and errors.
3. Create a more sophisticated scheduling algorithm to handle edge cases.
4. Enhance error handling and input validation.
5. Implement a more robust data access layer with repository patterns.
6. Integrate CI/CD pipelines for automated testing and deployment.
