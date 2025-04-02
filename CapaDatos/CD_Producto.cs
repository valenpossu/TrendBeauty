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
    public class CD_Producto
    {
        public List<Producto> Listar()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarProductos", oconexion);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Costo = Convert.ToDecimal(dr["Costo"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    FechaCompra = dr["FechaCompra"].ToString(),
                                    oCategoria = new Categoria(){ IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                                    Observaciones = dr["Observaciones"].ToString(),
                                    NombreImagen = dr["NombreImagen"].ToString(),
                                    RutaImagen = dr["RutaImagen"].ToString(),
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Trae productos de acuerdo a su id
        public List<Producto> ListarProductosPorCod(string IdProducto)
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarProductosPorCod", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", IdProducto);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Costo = Convert.ToDecimal(dr["Costo"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    FechaCompra = dr["FechaCompra"].ToString(),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                                    Observaciones = dr["Observaciones"].ToString(),
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Trae productos de acuerdo a la categoria
        public List<Producto> ListarProductosPorCategoria(string IdCategoria)
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("Sp_ListarProductosPorCategoria", oconexion);
                    cmd.Parameters.AddWithValue("IdCategoria", IdCategoria);
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Costo = Convert.ToDecimal(dr["Costo"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    FechaCompra = dr["FechaCompra"].ToString(),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                                    Observaciones = dr["Observaciones"].ToString(),
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
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

        //Buscar productos por nombre y de acuerdo a la categoria
        public List<Producto> ListarProductosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            List<Producto> lista = new List<Producto>();

            bool res = ValidacionDato(DatoBusqueda);

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand();

                    if (res)
                    {
                        cmd = new SqlCommand("BuscarProductoPorCod", oconexion);
                        cmd.Parameters.AddWithValue("IdCategoria", IdCategoria);
                        cmd.Parameters.AddWithValue("Cod", DatoBusqueda);
                        cmd.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        cmd = new SqlCommand("BuscarProductoPorNombre", oconexion);
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
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                    Descripcion = dr["Descripcion"].ToString(),
                                    Costo = Convert.ToDecimal(dr["Costo"]),
                                    Stock = Convert.ToInt32(dr["Stock"]),
                                    FechaCompra = dr["FechaCompra"].ToString(),
                                    oCategoria = new Categoria() { IdCategoria = Convert.ToInt32(dr["IdCategoria"]), Nombre = dr["Categoria"].ToString() },
                                    Activo = Convert.ToBoolean(dr["Activo"].ToString()),
                                    Observaciones = dr["Observaciones"].ToString(),
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Listar productos que no estan en inventario
        public List<Producto> ListaProductosNoInventario()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT A.IdProducto,A.Nombre FROM Productos A");
                    sb.AppendLine("WHERE NOT EXISTS(SELECT IdProducto FROM Inventario I WHERE I.IdProducto = A.IdProducto)");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString()
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Devuelve el stock inicial de un producto que no haya sido registrado en inventario
        public List<Producto> ListaProductosNoInventarioStock(string id)
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT IdProducto,Stock FROM Productos WHERE IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdProducto",id);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Stock = Convert.ToInt32( dr["Stock"])
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Listar productos que estan en inventario
        public List<Producto> ListaProductosInventario()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("SELECT i.IdProducto,p.Nombre FROM Productos p");
                    sb.AppendLine("INNER JOIN Inventario i ON p.IdProducto = i.IdProducto");

                    SqlCommand cmd = new SqlCommand(sb.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"]),
                                    Nombre = dr["Nombre"].ToString(),
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        //Devuelve el stock inicial de un producto que no haya sido registrado en inventario
        public List<Producto> ListaProductosConStock(string id)
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "SELECT Productos.IdProducto FROM Productos INNER JOIN Inventario ON Inventario.IdProducto = Productos.IdProducto WHERE Productos.IdProducto = @IdProducto AND StockVigente > 0";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@IdProducto", id);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader()) //poder leer todos los resultados de la ejecucion del query
                    {
                        while (dr.Read()) //mientras esta leyendo agrega los datos al objeto lista
                        {
                            lista.Add(
                                new Producto
                                {
                                    IdProducto = Convert.ToInt32(dr["IdProducto"])
                                });

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                lista = new List<Producto>();
            }
            return lista;
        }

        public int Registrar(Producto oProducto, out string Mensaje)
        {
            int idautogenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_RegistrarProducto", oconexion);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Costo", oProducto.Costo);
                    cmd.Parameters.AddWithValue("Stock", oProducto.Stock);
                    cmd.Parameters.AddWithValue("FechaCompra",Convert.ToDateTime(oProducto.FechaCompra));
                    cmd.Parameters.AddWithValue("Activo", oProducto.Activo);
                    cmd.Parameters.AddWithValue("Observaciones", oProducto.Observaciones);
                    cmd.Parameters.AddWithValue("IdUsuario", oProducto.IdUsuario);
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

        public bool GuardarDatosImagen(Producto oProducto, out string Mensaje)
        {
            bool Resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    string query = "UPDATE Productos SET RutaImagen = @RutaImagen, NombreImagen = @NombreImagen WHERE IdProducto = @IdProducto";

                    SqlCommand cmd = new SqlCommand(query, oconexion);
                    cmd.Parameters.AddWithValue("@RutaImagen", oProducto.RutaImagen);
                    cmd.Parameters.AddWithValue("@NombreImagen", oProducto.NombreImagen);
                    cmd.Parameters.AddWithValue("@IdProducto", oProducto.IdProducto);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    if (cmd.ExecuteNonQuery() > 0) //validamos si las acciones realizadas son mayores a 0 (es decir si se realizo una accion)
                    {
                        Resultado = true;
                    }
                    else
                    {
                        Mensaje = "No se pudo Actualizar Imagen";
                    }
                }
            }
            catch (Exception ex)
            {
                Resultado = false;
                Mensaje = ex.Message;
            }

            return Resultado;
        }

        public bool Editar(Producto oProducto, out string Mensaje)
        {
            bool resultado = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cn))
                {
                    SqlCommand cmd = new SqlCommand("sp_EditarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", oProducto.IdProducto);
                    cmd.Parameters.AddWithValue("Nombre", oProducto.Nombre);
                    cmd.Parameters.AddWithValue("Descripcion", oProducto.Descripcion);
                    cmd.Parameters.AddWithValue("IdCategoria", oProducto.oCategoria.IdCategoria);
                    cmd.Parameters.AddWithValue("Costo", oProducto.Costo);
                    cmd.Parameters.AddWithValue("Stock", oProducto.Stock);
                    cmd.Parameters.AddWithValue("FechaCompra", Convert.ToDateTime(oProducto.FechaCompra));
                    cmd.Parameters.AddWithValue("Activo", oProducto.Activo);
                    cmd.Parameters.AddWithValue("Observaciones", oProducto.Observaciones);
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
                    SqlCommand cmd = new SqlCommand("sp_EliminarProducto", oconexion);
                    cmd.Parameters.AddWithValue("IdProducto", id);
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
