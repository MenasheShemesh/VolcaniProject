using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Infrastructure
{
    public class Address
    {
        [Required]
        public string City { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        [Range(1, 999)]
        public int HouseNumber { get; set; }

        public Address()
        {
        }

        public Address(string city, string street, int houseNumber)
        {
            this.City = city;
            this.Street = street;
            this.HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{this.Street} {this.HouseNumber}, {this.City}";
        }

        public bool Equals(Address other)
        {
            return this.City == other.City &&
                   this.Street == other.Street &&
                   this.HouseNumber == other.HouseNumber;
        }
    }
}
