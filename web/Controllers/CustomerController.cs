using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using web.Models;

namespace web.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly webContext _context;

        public CustomerController(webContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id, int? page)
        {
            if (id != null)
            {
                var valores = _context.Customers.Find(id);
                return Json(valores);
            }
            if (page>0)
            {
                page = page;
            }
            else
            {
                page = 1;
            }
            int RegistrosPagina = 10;
            int RegistrosOmitidos = (int)(page - 1) * RegistrosPagina;
            int RegistrosTotales = _context.Customers.Count();
            float NumeroPaginas =  ((float)RegistrosTotales / RegistrosPagina);
            var registros = _context.Customers.OrderBy(s => s.Idcustomer).Skip(RegistrosOmitidos).Take(RegistrosPagina);
            ViewBag.TotalClientes = RegistrosTotales;
            ViewBag.PaginaActual = page;
            ViewBag.NumeroPaginas = (int) Math.Ceiling(NumeroPaginas);
            ViewBag.Registros = registros;
            return View();
        }
        [HttpPost]
        public IActionResult SaveCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return RedirectToAction(nameof(Index), customer);

        }
        [HttpGet]
        public IActionResult Verificar(string id)
        {
            //return Json("prueba");
            bool resultado = false;
            if (id != null)
            {
                List<Customer> ver = _context.Customers.Where(x => x.Dnicustomer == id).ToList();
                if (ver.Count > 0)
                {
                    resultado = true;
                    return Json(resultado);
                }
                else
                {
                    return Json(resultado);
                }
            }
            return NotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateCustomer(Customer customer)
        {
            if(ModelState.IsValid)
            {
                _context.Customers.Update(customer);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public JsonResult Confirmar(int id)
        {
            bool estado = true;
            List<Sale> ver = _context.Sales.Where(x => x.Idcustomer == id).ToList();
            if (ver.Count > 0)
            {
                estado = false;
                return Json(estado);
            }
            else
            {
                var customer = _context.Customers.FirstOrDefault(s => s.Idcustomer == id);
                if (customer != null)
                {
                    _context.Customers.Remove(customer);
                    _context.SaveChanges();
                    estado = true;
                }
                
                return Json(estado);
            }
            
        }
    }
}
