using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Producto
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_Producto objCapaDato = new CD_Producto();

        public List<Producto> Listar()
        {
            return objCapaDato.Listar();
        }

        public List<Producto> ListarProductosPorCategoria(string IdCategoria)
        {
            return objCapaDato.ListarProductosPorCategoria(IdCategoria);
        }

        public List<Producto> ListarProductosPorCod(string IdProducto)
        {
            return objCapaDato.ListarProductosPorCod(IdProducto);
        }

        
        public List<Producto> ListarProductosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            return objCapaDato.ListarProductosPorDatoBusquedaCategoria(IdCategoria, DatoBusqueda);
        }

        
        public List<Producto> ListaProductosNoInventario()
        {
            return objCapaDato.ListaProductosNoInventario();
        }

        public List<Producto> ListaProductosInventario()
        {
            return objCapaDato.ListaProductosInventario();
        }

        public List<Producto> ListaProductosNoInventarioStock(string id)
        {
            return objCapaDato.ListaProductosNoInventarioStock(id);
        }

        public List<Producto> ListaProductosConStock(string id)
        {
            return objCapaDato.ListaProductosConStock(id);
        }
        public int Registrar(Producto oProducto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oProducto.Nombre) || string.IsNullOrWhiteSpace(oProducto.Nombre))
            {
                Mensaje = "El Nombre del Producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(oProducto.Descripcion) || string.IsNullOrWhiteSpace(oProducto.Descripcion))
            {
                Mensaje = "La Descripcion del Producto no puede ser vacia";
            }
            else if (oProducto.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar una Categoria";
            }
            else if (oProducto.Costo == 0)
            {
                Mensaje = "Debe Ingresar El Costo del Producto";
            }
            else if (oProducto.Stock == 0)
            {
                Mensaje = "Debe Ingresar El Stock del Producto";
            }
            else if (oProducto.FechaCompra == "")
            {
                Mensaje = "Debe Ingresar La Fecha De Compra";
            }
            else if (oProducto.Stock == 0)
            {
                Mensaje = "Debe Ingresar Stock ";
            }
            else if (oProducto.IdUsuario == 0)
            {
                Mensaje = "Error Con la Sesion, No es Posible Guardar";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(oProducto, out Mensaje);
            }
            else
            {
                return 0;
            }

        }

        public bool Editar(Producto oProducto, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oProducto.Nombre) || string.IsNullOrWhiteSpace(oProducto.Nombre))
            {
                Mensaje = "El Nombre del Producto no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(oProducto.Descripcion) || string.IsNullOrWhiteSpace(oProducto.Descripcion))
            {
                Mensaje = "La Descripcion del Producto no puede ser vacia";
            }
            else if (oProducto.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Seleccionar una Categoria";
            }
            else if (oProducto.Costo == 0)
            {
                Mensaje = "Debe Ingresar El Costo del Producto";
            }
            else if (oProducto.Stock == 0)
            {
                Mensaje = "Debe Ingresar El Stock del Producto";
            }
            else if (oProducto.FechaCompra == "")
            {
                Mensaje = "Debe Ingresar La Fecha De Compra";
            }
            else if (oProducto.Stock == 0)
            {
                Mensaje = "Debe Ingresar Stock ";
            }

            if (string.IsNullOrEmpty(Mensaje)) //si el mensaje despues de las validaciones sigue vacion es porque no hay ningun error
            {
                return objCapaDato.Editar(oProducto, out Mensaje);
            }
            else
            {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }
    }
}
