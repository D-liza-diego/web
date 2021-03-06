using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class UsuarioController : Controller
    {
        private readonly webContext _context;

        public UsuarioController(webContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            //IEnumerable<Usuario> listausuario = _context.Usuarios;
            var list = _context.Usuarios.Where(x=>x.Rol!="Administrador").ToList();
            ViewBag.lista = list;
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                //TempData["mensaje"] = "Se creado el usuario exitosamente";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public JsonResult ObtenerDatos(int id)
        {
            var datos = _context.Usuarios.Find(id);
            return Json(datos);
        }
        public JsonResult GuardarCliente(Usuario usuario)
        {
            var estado = true;
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return Json(estado);
        }
        public JsonResult ModificarCliente(Usuario usuario)
        {
            var estado = true;
            _context.Usuarios.Update(usuario);
            _context.SaveChanges();
            return Json(estado);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Usuario usuario)
        {

            if (ModelState.IsValid)
            {
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Borrar(Usuario usuario)
        {
            if (usuario == null)
            {
                return NotFound();
            }
            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));

        }
    }
}
