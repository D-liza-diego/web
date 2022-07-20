namespace web.Models.Facturacion
{
    public class details
    {
        public string codProducto { get; set; }
        public string unidad { get; set; }
        public string descripcion { get; set; }
        public double cantidad { get; set; }
        public double mtoValorUnitario { get; set; }
        public double mtoValorVenta { get; set; }
        public double mtoBaseIgv { get; set; }
        public int porcentajeIgv { get; set; }
        public double igv { get; set; }
        public int tipAfeIgv { get; set; }
        public double totalImpuestos { get; set; }
        public double mtoPrecioUnitario { get; set; }
    }
}
