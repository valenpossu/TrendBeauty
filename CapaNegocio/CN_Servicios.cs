using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Servicios
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_Servicios objCapaDato = new CD_Servicios();
        public List<Servicios> listar()
        {
            return objCapaDato.Listar();
        }

        public List<Servicios> ListarServiciosPorCategoria(string IdCategoria)
        {
            return objCapaDato.ListarServiciosPorCategoria(IdCategoria);
        }

        public List<Servicios> ListarServiciosPorCodigo(string IdServicio)
        {
            return objCapaDato.ListarServiciosPorCodigo(IdServicio);
        }

        public List<Servicios> ListarServiciosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            return objCapaDato.ListarServiciosPorDatoBusquedaCategoria(IdCategoria, DatoBusqueda);
        }

        public int Registrar(Servicios oServicios, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oServicios.NombreServicio) && string.IsNullOrWhiteSpace(oServicios.NombreServicio))
            {
                Mensaje = "Nombre de Servicio No Puede Ser Vacio";
            }
            else if (oServicios.Valor == 0)
            {
                Mensaje = "Debe Ingresar Un Valor Para El Servicio";
            }
            else if (oServicios.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Ingresar Una Categoria Para El Servicio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                 return objCapaDato.Registrar(oServicios, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Servicios oServicios, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oServicios.NombreServicio) && string.IsNullOrWhiteSpace(oServicios.NombreServicio))
            {
                Mensaje = "Nombre de Servicio No Puede Ser Vacio";
            }
            else if (oServicios.Valor == 0)
            {
                Mensaje = "Debe Ingresar Un Valor Para El Servicio";
            }
            else if (oServicios.oCategoria.IdCategoria == 0)
            {
                Mensaje = "Debe Ingresar Una Categoria Para El Servicio";
            }

            if (string.IsNullOrEmpty(Mensaje)) //si el mensaje despues de las validaciones sigue vacion es porque no hay ningun error
            {
                return objCapaDato.Editar(oServicios, out Mensaje);
            }
            else
            {
                return false;
            }
        }
    }
}
