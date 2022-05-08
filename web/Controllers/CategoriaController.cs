using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly webContext _context;

        public CategoriaController(webContext context)
        {
            _context = context;
        }
        public IActionResult Index(int? id)
        {

            if (id == null)
            {
                var lista = _context.Categorias.ToList();
                ViewBag.ListaCategoria = lista;
                return View();
            }
            else
            {
                //return Json("mensaje" + id);
                var cat = _context.Categorias.Find(id);
                return Json(cat);
            }

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCategoria(Categoria categoria)
        {

            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");


        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCategoria(Categoria categoria)
        {
            _context.Categorias.Update(categoria);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            var cate = _context.Categorias.FirstOrDefault(s => s.Idcategoria == id);
            if (cate != null)
            {
                _context.Categorias.Remove(cate);
                _context.SaveChanges();
                result = true;
            }
            return Json(result);
        }
    }
}
