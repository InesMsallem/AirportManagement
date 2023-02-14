using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Services
{
    public class ServiceFlight : IServiceFlight
    {
        public List<Flight> Flights { get; set; } = new List<Flight>();

        public IEnumerable<DateTime> GetFlightDate(string destination)
        {
            //List<DateTime> result = new List<DateTime>();
            //for (int i = 0; i < Flights.Count; i++)
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

            //IEnumerable<DateTime> Query = (IEnumerable<DateTime>)(from f in Flights 
            //                              where f.Destination == destination 
            //                              select f);
            //return Query.ToList();
            IEnumerable<DateTime> queryLambda = Flights.Where(l => l.Destination == destination).Select(l => l.FlightDate);
               return queryLambda;
        }

        public void GetFlight(string filterType, string filterValue)
        {
            switch (filterType)
            {
                case "Destination":
                    foreach (Flight flight in Flights)
                    {
                        if (flight.Destination == filterValue)
                        {
                            Console.WriteLine(flight);
                        }
                    }
                    break;

                case "FlightDate":
                    foreach (Flight flight in Flights)
                    {
                        if (flight.FlightDate == DateTime.Parse(filterValue))
                        {
                            Console.WriteLine(flight);
                        }
                    }
                    break;

                case "EstimatedDuration":
                    foreach (Flight flight in Flights)
                    {
                        if (flight.EstimatedDuration == int.Parse(filterValue))
                        {
                            Console.WriteLine(flight);
                        }

                    }
                    break;
            }
        }

        public void showFlightDetails(Plane plane)
        {
            var Query =
                 from f in Flights
                 where f.Plane == plane
                 select new { f.FlightDate, f.Destination };
            foreach (var f in Query)

            {
                Console.WriteLine("Flight date:" + " " + f.FlightDate + " Flight destination" + " " + f.Destination);
            }

        }
        public int ProgrammedFlightNumber(DateTime startDate)
        {
            //methode linQ
            //var req= from f in Flights
            //         where (f.FlightDate-startDate).TotalDays<=7
            //         && DateTime.Compare(f.FlightDate,startDate)>0
            //         select f;
            //return req.Count(); 
            //requete lambda
            return Flights.Where(f => ((f.FlightDate - startDate).TotalDays <= 7) && DateTime.Compare(f.FlightDate, startDate) > 0).Select(f => f).Count();

        }

        public double DurationAverage(string destination)
        {

            var req = from f in Flights
                      where f.Destination.Equals(destination)
                      select f.EstimatedDuration;

            var reqAverage = TestData.listFlights
                                .Where(f => f.Destination == destination)
                                .Average(f => f.EstimatedDuration);


            return req.Average();
        }

        public IEnumerable<Flight> OrderedDurationFlights()
        {
            var req = from f in Flights
                      orderby f.EstimatedDuration
                      select f;
            return req;
        }

        public IEnumerable<Passenger> Seniortravellers(Flight flight)
        {
            var req = from p in flight.Passengers.OfType<Traveller>()
                      orderby p.BirthDate ascending
                      select p;
            return req.Take(3);

        }

        public void DestinationGroupedFlights()
        {
            var req = from f in Flights
                      group f by f.Destination;
            foreach (var g in req)
            {
                Console.WriteLine("Destination :" + g.Key);

                foreach (var f in g)
                { Console.WriteLine("Décolage:" + f.FlightDate); }
            }
        }
        public Action<Plane> FlightDetailsDel;
        public Func<string, double> DurationAverageDel;

        public ServiceFlight()
        {
            FlightDetailsDel = plane=> {
                var Query =
                from f in Flights
                     where f.Plane == plane
                     select new { f.FlightDate, f.Destination };
                foreach (var f in Query)

                {
                    Console.WriteLine("Flight date:" + " " + f.FlightDate + " Flight destination" + " " + f.Destination);
                }

            };
            DurationAverageDel = DurationAverage;
        }
    }




}
