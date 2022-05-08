using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Salesdetail
    {
        public int IdSaleDetail { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public int? IdSale { get; set; }
        public int? IdProduct { get; set; }

        public virtual Product? IdProductNavigation { get; set; }
        public virtual Sale? IdSaleNavigation { get; set; }
    }
}
