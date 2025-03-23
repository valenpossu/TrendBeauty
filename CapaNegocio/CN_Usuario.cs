using CapaDatos;
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Usuario
    {
        //instancia de la capa datos donde estan los metodos; asi podemos acceder a todos sus metodos
        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> listar()
        {
            return objCapaDato.listar();
        }

        public int Registrar(Usuario oUsers, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oUsers.Nombres) && string.IsNullOrWhiteSpace(oUsers.Nombres))
            {
                Mensaje = "Nombre de Usuario No Puede Ser Vacio";
            }
            else if (string.IsNullOrEmpty(oUsers.Apellidos) && string.IsNullOrWhiteSpace(oUsers.Apellidos))
            {
                Mensaje = "Apellido del Usuario No Puede Ser Vacio";
            }
            else if (string.IsNullOrEmpty(oUsers.Correo) && string.IsNullOrWhiteSpace(oUsers.Correo))
            {
                Mensaje = "Correo del Usuario No Puede Ser Vacio";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                string Clave = CN_Recursos.GenerarClave();
                string Asunto = "Creacion Cuenta 'GestionPeluqueria'";
                string mensaje_correo = "<h3>Su cuenta fue creada con exito</h3><br><p>Su contraseña para acceder es !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", Clave);

                bool respuesta = CN_Recursos.EnviarCorreo(oUsers.Correo, Asunto, mensaje_correo);

                if (respuesta) //validar si se envia el correo
                {
                    oUsers.Clave = CN_Recursos.ConvertirSha256(Clave);

                    return objCapaDato.Registrar(oUsers, out Mensaje);
                }
                else
                {
                    Mensaje = "No se puede enviar el correo";
                    return 0;
                }
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Usuario oUsers, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(oUsers.Nombres) || string.IsNullOrWhiteSpace(oUsers.Nombres))
            {
                Mensaje = "El nombre del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(oUsers.Apellidos) || string.IsNullOrWhiteSpace(oUsers.Apellidos))
            {
                Mensaje = "El apellido del usuario no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(oUsers.Correo) || string.IsNullOrWhiteSpace(oUsers.Correo))
            {
                Mensaje = "El correo del usuario no puede ser vacio";
            }

            if (string.IsNullOrEmpty(Mensaje)) //si el mensaje despues de las validaciones sigue vacion es porque no hay ningun error
            {
                return objCapaDato.Editar(oUsers, out Mensaje);
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

        public bool CambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objCapaDato.CambiarClave(idusuario,nuevaclave,out Mensaje);
        }

        public bool ReestablecerClave(int idusuario, string correo, out string Mensaje)
        {
            Mensaje = string.Empty;
            string NuevaClave = CN_Recursos.GenerarClave();
            bool Resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(NuevaClave), out Mensaje);

            if (Resultado)
            {
                string Asunto = "Contraseña Reestablecida";
                string mensaje_correo = "<h3>Su cuenta fue reestablecida con exito</h3><br><p>Su contraseña para acceder es !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", NuevaClave);

                bool respuesta = CN_Recursos.EnviarCorreo(correo, Asunto, mensaje_correo);

                if (respuesta) //validar si se envia el correo
                {
                    return true;
                }
                else
                {
                    Mensaje = "No se pudo enviar el correo";
                    return false;
                }
            }
            else
            {
                Mensaje = "No se pudo reestablecer la contraseña";
                return false;
            }

        }
    }
}
