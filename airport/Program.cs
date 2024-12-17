using System;
using System.Collections.Generic;

namespace flyinformation_lab6_
{
    class Program
    {
        static void Main(string[] args)
        {
            var flightSystem = new FlightInformationSystem();
            var queryHandler = new FlightQueryHandler(flightSystem);

            try
            {
                flightSystem.LoadFlightsFromJson(@"C:\Users\mynderka\source\repos\airport\airport\flights_data.json");
                Console.WriteLine("The flight database has been successfully loaded.\n");

                bool exit = false;
                while (!exit)
                {
                    Console.WriteLine("Select an option (1-6):");
                    Console.WriteLine("1 - Flights by airline company");
                    Console.WriteLine("2 - Delayed flights");
                    Console.WriteLine("3 - Flights on a specific date");
                    Console.WriteLine("4 - Flights within a time range to a specific destination");
                    Console.WriteLine("5 - Recent arrivals");
                    Console.WriteLine("6 - Exit");
                    string option = Console.ReadLine();
                    switch (option)
                    {
                        case "1":
                            Console.WriteLine("Enter the airline name:");
                            string airline = Console.ReadLine();
                            List<Flight> airlineFlights = queryHandler.GetFlightsByAirline(airline);
                            if (airlineFlights.Count > 0)
                            {
                                Console.WriteLine($"Flights for airline '{airline}':");
                                foreach (var flight in airlineFlights)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.Destination}, Departure: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("No flights found for this airline.");
                            }
                            break;

                        case "2":
                            List<Flight> delayedFlights = queryHandler.GetDelayedFlights();
                            if (delayedFlights.Count > 0)
                            {
                                Console.WriteLine("Delayed flights:");
                                foreach (var flight in delayedFlights)
                                    Console.WriteLine($"Flight {flight.FlightNumber}, Delay: {flight.Duration}");
                            }
                            else
                            {
                                Console.WriteLine("No delayed flights.");
                            }
                            break;

                        case "3":
                            Console.WriteLine("Enter the departure date (yyyy-MM-dd):");
                            DateTime date = DateTime.Parse(Console.ReadLine());
                            List<Flight> flightsByDate = queryHandler.GetFlightsByDepartureDate(date);
                            if (flightsByDate.Count > 0)
                            {
                                Console.WriteLine($"Flights on {date.ToShortDateString()}:");
                                foreach (var flight in flightsByDate)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.Destination}, Departure: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("No flights found for this date.");
                            }
                            break;

                        case "4":
                            Console.WriteLine("Enter the start date and time (yyyy-MM-dd HH:mm):");
                            DateTime startTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the end date and time (yyyy-MM-dd HH:mm):");
                            DateTime endTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the destination:");
                            string destination = Console.ReadLine();
                            List<Flight> flightsByTimeAndDestination = queryHandler.GetFlightsByTimeRangeAndDestination(startTime, endTime, destination);
                            if (flightsByTimeAndDestination.Count > 0)
                            {
                                Console.WriteLine($"Flights to {destination} within the specified time range:");
                                foreach (var flight in flightsByTimeAndDestination)
                                    Console.WriteLine($"{flight.FlightNumber} -> {flight.DepartureTime}, Departure: {flight.DepartureTime}");
                            }
                            else
                            {
                                Console.WriteLine("No flights found for the specified time range.");
                            }
                            break;

                        case "5":
                            Console.WriteLine("Enter the start date and time for arrivals (yyyy-MM-dd HH:mm):");
                            DateTime arrivalStartTime = DateTime.Parse(Console.ReadLine());
                            Console.WriteLine("Enter the end date and time for arrivals (yyyy-MM-dd HH:mm):");
                            DateTime arrivalEndTime = DateTime.Parse(Console.ReadLine());
                            List<Flight> recentArrivals = queryHandler.GetFlightsArrivedInTimeRange(arrivalStartTime, arrivalEndTime);
                            if (recentArrivals.Count > 0)
                            {
                                Console.WriteLine("Recent arrivals:");
                                foreach (var flight in recentArrivals)
                                    Console.WriteLine($"Flight {flight.FlightNumber} -> Arrival: {flight.ArrivalTime}");
                            }
                            else
                            {
                                Console.WriteLine("No recent arrivals found.");
                            }
                            break;

                        case "6":
                            exit = true;
                            Console.WriteLine("Goodbye!");
                            break;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}