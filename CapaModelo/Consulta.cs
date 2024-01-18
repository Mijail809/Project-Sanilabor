using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Consulta
    {
        public int IdConsulta { get; set; }
        public Empleado IdEmpleado { get; set; }
        public DateTime FechaConsulta { get; set; }
        public string Profesional { get; set; }
        public string MotivoConsulta { get; set; }
        public string EnfermedadActual { get; set; }
        public string Anamnesis { get; set; }
        public string OrientacionDiagnostica { get; set; }
        public string Contigencia { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
