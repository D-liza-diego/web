using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Salesdetails = new HashSet<Salesdetail>();
        }

        public int IdSale { get; set; }
        public decimal? Total { get; set; }
        public int Items { get; set; }
        public string Comprobante { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int? Idcustomer { get; set; }
        public bool? Borrado { get; set; }

        public virtual Customer? IdcustomerNavigation { get; set; }
        public virtual ICollection<Salesdetail> Salesdetails { get; set; }
    }
}
