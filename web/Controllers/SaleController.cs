using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Nancy.Json;
using Newtonsoft.Json;
using Rotativa.AspNetCore;
using System.Net.Http.Headers;
using System.Text;
using web.Models;
using web.Models.Facturacion;


namespace web.Controllers
{
    public class SaleController : Controller
    {
        public readonly webContext _context;
        public SaleController(webContext context)
        {
            _context = context;
        }
        public IActionResult sunat(int id)
        {
            
            var ruc = _context.Empresa.Select(e => e.ruc).First();
            var razon = _context.Empresa.Select(e => e.razonSocial).First();
            var nombre = _context.Empresa.Select(e => e.nombreComercial).First();
            var direccion = _context.Empresa.Select(e => e.direccion).First();
            var dpto = _context.Empresa.Select(e => e.departamento).First();
            var provincia = _context.Empresa.Select(e => e.provincia).First();
            var dist = _context.Empresa.Select(e => e.distrito).First();
            var ubigeo = _context.Empresa.Select(e => e.ubigueo).First();
            var token = _context.Empresa.Select(e => e.token).First();


            var serie = _context.Sales.Include(i => i.IdComprobanteNavigation).Where(s => s.IdSale == id).Select(s => s.IdComprobanteNavigation.Codigo).First();
            var correlativo = _context.Numeracions.Where(q => q.IdSale == id).Select(d => d.NumeracionBoleta).First() ;
            var fecha = _context.Sales.Where(e => e.IdSale == id).Select(y => y.Fecha).First();
            
            string mes, segundos, minutos;
            if (fecha.Month < 10) { mes = "0" + fecha.Month; }
            else { mes = fecha.Month.ToString(); }

            if (fecha.Second < 10) { segundos = "0" + fecha.Second; }
            else { segundos= fecha.Second.ToString(); }

            if (fecha.Minute < 10) { minutos = "0" + fecha.Minute; }
            else { minutos = fecha.Minute.ToString(); }

            var fechaString = fecha.Year + "-" + mes + "-" + fecha.Day + "T" + fecha.Hour + ":" + minutos + ":" + segundos + "-05:00";

            var total = _context.Sales.Where(e => e.IdSale == id).Select(e => e.Total).First();
            var detalle = _context.Salesdetails.Include(a => a.IdProductNavigation).Where(d => d.IdSale == id).
                Select(details => new
                {
                    codProducto = details.IdProductNavigation.Codigo,
                    unidad = "NIU",
                    descripcion = details.IdProductNavigation.Nameproduct,
                    cantidad = details.Cantidad,
                    mtoValorUnitario = Math.Round((details.IdProductNavigation.Precio / 1.18),2),
                    mtoValorVenta = (details.IdProductNavigation.Precio * details.Cantidad) / 1.18,
                    mtoBaseIgv = (details.IdProductNavigation.Precio * details.Cantidad) / 1.18,
                    porcentajeIgv = 18,
                    igv = (details.IdProductNavigation.Precio * details.Cantidad) - ((details.IdProductNavigation.Precio * details.Cantidad) / 1.18),
                    tipAfeIgv = 10,
                    totalImpuestos = (details.IdProductNavigation.Precio * details.Cantidad) - ((details.IdProductNavigation.Precio * details.Cantidad) / 1.18),
                    mtoPrecioUnitario = details.IdProductNavigation.Precio
                }).ToList();

            List<details> lst = new List<details>();
            foreach (var item in detalle)
            {
                lst.Add(new details()
                {
                    codProducto = item.codProducto,
                    unidad = item.unidad,
                    descripcion = item.descripcion,
                    cantidad = item.cantidad,
                    mtoValorUnitario = item.mtoValorUnitario,
                    mtoValorVenta = item.mtoValorVenta,
                    mtoBaseIgv = item.mtoBaseIgv,
                    porcentajeIgv = item.porcentajeIgv,
                    igv = item.igv,
                    tipAfeIgv = item.tipAfeIgv,
                    totalImpuestos = item.totalImpuestos,
                    mtoPrecioUnitario = item.mtoPrecioUnitario,
                });
            };
            formaPago forma = new formaPago
            {
                  moneda = "PEN",
                  tipo= "Contado"
            };
            address address = new address
            {
                direccion = direccion,
                provincia = provincia,
                departamento = dpto,
                distrito = dist,
                ubigueo = ubigeo
            };
            company company = new company
            {
                ruc = ruc,
                razonSocial = razon,
                nombreComercial = nombre,
                address = address
            };
            
            client client = new client
            {
                tipoDoc = "1",
                numDoc = "00000000",
                rznSocial = "Cliente Varios"
            };
            FacturacionBoleta facturacionBoleta = new FacturacionBoleta
            {
                ublVersion = "2.1",
                tipoOperacion = "0101",
                tipoDoc = "03",
                serie = serie,
                correlativo = correlativo.ToString(),
                fechaEmision = fechaString,
                formaPago = forma,
                tipoMoneda = "PEN",
                client = client,
                company = company,
                mtoOperGravadas = ((double)total / 1.18),
                mtoIGV = ((double)total - (double)total / 1.18),
                valorVenta = ((double)total / 1.18),
                totalImpuestos = ((double)total - (double)total / 1.18),
                subTotal = (double)total,
                mtoImpVenta = (double)total,
                details= lst
            };
            string path = @"C:\Users\Diego\Desktop" + serie+"-"+correlativo.ToString()+".pdf";
            string url = "https://facturacion.apisperu.com/api/v1/invoice/pdf";
            var json = JsonConvert.SerializeObject(facturacionBoleta);
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
          
            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = http.PostAsync(url, stringContent))
                { 
                    var respuesta = response.Result.Content.ReadAsByteArrayAsync().Result;
                    System.IO.File.WriteAllBytesAsync(path, respuesta);
                    return File(respuesta, "application/pdf", serie+"-"+correlativo.ToString()+".pdf");
                }
            }
        }
       
        public JsonResult Detalle(int id)
        { 
            var listadoVenta = _context.Sales.Where(i=>i.IdSale==id).Include(i => i.IdcustomerNavigation)
                .Include(i=>i.IdComprobanteNavigation.Numeracions).Include(i=>i.Salesdetails).ToList();
            return Json(listadoVenta);
        }
        public JsonResult VerProducto(int id)
        {
            var detalle_producto = _context.Products.Where(p => p.Idproduct == id).ToList();
            return Json(detalle_producto);
        }
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                var s = _context.Sales.Find(id);
                return Json(s);
            }
            else
            {
                ViewBag.datos = _context.Sales.Include(s => s.IdcustomerNavigation).Include(s=>s.IdComprobanteNavigation).ToList();
                return View();
            }
           
        }
        [HttpPost]
        public JsonResult Eliminar(int? id)
        {
            if(id!= null)
            {
                Sale sale = _context.Sales.Find(id);
                sale.Visibilidad = !sale.Visibilidad;
                _context.Update(sale);
                _context.SaveChanges();
                return Json("hecho");
               
            }
            else
            {
                return Json("no hecho");
            }
                
        }
        public IActionResult Crear(int? id)
        {
            ViewData["p"] = new SelectList(_context.Products.Where(s=>s.Cantidad>=5), "Idproduct", "Nameproduct");
            ViewData["c"] = new SelectList(_context.Customers, "Idcustomer", "Namecustomer");
            ViewData["tipoComprobante"] = new SelectList(_context.TipoComprobantes, "Id", "Descripcion");
            ViewBag.clientes = _context.Customers.OrderBy(e => e.Idcustomer);
            ViewBag.productos = _context.Products.Include(o=>o.IdcategoriaNavigation).OrderBy(e => e.Idproduct);
            if (id != null)
            {
                var dato_producto = _context.Products.Find(id);
                return Json(dato_producto);
            }
            return View();
        }
        [HttpPost]
        public JsonResult Save([FromBody]VentaVM ventaVM)
        {

            Sale venta = new Sale
            {
                Total = ventaVM.Total,
                Estado = ventaVM.Estado,
                Idcustomer = ventaVM.Idcustomer,
                Items = ventaVM.Items,
                Visibilidad = ventaVM.Visibilidad,
                IdComprobante = ventaVM.IdComprobante,
                Fecha = DateTime.Now
            };

            foreach (var i in ventaVM.Salesdetails)
            {
                venta.Salesdetails.Add(i);
                var idpro = _context.Products.Find(i.IdProduct);
                idpro.Cantidad = idpro.Cantidad - i.Cantidad;
                _context.Update(idpro);
            }
            _context.Add(venta);
            _context.SaveChanges();
            if(venta.IdComprobante==1)
            {
                int sumarBoleta;
                var checkList = _context.Numeracions.Where(q => q.IdTipoComprobante == 1).ToList();
                if (checkList.Count == 0)
                {
                    sumarBoleta = 0;
                }
                else
                {
                    var ultimoRegistro = _context.Numeracions.Where(i => i.IdTipoComprobante == 1)
                        .OrderByDescending(e => e.NumeracionBoleta).Take(1).Select(s => s.NumeracionBoleta).First();
                    sumarBoleta = (int)ultimoRegistro;
                }
                Numeracion saveBoleta = new Numeracion
                {
                    NumeracionBoleta = sumarBoleta + 1,
                    NumeracionFactura = null,
                    IdTipoComprobante = venta.IdComprobante,
                    IdSale = venta.IdSale
                };
                _context.Add(saveBoleta);
            }
            if (venta.IdComprobante == 2)
            {
                int sumarFactura;
                var checkList = _context.Numeracions.Where(q => q.IdTipoComprobante == 2).ToList();
                if(checkList.Count==0)
                {
                    sumarFactura = 0;
                }
                else
                {
                    var ultimoRegistro = _context.Numeracions.Where(i => i.IdTipoComprobante == 2)
                        .OrderByDescending(e => e.NumeracionFactura).Take(1).Select(s => s.NumeracionFactura).First();
                    sumarFactura = (int) ultimoRegistro;
                }
                Numeracion saveFactura = new Numeracion
                {
                    NumeracionBoleta = null,
                    NumeracionFactura = sumarFactura + 1,
                    IdTipoComprobante = venta.IdComprobante,
                    IdSale = venta.IdSale
                };
                _context.Add(saveFactura);
            }
            //     var idUltimoFactura = 0;
            //     var ultimoRegistro = _context.Numeracions.Where(i => i.IdTipoComprobante == 2)
            //                         .OrderByDescending(e => e.NumeracionFactura).Take(1).Select(s => s.NumeracionFactura).First();
            //     //foreach (var x in ultimoRegistro)
            //     //{
            //     //    idUltimoFactura = x;
            //     //}
            //     //Numeracion factura = _context.Numeracions.Find(idUltimoFactura);
            //     //var maxValorFactura = factura.NumeracionFactura;
            _context.SaveChanges();
            return Json(true);
        }
        public JsonResult Ex(int id)
        {
            bool estado = false;
            var iddetalle = _context.Salesdetails.Where(p => p.IdSale == id).ToList();
            foreach (var i in iddetalle)
            {
                var idproducto = _context.Products.Find(i.IdProduct);
                idproducto.Cantidad += i.Cantidad;
                _context.Update(idproducto);
                _context.Salesdetails.Remove(i);
                _context.SaveChanges();
                estado = true;
            };
            var idsale = _context.Sales.FirstOrDefault(s => s.IdSale == id);
            _context.Sales.Remove(idsale);
            _context.SaveChanges();
            return Json(estado);
        }
        public IActionResult Reporte()
        {
            return View();
        }
        public JsonResult Datos(int id)
        {
            var detalle = _context.Salesdetails.Where(w => w.IdSale == id).Include(p => p.IdProductNavigation)
              .Include(c => c.IdSaleNavigation).ToList();
            return Json(detalle);
        }
        
        public JsonResult Letras(int valor)
        {
            decimal conversion = valor;
            var res = Conversion.NumeroALetras(conversion);
            return Json(res);
        }
    }
}
