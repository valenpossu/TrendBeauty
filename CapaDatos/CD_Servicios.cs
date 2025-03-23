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
    public class CD_Servicios
    {
        public List<Servicios> Listar()
        {
            List<Servicios> lista = new List<Servicios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarServicios", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Servicios
                                {
                                    IdServicio = Convert.ToInt32(dr["IdServicio"]),
                                    NombreServicio = dr["NombreServicio"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"]),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString())
                                });

                        }
                    }

                }
            }
            catch (Exception)
            {
                lista = new List<Servicios>();
            }
            return lista;
        }

        public List<Servicios> ListarServiciosPorCategoria(string IdCategoria)
        {
            List<Servicios> lista = new List<Servicios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarServiciosPorCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria",IdCategoria);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Servicios
                                {
                                    IdServicio = Convert.ToInt32(dr["IdServicio"]),
                                    NombreServicio = dr["NombreServicio"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"]),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString())
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Servicios>();
            }
            return lista;
        }

        public List<Servicios> ListarServiciosPorCodigo(string IdServicio)
        {
            List<Servicios> lista = new List<Servicios>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarServiciosPorCod", oconexion);
                    cmd.Parameters.AddWithValue("IdServicio", IdServicio);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Servicios
                                {
                                    IdServicio = Convert.ToInt32(dr["IdServicio"]),
                                    NombreServicio = dr["NombreServicio"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"]),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString())
                                });
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Servicios>();
            }
            return lista;
        }

        public bool ValidacionDato(string DatoBusqueda)
        {
            try
            {
                float.Parse(DatoBusqueda);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<Servicios> ListarServiciosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            List<Servicios> lista = new List<Servicios>();

            bool res = ValidacionDato(DatoBusqueda);

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand();

                    if (res)
                    {
                        cmd = new SqlCommand("BuscarServicioPorCod", oconexion);
                        cmd.Parameters.AddWithValue("IdCategoria", IdCategoria);
                        cmd.Parameters.AddWithValue("Cod", DatoBusqueda);
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd = new SqlCommand("BuscarServicioPorNombre", oconexion);
                        cmd.Parameters.AddWithValue("IdCategoria", IdCategoria);
                        cmd.Parameters.AddWithValue("Nombre", DatoBusqueda);
                        cmd.CommandType = CommandType.StoredProcedure;
                    }

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Servicios
                                {
                                    IdServicio = Convert.ToInt32(dr["IdServicio"]),
                                    NombreServicio = dr["NombreServicio"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Valor = Convert.ToDecimal(dr["Valor"]),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString())
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Servicios>();
            }
            return lista;
        }

        public int Registrar(Servicios oServicios, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarServicios", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", oServicios.NombreServicio);
                    cmd.Parameters.AddWithValue("Descripcion", oServicios.Descripcion);
                    cmd.Parameters.AddWithValue("Valor", oServicios.Valor);
                    cmd.Parameters.AddWithValue("IdCategoria", oServicios.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Activo", oServicios.Activo);
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

        public bool Editar(Servicios oServicios, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarServicio", oconexion);
                    cmd.Parameters.AddWithValue("IdServicio", oServicios.IdServicio);
                    cmd.Parameters.AddWithValue("Nombre", oServicios.NombreServicio);
                    cmd.Parameters.AddWithValue("Descripcion", oServicios.Descripcion);
                    cmd.Parameters.AddWithValue("Valor", oServicios.Valor);
                    cmd.Parameters.AddWithValue("IdCategoria", oServicios.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Activo", oServicios.Activo);
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
    }
}
