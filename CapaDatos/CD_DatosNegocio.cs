using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DatosNegocio
    {
        public List<DatosNegocio> listar()
        {
            List<DatosNegocio> lista = new List<DatosNegocio>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "select IdNegocio,Nombre,Nit,Direccion,Correo from DatosNegocio";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new DatosNegocio
                                {
                                    IdNegocio = Convert.ToInt32(dr["IdNegocio"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Nit = Convert.ToInt32(dr["Nit"]),
                                    Direccion = dr["Direccion"].ToString(),
                                    Correo = dr["Correo"].ToString()
                                });

                        }
                    }

                }
            }
            catch (Exception)
            {
                lista = new List<DatosNegocio>();
            }
            return lista;
        }

        public int Registrar(DatosNegocio oDatosNegocio, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarDatosNegocio", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", oDatosNegocio.Nombre);
                    cmd.Parameters.AddWithValue("Nit", oDatosNegocio.Nit);
                    cmd.Parameters.AddWithValue("Direccion", oDatosNegocio.Direccion);
                    cmd.Parameters.AddWithValue("Correo", oDatosNegocio.Correo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    idautogenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idautogenerado = 0;
                Mensaje = ex.Message;
            }

            return idautogenerado;
        }

        public bool Editar(DatosNegocio oDatosNegocio, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarDatosNegocio", oconexion);
                    cmd.Parameters.AddWithValue("IdNegocio", oDatosNegocio.IdNegocio);
                    cmd.Parameters.AddWithValue("Nombre", oDatosNegocio.Nombre);
                    cmd.Parameters.AddWithValue("Nit", oDatosNegocio.Nit);
                    cmd.Parameters.AddWithValue("Direccion", oDatosNegocio.Direccion);
                    cmd.Parameters.AddWithValue("Correo", oDatosNegocio.Correo);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();

                    resultado = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "DELETE TOP (1) FROM DatosNegocio WHERE IdNegocio = @id";
                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;


                    oconexion.Open();
                    resultado = cmd.ExecuteNonQuery() > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                resultado = false;
                Mensaje = ex.Message;
            }

            return resultado;
        }
    }
}
