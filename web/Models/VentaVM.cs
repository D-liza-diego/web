namespace web.Models
{
    public class VentaVM
    {
        public decimal Total { get; set; }
        public int Items { get; set; }
        public string Estado { get; set; } = null!;
        public int? Idcustomer { get; set; }
        public bool Visibilidad { get; set; }
        public int? IdComprobante { get; set; }
        public List<Salesdetail> Salesdetails { get; set; }=null!;
    }
}
