using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;

namespace web.Controllers
{
    public class SaleController : Controller
    {
        public readonly webContext _context;

        public SaleController(webContext context)
        {
            _context = context;
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
                ViewBag.datos = _context.Sales.Include(s => s.IdcustomerNavigation).ToList();
                return View();
            }
           
        }
        [HttpPost]
        public JsonResult Eliminar(int? id)
        {
            if(id!= null)
            {
                Sale sale = _context.Sales.Find(id);
                sale.Borrado = false;
                _context.Update(sale);
                _context.SaveChanges();
                return Json("hecho");
               
            }
            else
            {
                return Json("no hecho");
            }
                
        }
        public IActionResult llenar(int? id)
        {
            ViewData["p"] = new SelectList(_context.Products.Where(s=>s.Cantidad>=5), "Idproduct", "Nameproduct");
            ViewData["c"] = new SelectList(_context.Customers, "Idcustomer", "Namecustomer");
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
            bool estado = false;

            Sale venta = new Sale 
            { 
                Total = ventaVM.Total, 
                Comprobante = ventaVM.Comprobante, 
                Estado = ventaVM.Estado, 
                Idcustomer = ventaVM.Idcustomer, 
                Items = ventaVM.Items, 
                Borrado=ventaVM.Borrado
            };

            foreach (var i in ventaVM.Salesdetails)
            {
                venta.Salesdetails.Add(i);
                var idpro= _context.Products.Find(i.IdProduct);
                idpro.Cantidad = idpro.Cantidad - i.Cantidad;
                _context.Update(idpro);
                estado = true;
            }
          
            _context.Add(venta);
            _context.SaveChanges();
            return Json(estado);
        }
       
    }
}
