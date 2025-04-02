using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace CapaNegocio
{
    public class CN_Recursos
    {
        //Metodo para generar clave automatica al usuario
        public static string GenerarClave()
        {
            string Clave = Guid.NewGuid().ToString("N").Substring(0, 6);//permite retornar un codigo unico desde c#; N: solo caracteres alfanumericos y la clave de hasta 6 digitos

            return Clave;
        }

        //Metodo para enviar el correo
        public static bool EnviarCorreo(string Correo, string Asunto, string Mensaje)
        {
            bool resultado = false;

            try
            {
                MailMessage mail = new MailMessage();
                mail.To.Add(Correo); //A donde se va a enviar el mensaje, a quien va dirigido.
                mail.From = new MailAddress("valenpossu@gmail.com");//desde que correo se esta enviando el mensaje
                mail.Subject = Asunto; //Asunto del correo
                mail.Body = Mensaje; //el cuerpo del correo
                mail.IsBodyHtml = true;

                //servidor que se va a encargar de enviar el mensaje
                var smtp = new SmtpClient()
                {
                    Credentials = new NetworkCredential("valenpossu@gmail.com", "gevlhuxrsqxvxjnz"), //contraseña generada desde gmail, verificacion en dos pasos (Activar) y luego contraseñas de aplaicaciones y se genera
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true
                };

                smtp.Send(mail); //envia el mensaje
                resultado = true;
            }
            catch (Exception ex)
            {
                resultado = false;
            }

            return resultado;
        }

        //Encriptar Clave
        public static string ConvertirSha256(string texto)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        public static string ConvertirBase64(string ruta, out bool conversion)
        {
            string textoBase64 = string.Empty;
            conversion = true;

            try
            {
                byte[] bytes = File.ReadAllBytes(ruta); //pasar la ruta donde se encuentra la imagen lo convierte en un array de bytes
                textoBase64 = Convert.ToBase64String(bytes);
            }
            catch
            {
                conversion = false;
            }

            return textoBase64;
        }
    }
}
