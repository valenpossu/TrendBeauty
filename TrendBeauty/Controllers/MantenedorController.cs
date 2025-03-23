using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionPeluqueria.Controllers
{
    [Authorize]
    public class MantenedorController : Controller
    {

        
        // GET: Mantenedor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Servicios()
        {
            return View();
        }

        public ActionResult Productos()
        {
            return View();
        }

        public ActionResult Compras()
        {
            return View();
        }

        #region Categoria
        [HttpGet]
        public JsonResult ListarCategorias()
        {
            List<Categoria> oLista = new List<Categoria>();

            oLista = new CN_Categoria().listar(); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para registrar y editar
        [HttpPost]
        public JsonResult GuardarCategoria(Categoria oCategorias)
        {
            object Resultado; //object nos permite que en la variable resultado podamos almacenar cualquier valor
            string Mensaje = string.Empty;

            if (oCategorias.IdCategoria == 0)
            {
                Resultado = new CN_Categoria().Registrar(oCategorias, out Mensaje);
            }
            else
            {
                Resultado = new CN_Categoria().Editar(oCategorias, out Mensaje); //out: parametro de salida
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet); //El json que se envia //declaramos una propiedad 'Resultado' que va a almacenar el valor de Resultado, igual para el Mensaje
        }

        [HttpPost]
        public JsonResult EliminarCategoria(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Categoria().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Servicios

        public JsonResult ListarServicios()
        {
            List<Servicios> oLista = new List<Servicios>();

            oLista = new CN_Servicios().listar(); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarServiciosPorCategoria(string IdCategoria)
        {
            List<Servicios> oLista = new List<Servicios>();

            oLista = new CN_Servicios().ListarServiciosPorCategoria(IdCategoria); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarServiciosPorCodigo(string IdServicio)
        {
            List<Servicios> oLista = new List<Servicios>();

            oLista = new CN_Servicios().ListarServiciosPorCodigo(IdServicio); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarServiciosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            List<Servicios> oLista = new List<Servicios>();

            oLista = new CN_Servicios().ListarServiciosPorDatoBusquedaCategoria(IdCategoria, DatoBusqueda); //almacena todo los elementos que encuentre de la clase CN_Productos especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarServicios(Servicios oServicios)
        {
            object Resultado; //object nos permite que en la variable resultado podamos almacenar cualquier valor
            string Mensaje = string.Empty;

            if (oServicios.IdServicio == 0)
            {
                Resultado = new CN_Servicios().Registrar(oServicios, out Mensaje);
            }
            else
            {
                Resultado = new CN_Servicios().Editar(oServicios, out Mensaje); //out: parametro de salida
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet); //El json que se envia //declaramos una propiedad 'Resultado' que va a almacenar el valor de Resultado, igual para el Mensaje
        }


        #endregion

        #region Productos

        [HttpGet]
        public JsonResult ListarProductos()
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().Listar(); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosPorCategoria(string IdCategoria)
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().ListarProductosPorCategoria(IdCategoria); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosPorCod(string IdProducto)
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().ListarProductosPorCod(IdProducto); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListaProductosConStock(string IdProducto)
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().ListaProductosConStock(IdProducto); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarProductosPorDatoBusquedaCategoria(string IdCategoria, string DatoBusqueda)
        {
            List<Producto> oLista = new List<Producto>();

            oLista = new CN_Producto().ListarProductosPorDatoBusquedaCategoria(IdCategoria,DatoBusqueda); //almacena todo los elementos que encuentre de la clase CN_Productos especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para registrar y editar
        [HttpPost]
        public JsonResult GuardarProducto(Producto oProducto)
        {
            object Resultado; //object nos permite que en la variable resultado podamos almacenar cualquier valor
            string Mensaje = string.Empty;


            if (oProducto.IdProducto == 0)
            {
                Resultado = new CN_Producto().Registrar(oProducto, out Mensaje);
            }
            else
            {
                Resultado = new CN_Producto().Editar(oProducto, out Mensaje); //out: parametro de salida
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet); //El json que se envia //declaramos una propiedad 'Resultado' que va a almacenar el valor de Resultado, igual para el Mensaje
        }

        [HttpPost]
        public JsonResult EliminarProducto(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Producto().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}