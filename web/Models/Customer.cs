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
        public string Dnicustomer { get; set; }
        public string Namecustomer { get; set; }
        public string Lastnamecustomer { get; set; }
        public string Phonecustomer { get; set; }

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
