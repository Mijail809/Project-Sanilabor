using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_Empleado
    {
        public static List<Empleado> Listar()
        {
            List<Empleado> rptEmpleado = new List<Empleado>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarEmpleado", oConexion);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptEmpleado.Add(new Empleado()
                        {
                            IdEmpleado  = Convert.ToInt32(dr["IdEmpleado"].ToString()),
                            DNI = dr["DNI"].ToString(),
                            Nombres = dr["Nombres"].ToString(),
                            Apellidos = dr["Apellidos"].ToString(),
                            //FechaNacimiento = Convert.ToDateTime(dr["FechaNacimiento"]),
                            PuestoTrabajo = dr["PuestoTrabajo"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])
                        });
                    }
                    dr.Close();

                    return rptEmpleado;

                }
                catch (Exception ex)
                {
                    rptEmpleado = null;
                    return rptEmpleado;
                }
            }
        }

        public static bool Registrar(Empleado oEmpleado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarEmpleados", oConexion);

                     var FlagFechaNacimiento = 0;
                     var FlagFechaBajaEmpleado = 0;
                     var FlagPropuestaAlta = 0;
                     var FlagAltaEfectiva = 0;
                     var FlagPropuestaIncapacidad = 0;
                    
                    if (Convert.ToString(oEmpleado.FechaNacimiento) == "1/1/0001 00:00:00")
                    {
                        FlagFechaNacimiento = 1;
                        oEmpleado.FechaNacimiento = DateTime.Now;
                    }
                    if (Convert.ToString(oEmpleado.FechaBajaEmpleado) == "1/1/0001 00:00:00")
                    {
                        FlagFechaBajaEmpleado = 1;
                        oEmpleado.FechaBajaEmpleado = DateTime.Now;
                    }
                    if (Convert.ToString(oEmpleado.FechaPropuestaAlta) == "1/1/0001 00:00:00")
                    {
                        FlagPropuestaAlta = 1;
                        oEmpleado.FechaPropuestaAlta = DateTime.Now;
                    }
                    if (Convert.ToString(oEmpleado.FechaAltaEfectiva) == "1/1/0001 00:00:00")
                    {
                        FlagAltaEfectiva = 1;
                        oEmpleado.FechaAltaEfectiva = DateTime.Now;
                    }
                    if (Convert.ToString(oEmpleado.FechaPropuestaIncapacidad) == "1/1/0001 00:00:00")
                    {
                        FlagPropuestaIncapacidad = 1;
                        oEmpleado.FechaPropuestaIncapacidad = DateTime.Now;
                    }
                    cmd.Parameters.AddWithValue("DNI", oEmpleado.DNI);
                    cmd.Parameters.AddWithValue("Telefono", string.IsNullOrEmpty(oEmpleado.Telefono)?DBNull.Value :(object)oEmpleado.Telefono);
                    cmd.Parameters.AddWithValue("Nombres", oEmpleado.Nombres);
                    cmd.Parameters.AddWithValue("Apellidos", oEmpleado.Apellidos);
                    cmd.Parameters.AddWithValue("FechaNacimiento", oEmpleado.FechaNacimiento);
                    cmd.Parameters.AddWithValue("Direccion", string.IsNullOrEmpty(oEmpleado.Direccion) ? DBNull.Value : (object)oEmpleado.Direccion);
                    cmd.Parameters.AddWithValue("CP", string.IsNullOrEmpty(oEmpleado.CP) ? DBNull.Value : (object)oEmpleado.CP);
                    cmd.Parameters.AddWithValue("Provincia ", string.IsNullOrEmpty(oEmpleado.Provincia) ? DBNull.Value : (object)oEmpleado.Provincia);
                    cmd.Parameters.AddWithValue("Poblacion ", string.IsNullOrEmpty(oEmpleado.Poblacion) ? DBNull.Value : (object)oEmpleado.Poblacion);
                    cmd.Parameters.AddWithValue("PuestoTrabajo", string.IsNullOrEmpty(oEmpleado.PuestoTrabajo) ? DBNull.Value : (object)oEmpleado.PuestoTrabajo);
                    cmd.Parameters.AddWithValue("Riesgos1Enfermedad", string.IsNullOrEmpty(oEmpleado.Riesgos1Enfermedad) ? DBNull.Value : (object)oEmpleado.Riesgos1Enfermedad);
                    cmd.Parameters.AddWithValue("Riesgos2Enfermedad", string.IsNullOrEmpty(oEmpleado.Riesgos2Enfermedad) ? DBNull.Value : (object)oEmpleado.Riesgos2Enfermedad);
                    cmd.Parameters.AddWithValue("Riesgos3Enfermedad", string.IsNullOrEmpty(oEmpleado.Riesgos3Enfermedad) ? DBNull.Value : (object)oEmpleado.Riesgos3Enfermedad);
                    cmd.Parameters.AddWithValue("Observaciones", string.IsNullOrEmpty(oEmpleado.Observaciones) ? DBNull.Value : (object)oEmpleado.Observaciones);
                    cmd.Parameters.AddWithValue("IdEstadoEmpleado", oEmpleado.IdEstadoEmpleado);
                    cmd.Parameters.AddWithValue("FechaBajaEmpleado", oEmpleado.FechaBajaEmpleado);
                    cmd.Parameters.AddWithValue("FechaPropuestaAlta", oEmpleado.FechaPropuestaAlta);
                    cmd.Parameters.AddWithValue("FechaAltaEfectiva ", oEmpleado.FechaAltaEfectiva);
                    cmd.Parameters.AddWithValue("FechaPropuestaIncapacidad", oEmpleado.FechaPropuestaIncapacidad);
                    cmd.Parameters.AddWithValue("FlagFechaNacimiento", FlagFechaNacimiento);
                    cmd.Parameters.AddWithValue("FlagFechaBajaEmpleado", FlagFechaBajaEmpleado);
                    cmd.Parameters.AddWithValue("FlagPropuestaAlta", FlagPropuestaAlta);
                    cmd.Parameters.AddWithValue("FlagAltaEfectiva", FlagAltaEfectiva);
                    cmd.Parameters.AddWithValue("FlagPropuestaIncapacidad", FlagPropuestaIncapacidad);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oConexion.Open();

                    cmd.ExecuteNonQuery();  

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }

        public static bool Eliminar(int idEmpleado)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", oConexion);
                    cmd.Parameters.AddWithValue("IdEmpleado", idEmpleado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    respuesta = false;
                }

            }

            return respuesta;

        }
    }

    
}
