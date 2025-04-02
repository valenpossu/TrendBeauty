using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;

namespace GestionPeluqueria.Controllers
{
    [Authorize]
    public class NegocioController : Controller
    {
        // GET: Negocio
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TiendasFisicas()
        {
            return View();
        }

        #region Negocio
        public JsonResult ListarNegocio()
        {
            List<DatosNegocio> oLista = new List<DatosNegocio>();

            oLista = new CN_DatosNegocio().listar();

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para registrar y editar
        [HttpPost]
        public JsonResult GuardarNegocio(DatosNegocio oDatosNegocio)
        {
            object Resultado; //object nos permite que en la variable resultado podamos almacenar cualquier valor
            string Mensaje = string.Empty;

            if (oDatosNegocio.IdNegocio == 0)
            {
                Resultado = new CN_DatosNegocio().Registrar(oDatosNegocio, out Mensaje);
            }
            else
            {
                Resultado = new CN_DatosNegocio().Editar(oDatosNegocio, out Mensaje); //out: parametro de salida
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet); //El json que se envia //declaramos una propiedad 'Resultado' que va a almacenar el valor de Resultado, igual para el Mensaje
        }

        [HttpPost]
        public JsonResult EliminarNegocio(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_DatosNegocio().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}