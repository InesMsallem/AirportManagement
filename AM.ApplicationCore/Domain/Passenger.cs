using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AM.ApplicationCore.Domain
{
    public class Passenger
    {
        public int Id { get; set; }
        public DateTime BirthDate { get; set; }
        public int PassportNumber { get; set; }
        public string EmailAdress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TelNumber { get; set; }
        public List<Flight> Flights { get; set; }

        public override string ToString()
        {
            return "FirstName & LastName: " +this.FirstName + this.LastName;
        }

        //public bool CheckProfile(string firstName, string lastName)
        //{
        //    return firstName == this.FirstName && lastName == this.LastName;
        //}

        //public bool CheckProfile(string firstName, string lastName,string email)
        //{
        //    return firstName == this.FirstName && lastName == this.LastName && 
        //        email == this.EmailAdress;
        //}

        public bool CheckProfile(string firstName, string lastName,string email = null)
        {
            if(email == null)
            {
                return firstName == this.FirstName && lastName == this.LastName;
            }
            else
            {
                return firstName == this.FirstName && lastName == this.LastName &&
                email == this.EmailAdress;
            }
        }

        public virtual void PassengerType()
        {
            Console.WriteLine("I am a passenger");
        }
    }
}
