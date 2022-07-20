namespace web.Models.Facturacion
{
    public class FacturacionBoleta
    {
        public string ublVersion { get; set; }
        public string tipoOperacion { get; set; }
        public string tipoDoc { get; set; }
        public string serie { get; set; }
        public string correlativo { get; set; }
        public string fechaEmision { get; set; }
        public formaPago formaPago { get; set; }//list
        public string tipoMoneda { get; set; }
        public client client { get; set; }//list
        public company company { get; set; }//list
        public double mtoOperGravadas { get; set; }
        public double mtoIGV { get; set; }
        public double valorVenta { get; set; }
        public double totalImpuestos { get; set; }
        public double subTotal { get; set; }
        public double mtoImpVenta { get; set; }
        public List<details> details { get; set; }//list
        public List<legends> legends { get; set; }//LIST
    }
}
