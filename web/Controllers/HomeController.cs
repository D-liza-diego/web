using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using web.Models;
using web.Models.Reporte;

namespace web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
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

    }
}