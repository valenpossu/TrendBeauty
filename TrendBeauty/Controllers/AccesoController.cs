using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Description;

namespace GestionPeluqueria.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CrearCuenta()
        {
            return View();
        }

        public ActionResult CambiarClave()
        {
            return View();
        }

        public ActionResult Reestablecer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string correo, string clave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuario().listar().Where(u => u.Correo == correo && u.Clave == CN_Recursos.ConvertirSha256(clave)).FirstOrDefault();

            if (oUsuario == null)
            {
                //ViewBag: guardar información y compartirla dentro de la misma vista; en este caso 'Index'- variable temporal; ViewBag.Error la palabra 'Error' puede ser cualquier nombre
                ViewBag.Error = "Correo o Contraseña Incorrectos";
                return View();
            }
            else
            {
                //condicional para reestablecer clave si Inicia sesion por primera vez
                if (oUsuario.Reestablecer)
                {
                    TempData["IdUsuario"] = oUsuario.IdUsuario; //TemData: Nos permite guardar informacion y compartirla entre multiples vistas que estan dentro de un mismo controlador - variable temporal

                    return RedirectToAction("CambiarClave"); //no es necesario poner el controlador porque estamos en el controlador
                }

                FormsAuthentication.SetAuthCookie(oUsuario.Correo, false); //creando una autenticacion del usuario por su correo
                Session["IdUsuario"] = oUsuario.IdUsuario;
                Session["Rol"] = oUsuario.Rol;

                //ViewBag: guardar información y compartirla con la vista - variable temporal; ViewBag.Error la palabra 'Error' puede ser cualquier nombre
                ViewBag.Error = null;

                return RedirectToAction("Index", "Home");

            }
        }

        [HttpPost]
        public ActionResult CrearUsuario(string nombre, string apellido, string correo, string clave)
        {
            string Mensaje;
            int Resultado = 0;

            Usuario oUsuario = new Usuario();
            oUsuario.Nombres = nombre;
            oUsuario.Apellidos = apellido;
            oUsuario.Correo = correo;   
            oUsuario.Clave = clave;

            Resultado = new CN_Usuario().Registrar(oUsuario, out Mensaje);

            if(Resultado == 0)
            {
                ViewBag.Error = Mensaje;
                return RedirectToAction("CrearUsuario");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public ActionResult CambiarClave(string idusuario, string claveactual, string nuevaclave, string confirmarclave)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuario().listar().Where(u => u.IdUsuario == int.Parse(idusuario)).FirstOrDefault();

            //validacion de la clave que es igual a la actual que tiene en el sistema
            if (oUsuario.Clave != CN_Recursos.ConvertirSha256(claveactual))
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = ""; //Nos permite almacenar valor mas simples como cadena de textos
                ViewBag.Error = "La contraseña actual no es correcta";
                return View();
            }
            else if (nuevaclave != confirmarclave)
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = claveactual; //guardamos temporalmente la clave actual no se borra si las contraseñas no coinciden
                ViewBag.Error = "Las contraseñas no coinciden";
                return View();
            }
            else if (string.IsNullOrEmpty(nuevaclave) || string.IsNullOrEmpty(confirmarclave) || string.IsNullOrWhiteSpace(nuevaclave) || string.IsNullOrWhiteSpace(confirmarclave))
            {
                TempData["IdUsuario"] = idusuario;
                ViewData["vclave"] = claveactual; //guardamos temporalmente la clave actual no se borra si las contraseñas no coinciden
                ViewBag.Error = "No Puede Ingresar Una Nueva Contraseña Vacia, Ingrese una";
                return View();
            }

            ViewData["vclave"] = null;

            nuevaclave = CN_Recursos.ConvertirSha256(nuevaclave);
            string Mensaje = string.Empty;
            bool respuesta = new CN_Usuario().CambiarClave(int.Parse(idusuario), nuevaclave, out Mensaje);

            if (respuesta)
            {
                return RedirectToAction("Index");
            }
            else
            {
                TempData["IdUsuario"] = idusuario;
                ViewBag.Error = Mensaje;
                return View();

            }
        }


        [HttpPost]
        public ActionResult Reestablecer(string correo)
        {
            Usuario oUsuario = new Usuario();
            oUsuario = new CN_Usuario().listar().Where(item => item.Correo == correo).FirstOrDefault();

            if (oUsuario == null)
            {
                ViewBag.Error = "No se encontró un usuario relacionado a ese correo";
                return View();
            }

            string Mensaje = string.Empty;
            bool respuesta = new CN_Usuario().ReestablecerClave(oUsuario.IdUsuario, correo, out Mensaje);

            if (respuesta)
            {
                ViewBag.Error = null;
                return RedirectToAction("Index", "Acceso");
            }
            else
            {
                ViewBag.Error = Mensaje;
                return View();
            }
        }

        public ActionResult CerrarSesion()
        {
            FormsAuthentication.SignOut(); //Elimina la autenticacion del usuario
            return RedirectToAction("Index", "Acceso");
        }
    }
}