using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class Numeracion
    {
        public int Id { get; set; }
        public int? NumeracionBoleta { get; set; }
        public int? NumeracionFactura { get; set; }
        public int? IdTipoComprobante { get; set; }
        public int? IdSale { get; set; }

        public virtual Sale IdSaleNavigation { get; set; }
        public virtual TipoComprobante IdTipoComprobanteNavigation { get; set; }
    }
}
