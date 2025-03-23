using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Categoria
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_Categoria objCapaDato = new CD_Categoria();
        public List<Categoria> listar()
        {
            return objCapaDato.listar();
        }

        public int Registrar(Categoria oCategorias, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oCategorias.Nombre) || string.IsNullOrWhiteSpace(oCategorias.Nombre))
            {
                Mensaje = "La Descripcion de la categoria no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(oCategorias, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Categoria oCategorias, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oCategorias.Nombre) || string.IsNullOrWhiteSpace(oCategorias.Nombre))
            {
                Mensaje = "La Descripcion de la categoria no puede ser vacia";
            }

            if (string.IsNullOrEmpty(Mensaje)) //si el mensaje despues de las validaciones sigue vacion es porque no hay ningun error
            {
                return objCapaDato.Editar(oCategorias, out Mensaje);
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
