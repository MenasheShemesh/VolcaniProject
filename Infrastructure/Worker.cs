using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Infrastructure
{
    public class Worker
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public Address Address { get; set; }

        public Worker()
        {
        }

        public Worker(string firstName, string lastName, string city, string street, int houseNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = new Address(city, street, houseNumber);
        }

        public bool Equals(Worker other)
        {
            return this.FirstName == other.FirstName &&
                   this.LastName == other.LastName &&
                   this.Address.Equals(other.Address);
        }
    }
}
