using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Sale
    {
        public Sale()
        {
            Numeracions = new HashSet<Numeracion>();
            Salesdetails = new HashSet<Salesdetail>();
        }

        public int IdSale { get; set; }
        public decimal? Total { get; set; }
        public int Items { get; set; }
      
        public string Estado { get; set; }
        public int? Idcustomer { get; set; }
        public DateTime Fecha { get; set; }
        public bool Visibilidad { get; set; }
        public int? IdComprobante { get; set; }

        public virtual TipoComprobante IdComprobanteNavigation { get; set; }
        public virtual Customer IdcustomerNavigation { get; set; }
        public virtual ICollection<Numeracion> Numeracions { get; set; }
        public virtual ICollection<Salesdetail> Salesdetails { get; set; }
    }
}
