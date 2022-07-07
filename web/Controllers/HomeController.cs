using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using web.Models;
using web.Models.Reporte;

namespace web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly webContext _context;

        public HomeController(webContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles ="Administrador")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Denegado()
        {
            return View();
        }
        public IActionResult Obtener()
        {
            ViewBag.x = HttpContext.Session.GetString("usuario");
            return Json(ViewBag.x);
        }
        public JsonResult Reporte()
        {
            SP_productos x = new SP_productos();
            List<producto_vendido> producto_Vendidos = x.RetonarProductos();
            return Json(producto_Vendidos);
        }
        public JsonResult GraficoProductos()
        {
            SP_ProductoVendido procedimiento = new SP_ProductoVendido();
            List<ProductoVendido> lista = procedimiento.RetonarProductos();
            return Json(lista);
        }
        public JsonResult GraficoVentas()
        {
            SP_VentasMes procedimiento = new SP_VentasMes();
            List<VentasMes> lista = procedimiento.RetonarVentas();
            return Json(lista);
        }
        public JsonResult productSold(int mes)
        {
            //var sp = _context.Salesdetails.Join(_context.Products, ventadetalle => ventadetalle.IdProduct, producto => producto.Idproduct, (ventadetalle, producto) => new { ventadetalle, producto })
            //                                .Select(z => new { z.producto.Nameproduct, z.ventadetalle.Cantidad });

            //var sp = from detalle in _context.Salesdetails
            //         join producto in _context.Products on detalle.IdProduct equals producto.Idproduct
            //         select new { producto.Nameproduct, detalle.Cantidad};
            
            var fechaCheck = _context.Sales.Where(w => w.Fecha.Month == mes).Any();
            if (!fechaCheck)
            {
                var filtro = _context.Salesdetails.Include(x => x.IdProductNavigation).Include(a => a.IdSaleNavigation)
                .GroupBy(x => x.IdProductNavigation.Nameproduct).
                Select(x => new
                {
                    Producto = x.Key,
                    Total = x.Select(y => y.Cantidad).Sum()
                }); 
                var arrayFalse = new { data= filtro, fechaCheck };
                return Json(arrayFalse);
            }
           

            var filtroMes = _context.Salesdetails.Include(x => x.IdProductNavigation).Include(a => a.IdSaleNavigation)
                .Where(x => x.IdSaleNavigation.Fecha.Month == mes)
                .GroupBy(x => x.IdProductNavigation.Nameproduct).
                Select(x => new
                {
                    Mes = x.Select(y => y.IdSaleNavigation.Fecha.Month),
                    Producto = x.Key,
                    Total = x.Select(y => y.Cantidad).Sum()
                });
            var arrayTrue = new { data= filtroMes, fechaCheck };
            return Json(arrayTrue);

        }
        public JsonResult salesMade(int mes)
        {
            var fechaCheck = _context.Sales.Where(w => w.Fecha.Month == mes).Any();
            if(!fechaCheck)
            {
                var filtro = _context.Sales.GroupBy(z => z.Fecha.Month).
                Select(z => new
                {
                    Fecha = z.Key,
                    Total = z.Select(y => y.Total).Sum()
                });
                var arrayFalse = new { data = filtro, fechaCheck };
                return Json(arrayFalse);
            }

            var filtroMes = _context.Sales.Where(x => x.Fecha.Month == mes).GroupBy(z => z.Fecha.Month).
                Select(z => new
                {
                    Fecha = z.Key,
                    Total = z.Select(y => y.Total).Sum()
                });
            var arrayTrue = new { data = filtroMes, fechaCheck };
            return Json(arrayTrue);
        }
    }
}