using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

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
            msg.Subject = "HOME SWITCH HOME - Mail de recuperación de contraseña";
            msg.Body = string.Format("{0} {1} ", clienteModel.Nombre, clienteModel.Apellido) +
                       "Usted ha solicitado la recuperación de su contraseña de acceso para el sistema HOME SWITCH HOME. " +
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

        public bool EnviarMailGanoSubasta(SubastaViewModel subastaModel)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");
            msg.To.Add(new MailAddress(subastaModel.Cliente.Email));
            msg.Subject = "HOME SWITCH HOME - ¡Ha ganado la subasta!";
            msg.Body = string.Format("Felicitaciones {0}, usted ha resultado vencedor de la subasta de la residencia {1} en el sistema HOME SWITCH HOME.", subastaModel.Cliente.Nombre, subastaModel.Propiedad.Nombre) +
                       string.Format("Su reserva comienza el día {0}. ¡Que la disfrute! Atte. Equipo de Home Switch Home.", subastaModel.FechaReserva);
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

        public void EnviarMailReservaHotSale()
        {
            throw new NotImplementedException();
        }

        public bool EnviarNotificacionNuevaSubasta(List<string> listaMails, SubastaViewModel subastaModel)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");

            foreach (var mail in listaMails)
            {
                msg.To.Add(new MailAddress(mail));
            }
            
            msg.Subject = "HOME SWITCH HOME - ¡Próxima subasta en residencia de su interés!";
            msg.Body = string.Format("Le informamos que a partir del día {0}, y durante las siguientes 72 horas, se llevará a cabo una subasta en la residencia {1}. ", subastaModel.FechaComienzo, subastaModel.Propiedad.Nombre) +
                       string.Format("Con un valor inicial de {0} USD. ¡Esperamos contar con usted! Atte. Equipo de Home Switch Home.", subastaModel.ValorMinimo);
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

        public bool EnviarNotificacionNuevoHotSale(List<ClienteViewModel> listaClientes, SubastaViewModel subastaModel)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");

            foreach (var cliente in listaClientes)
            {
                msg.To.Add(new MailAddress(cliente.Email));
            }

            msg.Subject = "HOME SWITCH HOME - ¡Próximo HOT SALE en residencia de su interés!";
            msg.Body = string.Format("Le informamos que a partir del día {0}, dará comienzo el HOT SALE en la residencia {1}. ", subastaModel.FechaComienzo, subastaModel.Propiedad.Nombre) +
                       string.Format("¡Esperemos contar con usted! Atte. Equipo de Home Switch Home.", subastaModel.FechaReserva);
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

        public bool EnviarUsuarioAceptadoMail(ClienteViewModel cliente)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");
            msg.To.Add(new MailAddress(cliente.Email));

            msg.Subject = "HOME SWITCH HOME - ¡Bienvenido a Home Switch Home!";
            msg.Body = string.Format("De parte de todo el equipo le queremos informar que ya puede acceder al sitio. Esto le va a permitir pujar en nuestras subastas, recibir notificaciones de sus residencias favoritas y muchas cosas más. ") +
                       string.Format("¡Gracias nuevamente {0}! Atte. Equipo de Home Switch Home.", cliente.Nombre);
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

        public bool EnviarPremiumAceptadoMail(ClienteViewModel cliente)
        {
            MailMessage msg = new MailMessage();

            msg.From = new MailAddress("notificaciones.hsh@gmail.com");
            msg.To.Add(new MailAddress(cliente.Email));
            
            msg.Subject = "HOME SWITCH HOME - ¡Ahora ya es PREMIUM!";
            msg.Body = string.Format("Hola {0}, ante todo queremos felicitarlo porque ya forma parte del selecto grupo de usuarios que descansan dónde y cuándo quieren, ¡sólo haciendo un click!.", cliente.Nombre) +
                       string.Format("¡Gracias {0} por formar parte! Atte. Equipo de Home Switch Home.", cliente.Nombre);
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
    }
}