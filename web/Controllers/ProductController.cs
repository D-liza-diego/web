using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        public readonly webContext _context;

        public ProductController(webContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {
            if (id == null)
            {
                var produ = _context.Products.Include(p => p.IdcategoriaNavigation).ToList();
                ViewData["x"] = new SelectList(_context.Categorias, "Idcategoria", "Catname");
                ViewBag.lista = produ;
                return View();
            }
            else
            {
                var p = _context.Products.Find(id);
                return Json(p);
            }

        }
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            var llenado = new Product()
            {
                Idproduct = product.Idproduct,
                Nameproduct = product.Nameproduct,
                Precio = product.Precio,
                Idcategoria = product.Idcategoria,
                Cantidad=product.Cantidad
            };
            _context.Add(llenado);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int id)
        {
            bool result = false;
            var pro = _context.Products.FirstOrDefault(s => s.Idproduct == id);
            if (pro != null)
            {
                _context.Products.Remove(pro);
                _context.SaveChanges();
                result = true;
            }
            return Json(result);
        }
        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            var actualizado = new Product()
            {
                Idproduct = product.Idproduct,
                Nameproduct = product.Nameproduct,
                Precio = product.Precio,
                Idcategoria = product.Idcategoria,
                Cantidad = product.Cantidad
            };
            _context.Update(actualizado);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
