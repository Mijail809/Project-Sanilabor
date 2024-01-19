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
    public class CD_Consulta
    {
        public static List<Consulta> Listar(int IdEmpleado)
        {
            List<Consulta> rptListaNivel = new List<Consulta>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarConsultas", oConexion);
                cmd.Parameters.AddWithValue("IdEmpleado", IdEmpleado);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        rptListaNivel.Add(new Consulta()
                        {
                            IdConsulta = Convert.ToInt32(dr["IdConsulta"].ToString()),
                            IdEmpleado = new Empleado()
                            {
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"].ToString()),
                                Nombres = dr["Nombres"].ToString(),
                                Apellidos = dr["Apellidos"].ToString()
                                //Descripcion = dr["DescripcionPeriodo"].ToString(),
                            },
                            FechaConsulta = Convert.ToDateTime(dr["FechaConsulta"]),
                            Profesional = dr["Profesional"].ToString(),
                            MotivoConsulta = dr["MotivoConsulta"].ToString(),
                            EnfermedadActual = dr["EnfermedadActual"].ToString(),
                            Anamnesis = dr["Anamnesis"].ToString(),
                            OrientacionDiagnostica = dr["OrientacionDiagnostica"].ToString(),
                            Contigencia = dr["Contigencia"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"])
                        });
                    }
                    dr.Close();

                    return rptListaNivel;

                }
                catch (Exception ex)
                {
                    rptListaNivel = null;
                    return rptListaNivel;
                }
            }
        }

        public static bool Registrar(Consulta oconsulta)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarConsultas", oConexion);
                    cmd.Parameters.AddWithValue("IdEmpleado", oconsulta.IdEmpleado.IdEmpleado);
                    cmd.Parameters.AddWithValue("FechaConsulta", oconsulta.FechaConsulta);
                    cmd.Parameters.AddWithValue("Profesional", oconsulta.Profesional);
                    cmd.Parameters.AddWithValue("MotivoConsulta", oconsulta.MotivoConsulta);
                    cmd.Parameters.AddWithValue("EnfermedadActual", oconsulta.EnfermedadActual);
                    cmd.Parameters.AddWithValue("Anamnesis", string.IsNullOrEmpty(oconsulta.Anamnesis)? DBNull.Value : (object)oconsulta.Anamnesis);
                    cmd.Parameters.AddWithValue("OrientacionDiagnostica", string.IsNullOrEmpty(oconsulta.OrientacionDiagnostica)? DBNull.Value :(object)oconsulta.OrientacionDiagnostica);
                    cmd.Parameters.AddWithValue("Contigencia", oconsulta.Contigencia);
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //string.IsNullOrEmpty(oEmpleado.Riesgos2Enfermedad) ? DBNull.Value : (object)oEmpleado.Riesgos2Enfermedad);

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

        public static bool Editar(Consulta oConsulta)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarConsulta", oConexion);
                    cmd.Parameters.AddWithValue("IdConsulta", oConsulta.IdConsulta);
                    cmd.Parameters.AddWithValue("FechaConsulta", oConsulta.FechaConsulta);
                    cmd.Parameters.AddWithValue("Profesional", oConsulta.Profesional);
                    cmd.Parameters.AddWithValue("MotivoConsulta", oConsulta.MotivoConsulta);
                    cmd.Parameters.AddWithValue("EnfermedadActual", oConsulta.EnfermedadActual);
                    cmd.Parameters.AddWithValue("Anamnesis", oConsulta.Anamnesis);
                    cmd.Parameters.AddWithValue("OrientacionDiagnostica", oConsulta.OrientacionDiagnostica);
                    cmd.Parameters.AddWithValue("Contingencia", oConsulta.Contigencia);
                    cmd.Parameters.AddWithValue("Activo", oConsulta.Activo);
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

        public static bool Eliminar(int idConsulta)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("sp_EliminarConsulta", oConexion);
                    cmd.Parameters.AddWithValue("IdConsulta", idConsulta);
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
