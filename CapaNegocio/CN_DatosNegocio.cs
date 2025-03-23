using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaNegocio
{
    public class CN_DatosNegocio
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_DatosNegocio objCapaDato = new CD_DatosNegocio();

        public List<DatosNegocio> listar()
        {
            return objCapaDato.listar();
        }

        public int Registrar(DatosNegocio oDatosNegocio, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oDatosNegocio.Nombre) && string.IsNullOrWhiteSpace(oDatosNegocio.Nombre))
            {
                Mensaje = "Nombre de Negocio No Puede Ser Vacio";
            }
            else if (oDatosNegocio.Nit == 0)
            {
                Mensaje = "Ingrese Nit, si no Cuenta Con Uno Ingrese Cedula de Representante Legal";
            }
            else if (string.IsNullOrEmpty(oDatosNegocio.Direccion) && string.IsNullOrWhiteSpace(oDatosNegocio.Direccion))
            {
                Mensaje = "Direccion del Negocio No Puede Ser Vacio";
            }
            else if (string.IsNullOrEmpty(oDatosNegocio.Correo) && string.IsNullOrWhiteSpace(oDatosNegocio.Correo))
            {
                Mensaje = "Correo del Negocio No Puede Ser Vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(oDatosNegocio, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(DatosNegocio oDatosNegocio, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oDatosNegocio.Nombre) && string.IsNullOrWhiteSpace(oDatosNegocio.Nombre))
            {
                Mensaje = "Nombre de Negocio No Puede Ser Vacio";
            }
            else if (oDatosNegocio.Nit == 0)
            {
                Mensaje = "Ingrese Nit, si no Cuenta Con Uno Ingrese Cedula de Representante Legal";
            }
            else if (string.IsNullOrEmpty(oDatosNegocio.Direccion) && string.IsNullOrWhiteSpace(oDatosNegocio.Direccion))
            {
                Mensaje = "Direccion del Negocio No Puede Ser Vacio";
            }
            else if (string.IsNullOrEmpty(oDatosNegocio.Correo) && string.IsNullOrWhiteSpace(oDatosNegocio.Correo))
            {
                Mensaje = "Correo del Negocio No Puede Ser Vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(oDatosNegocio, out Mensaje);
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
