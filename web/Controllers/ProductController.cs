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
        public JsonResult CantidadVerificar(int id )
        {
            var produ = _context.Products.Include(p => p.IdcategoriaNavigation).Where(w=>w.Idproduct==id).Select(d=>d.Cantidad).ToList();
            return Json(produ);
        }
        [HttpPost]
        public IActionResult SaveProduct(Product product)
        {
            var guardar = new Product()
            {
                Idproduct = product.Idproduct,
                Nameproduct = product.Nameproduct,
                Precio = product.Precio,
                Idcategoria = product.Idcategoria,
                Cantidad=product.Cantidad,
                
            };
            _context.Add(guardar);
            _context.SaveChanges();
            var productID = guardar.Idproduct;
            Product producto = _context.Products.Find(productID);
            if (productID < 10) { producto.Codigo = "P000" + productID; }
            if (productID > 10 && productID <100 ) { producto.Codigo = "P00" + productID; }
            if (productID > 100 && productID < 1000) { producto.Codigo = "P0" + productID; }
            if (productID > 1000 && productID < 10000) { producto.Codigo = "P" + productID; }
            _context.Update(producto);
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
                Codigo= product.Codigo,
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
