using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaModelo
{
    public class Empleado
    {
       public int IdEmpleado { get; set; }
        public string DNI { get; set; }
        public string Telefono { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public string CP { get; set; }
        public string Provincia { get; set; }
        public string Poblacion { get; set; }
        public string PuestoTrabajo { get; set; }
        public string Riesgos1Enfermedad { get; set; }
        public string Riesgos2Enfermedad { get; set; }
        public string Riesgos3Enfermedad { get; set; }
        public string Observaciones { get; set; }
        public int    IdEstadoEmpleado { get; set; }
        public DateTime FechaBajaEmpleado { get; set; }
        public DateTime FechaPropuestaAlta { get; set; }
        public DateTime FechaAltaEfectiva { get; set; }
        public DateTime FechaPropuestaIncapacidad { get; set; }
        public bool   Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
