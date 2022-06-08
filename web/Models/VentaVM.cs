namespace web.Models
{
    public class VentaVM
    {
        public decimal Total { get; set; }
        public int Items { get; set; }
        public string Comprobante { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public int? Idcustomer { get; set; }
        public bool? Visibilidad { get; set; } = null!;
      
        public List<Salesdetail> Salesdetails { get; set; }=null!;
    }
}
