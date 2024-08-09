using Microsoft.AspNetCore.Mvc;

namespace GestionEmpleados.Controllers
{
    public class AdministradorController : Controller
    {
        public IActionResult Empleados()
        {
            return View();
        }

        public IActionResult Roles()
        {
            return View();
        }

        public IActionResult Horarios()
        {
            return View();
        }
    }
}
