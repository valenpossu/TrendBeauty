using CapaEntidad;
using CapaNegocio;
using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GestionPeluqueria.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }

        public ActionResult Reportiador()
        {
            return View();
        }

        #region Usuarios
        [HttpGet]
        public JsonResult ListarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();

            oLista = new CN_Usuario().listar(); //almacena todo los elementos que encuentre de la clase CN_Usuarios especificamente del metodo listar

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        //Metodo para registrar y editar
        [HttpPost]
        public JsonResult GuardarUsuarios(Usuario oUsuario)
        {
            object Resultado; //object nos permite que en la variable resultado podamos almacenar cualquier valor
            string Mensaje = string.Empty;

            if (oUsuario.IdUsuario == 0)
            {
                Resultado = new CN_Usuario().Registrar(oUsuario, out Mensaje);
            }
            else
            {
                Resultado = new CN_Usuario().Editar(oUsuario, out Mensaje); //out: parametro de salida
            }

            return Json(new { Resultado = Resultado, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet); //El json que se envia //declaramos una propiedad 'Resultado' que va a almacenar el valor de Resultado, igual para el Mensaje
        }

        [HttpPost]
        public JsonResult EliminarUsuarios(int id)
        {
            bool respuesta = false;
            string Mensaje = string.Empty;

            respuesta = new CN_Usuario().Eliminar(id, out Mensaje);

            return Json(new { resultado = respuesta, Mensaje = Mensaje }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Reporte
        [HttpGet]
        public JsonResult VistaDashboard()
        {
            Dashboard objeto = new CN_Reportes().VerDashboard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListarReporte(string FechaInicio, string FechaFin)
        {
            List<Reporte> oLista = new List<Reporte>();

            oLista = new CN_Reportes().Ventas(FechaInicio, FechaFin);

            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public FileResult ExportarVenta(string FechaInicio, string FechaFin)
        {
            List<Reporte> lista = new List<Reporte>();

            lista = new CN_Reportes().Ventas(FechaInicio, FechaFin);

            DataTable dt = new DataTable();

            dt.Locale = new CultureInfo("es-CO");
            dt.Columns.Add("Fecha Venta", typeof(string));
            dt.Columns.Add("Producto", typeof(string));
            dt.Columns.Add("Cantidad", typeof(int));
            dt.Columns.Add("Valor Venta", typeof(decimal));
            dt.Columns.Add("Estado", typeof(string));
            dt.Columns.Add("Descuento", typeof(int));

            //enviarle cada elemento en que encuentre en la lista a la tabla(excel)
            foreach (Reporte rp in lista)
            {
                dt.Rows.Add(new object[]
                {
                    rp.FechaVenta,
                    rp.Producto,
                    rp.CantidadVendida,
                    rp.PrecioVenta,
                    rp.Estado,
                    rp.Descuento
                });
            }
            dt.TableName = "Datos";

            using (XLWorkbook wb = new XLWorkbook())
            {
                //agregamos la tabla en una hoja del documento que estamos creando
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())//Memoria stream
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Reporte" + DateTime.Now.ToString() + ".xlsx"); //especifico que el archivo es de tipo excel
                }
            }
        }
        #endregion
    }
}