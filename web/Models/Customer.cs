using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }

        public int Idcustomer { get; set; }
        public string Dnicustomer { get; set; } = null!;
        public string Namecustomer { get; set; } = null!;
        public string Lastnamecustomer { get; set; } = null!;
        public string Phonecustomer { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
