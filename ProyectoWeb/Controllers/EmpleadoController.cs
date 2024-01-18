using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb.Controllers
{
    public class EmpleadoController : Controller
    {
        // GET: AreaLaboral
        public ActionResult Crear()
        {
            return View();
        }
        public JsonResult Listar()
        {
            List<Empleado> oListaAreaLaboral = CD_Empleado.Listar();
            return Json(new { data = oListaAreaLaboral }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Guardar(Empleado oEmpleado)
        {
            bool respuesta = false;

            if (oEmpleado.IdEmpleado == 0)
            {
                respuesta = CD_Empleado.Registrar(oEmpleado);
            }


            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult Eliminar(int idempleado = 0)
        {
            bool respuesta = CD_Empleado.Eliminar(idempleado);
            return Json(new { resultado = respuesta }, JsonRequestBehavior.AllowGet);
        }
    }
}