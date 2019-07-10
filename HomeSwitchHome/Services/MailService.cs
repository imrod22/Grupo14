using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace HomeSwitchHome.Services
{
    public class MailService : IMailService
    {
        SmtpClient client;

        public MailService() {

            this.client = new SmtpClient();
            this.client.Port = 25;
            this.client.DeliveryMethod = SmtpDeliveryMethod.Network;
            this.client.UseDefaultCredentials = false;
            this.client.Host = "smtp.gmail.com";
            this.client.EnableSsl = true;
            this.client.Credentials = new NetworkCredential("notificaciones.hsh@gmail.com", "homeswitch123");
            this.client.Timeout = 10000;
        }

        public bool EnviarMailConRecuperacionContrasenia(ClienteViewModel clienteModel)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");
            msg.To.Add(new MailAddress(clienteModel.Email));
            msg.Subject = "HOME SWITCH HOME - MAIL DE RECUPERACION DE CONTRASEÑA";
            msg.Body = string.Format("{0} {1} ", clienteModel.Nombre, clienteModel.Apellido) +
                       "Usted ha solicitado la recuperacion de su contraseña de acceso para el sistema HOME SWITCH HOME. " +
                       string.Format("Usuario:    {0}. ", clienteModel.Usuario) +
                       string.Format("Contraseña: {0}.", clienteModel.Password);
            try
            {
                client.Send(msg);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                msg.Dispose();
            }
        }

        public void EnviarMailGanoSubasta()
        {
            throw new NotImplementedException();
        }

        public void EnviarMailReservaPropiedad()
        {
            throw new NotImplementedException();
        }

        public void EnviarNotificacionNuevaSubasta()
        {
            throw new NotImplementedException();
        }

        public void EnviarNotificacionNuevoHotSale()
        {
            throw new NotImplementedException();
        }
    }
}