using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Infrastructure
{
    public class Phone
    {
        [StringLength(3, MinimumLength = 3)]
        [RegularExpression("^05\\d$")]
        public string Prefix { get; set; }

        [StringLength(7, MinimumLength = 7)]
        [RegularExpression("^\\d{7}$")]
        public string Number { get; set; }

        public Phone()
        {
        }

        public Phone(string prefix, string number)
        {
            this.Prefix = prefix;
            this.Number = number;
        }
    }

}
