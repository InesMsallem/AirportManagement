using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServiceFlight : IServiceFlight
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();

        public void DestinationGroupedFlight()
        {
            var query = from f in Flights
                        group f by f.Destination;
            foreach (var g in query)
            {
                Console.WriteLine("Destination:" + g.Key);
                foreach (var v in g)
                {
                    Console.WriteLine("decolage:" + v.FlightDate);
                }
            }
        }

        public double DurationAverage(string destination)
        {
            var query = from f in Flights
                        where f.Destination == destination
                        select f.EstimatedDuration;
            return query.Average();

        }

        public IEnumerable<DateTime> getDateFlight(string destination)
        {
            List<DateTime> result = new List<DateTime>();
            //for(int i = 0; i< Flights.Count; i++)
            //{
            //    if (Flights[i].Destination == destination)
            //    {
            //        result.Add(Flights[i].FlightDate);
            //    }
            //}
            //return result;
            //foreach (var flight in Flights)
            //{
            //    if (flight.Destination == destination)
            //    {
            //        result.Add(flight.FlightDate);
            //    }
            //}
            //return result;
            IEnumerable<DateTime> query = from f in Flights
                                          where f.Destination == destination
                                          select f.FlightDate;
            return query;
        }
        public void GetFlights(string filterType, string filterValue)
        {
            switch (filterType)
            {
                case "Destination":
                    foreach (Flight f in Flights)
                    {
                        if (f.Destination.Equals(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
                case "EstimatedDuration":
                    foreach (Flight f in Flights)
                    {
                        if (f.EstimatedDuration == int.Parse(filterValue))
                            Console.WriteLine(f);
                    }
                    break;
            }
        }

        public IEnumerable<Flight> OrderDurationFlight()
        {
            var query = from f in Flights
                        orderby f.EstimatedDuration descending
                        select f;
            return query;

        }

        public int ProgrammeFlightNumber(DateTime startDate)
        {
            var qeury = from f in Flights
                        where (DateTime.Compare(f.FlightDate, startDate) > 0
                        && (f.FlightDate - startDate).TotalDays < 7)
                        select f;
            return qeury.Count();
        }

        public IEnumerable<Traveller> SeniorTraveller(Flight flight)
        {
            var query = from p in flight.Passengers.OfType<Traveller>()
                        orderby p.BirthDate ascending
                        select p;
            return query;

        }

        public void ShowFlightDetails(Plane plane)
        {
            var query = from f in Flights
                        where f.Plane == plane
                        select f;
            foreach (var f in query)
            {
                Console.WriteLine("destination :" + f.Destination + "flightDate:" + f.FlightDate);
            }
        }

        public IEnumerable<DateTime> GetFlightDate(string destination)
        {
            throw new NotImplementedException();
        }

        public Action<Plane> FlightDetailsDel;
        public Func<string, double> DurationAverageDel;

        public ServiceFlight()
        {
            FlightDetailsDel = plane =>
            {
                var query = from f in Flights
                            where f.Plane == plane
                            select f;
                foreach (var f in query)
                {
                    Console.WriteLine("destination :" + f.Destination + "flightDate:" + f.FlightDate);
                }
            };


        }
    }

}