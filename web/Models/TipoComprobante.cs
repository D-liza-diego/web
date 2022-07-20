using System;
using System.Collections.Generic;

namespace web.Models
{
    public partial class TipoComprobante
    {
        public TipoComprobante()
        {
            Numeracions = new HashSet<Numeracion>();
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<Numeracion> Numeracions { get; set; }
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
