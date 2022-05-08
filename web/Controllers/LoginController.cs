using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using web.Models;

namespace web.Controllers
{
    public class LoginController : Controller
    {
        private readonly webContext _context;

        public LoginController(webContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Ingresar(Usuario model)
        {
            var login = _context.Usuarios.Where(a => a.Name == model.Name && a.Dni == model.Dni).FirstOrDefault();
            if (login != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login.Name),
                    new Claim (ClaimTypes.Role, login.Rol)
                };
                var claimsID = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsID));
                HttpContext.Session.SetString("usuario", login.Name);
                return RedirectToAction("Index", "Home", Json(claims));
               
            }
            else
            {
                return View("Index");
            }
        }
        public IActionResult logout()
        {
            //HttpContext.Session.Remove("usuario");
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}
