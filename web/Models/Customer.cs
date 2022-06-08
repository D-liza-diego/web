using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Sales = new HashSet<Sale>();
        }

        public int Idcustomer { get; set; }
        [Display(Name = "DNI del cliente")]
        public string Dnicustomer { get; set; } = null!;
        [Display(Name = "Nombre del cliente")]
        public string Namecustomer { get; set; } = null!;
        [Display(Name = "Apellidos del cliente")]
        public string Lastnamecustomer { get; set; } = null!;
        [Display(Name = "Contacto del cliente")]
        public string Phonecustomer { get; set; } = null!;

        public virtual ICollection<Sale> Sales { get; set; }
    }
}
