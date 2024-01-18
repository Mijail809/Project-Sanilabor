using CapaDatos;
using CapaModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProyectoWeb.Controllers
{
    public class ConsultasController : Controller
    {
        // GET: Consultas
        public ActionResult Crear()
        {
            return View();
        }
        public JsonResult Listar(int idEmpleado = 0)
        {
            //List<Empleado> oListaAreaLaboral = CD_Empleado.Listar();
            //return Json(new { data = oListaAreaLaboral }, JsonRequestBehavior.AllowGet);

            List<Consulta> oListaConsultas = CD_Consulta.Listar();

            if (oListaConsultas != null)
            {
                oListaConsultas = oListaConsultas.Where(x => x.IdEmpleado.IdEmpleado == idEmpleado).ToList();

            }

            return Json(oListaConsultas, JsonRequestBehavior.AllowGet);
        }
    }
}