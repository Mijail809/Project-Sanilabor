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
        public static List<Consulta> Listar()
        {
            List<Consulta> rptListaNivel = new List<Consulta>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("sp_ListarConsultas", oConexion);
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
                                IdEmpleado = Convert.ToInt32(dr["IdEmpleado"].ToString())
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
                    cmd.Parameters.AddWithValue("IdEmpleado", oconsulta.IdEmpleado);
                    cmd.Parameters.AddWithValue("FechaConsulta", oconsulta.FechaConsulta);
                    cmd.Parameters.AddWithValue("Profesional", oconsulta.Profesional);
                    cmd.Parameters.AddWithValue("MotivoConsulta", oconsulta.MotivoConsulta);
                    cmd.Parameters.AddWithValue("EnfermedadActual", oconsulta.EnfermedadActual);
                    cmd.Parameters.AddWithValue("Anamnesis", oconsulta.Anamnesis);
                    cmd.Parameters.AddWithValue("OrientacionDiagnostica", oconsulta.OrientacionDiagnostica);
                    cmd.Parameters.AddWithValue("Contigencia", oconsulta.Contigencia);
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
