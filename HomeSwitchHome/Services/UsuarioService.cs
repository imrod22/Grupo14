using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.Services
{
    public class UsuarioService : IUsuarioService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public UsuarioService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public USUARIO ObtenerUsuarioRegistrado(string usuario, string password)
        {
            var usuarioActual = this.HomeSwitchDB.USUARIO.Where(t => t.Usuario == usuario && t.Password == password && t.Login).FirstOrDefault();
            return usuarioActual; 
        }

        public ClienteViewModel ObtenerInformacionCliente(int IdUsuario)
        {
            var clienteActual = this.HomeSwitchDB.CLIENTE.Where(t => t.IdUsuario == IdUsuario).FirstOrDefault();
            return (clienteActual != null) ? new ClienteViewModel().ToViewModel(clienteActual) : null;
        }

        public bool EsUsuarioPremium(int IdCliente)
        {
            return this.HomeSwitchDB.PREMIUM.Any(t => t.IdCliente == IdCliente && t.Aceptado == "SI");
        }

        public bool EsAdmin(int IdUsuario)
        {
            return this.HomeSwitchDB.ADMINISTRADOR.Any(t => t.IdUsuario == IdUsuario);
        }

        public bool RegistrarNuevoCliente(ClienteViewModel nuevoCliente)
        {
            if (!this.ExisteCliente(nuevoCliente) && this.CrearUsuario(nuevoCliente.Usuario, nuevoCliente.Password))
            {
                var clienteACrear = new CLIENTE();

                clienteACrear.Nombre = nuevoCliente.Nombre;
                clienteACrear.Apellido = nuevoCliente.Apellido;

                clienteACrear.IdUsuario = this.HomeSwitchDB.USUARIO.Where(t => t.Usuario == nuevoCliente.Usuario).Select(t => t.IdUsuario).FirstOrDefault();

                var esCBUNumerico = int.TryParse(nuevoCliente.CBU, out int cbuEntero);

                clienteACrear.Banco = nuevoCliente.Banco;
                clienteACrear.CBU = esCBUNumerico ? cbuEntero : 0;
                clienteACrear.DomicioFiscal = nuevoCliente.DomicioFiscal;
                clienteACrear.FechaDeNacimiento = Convert.ToDateTime(nuevoCliente.FechaDeNacimiento);
                clienteACrear.MedioDePago = nuevoCliente.MedioDePago;
                clienteACrear.DNI = nuevoCliente.DNI;
                clienteACrear.Email = nuevoCliente.Email;

                this.HomeSwitchDB.CLIENTE.Add(clienteACrear);
                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return true;
            }

            return false;

        }

        public bool RegistrarComoPremium(int IdCliente)
        {
            var existePremium =  this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).Any();

            if (!existePremium) {
                var nuevoPremium = new PREMIUM();
                nuevoPremium.IdCliente = IdCliente;
                nuevoPremium.Aceptado = "NO";

                this.HomeSwitchDB.PREMIUM.Add(nuevoPremium);
                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return true;
            }
            
            return false;

        }

        public bool ConfirmarNuevoCliente(int idCliente)
        {
            var nuevoCliente = this.HomeSwitchDB.CLIENTE.SingleOrDefault(t => t.IdCliente == idCliente);

            if (nuevoCliente != null)
            {
                var nuevoUsuario = this.HomeSwitchDB.USUARIO.SingleOrDefault(t => t.IdUsuario == nuevoCliente.USUARIO.IdUsuario);
                nuevoUsuario.Login = true;

                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Clientes");
                return true;
            }

            else return false;
        }

        public bool ConfirmarPremium(int IdCliente)
        {
            var premiumAceptado = this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).FirstOrDefault();
            premiumAceptado.Aceptado = "SI";
            this.HomeSwitchDB.SaveChanges();
            CacheHomeSwitchHome.RemoveOnCache("Clientes");

            return true;
        }

        private List<ClienteViewModel> ObtenerListaDeClientes()
        {
            List<ClienteViewModel> clientes;

            if (!CacheHomeSwitchHome.ExistOnCache("Clientes"))
            {
                clientes = new List<ClienteViewModel>();

                foreach (var cliente in this.HomeSwitchDB.CLIENTE.ToList())
                {
                    var clienteActual = new ClienteViewModel().ToViewModel(cliente);

                    clientes.Add(clienteActual);
                }

                CacheHomeSwitchHome.SaveToCache("Clientes", clientes);
            }
            
            clientes = (List<ClienteViewModel>)CacheHomeSwitchHome.GetFromCache("Clientes");

            return clientes;
        }

        public PREMIUM ObtenerInformacionPremium(int IdCliente) {

            var premiumActual = this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).SingleOrDefault();
            return premiumActual;
        }

        public List<ClienteViewModel> ObtenerNuevosClientes()
        {
            var clientes = this.ObtenerListaDeClientes();

            return clientes.Where(t => !t.Login).ToList();
        }

        public List<ClienteViewModel> ObtenerSolicitudesPremium()
        {
            var clientes = this.ObtenerListaDeClientes();

            foreach (var cliente in clientes)
            {
                var premium = this.ObtenerInformacionPremium(cliente.IdCliente);

                cliente.Premium = premium == null ? string.Empty : premium.Aceptado;
            }

            return clientes.Where(t => t.Premium == "NO").ToList();
        }
        
        private bool ExisteCliente(ClienteViewModel clienteACrear)
        {
            return this.HomeSwitchDB.CLIENTE.Any(t => t.Email == clienteACrear.Email);
        }

        private bool CrearUsuario(string usuario, string contrasenia)
        {

            if (!this.HomeSwitchDB.USUARIO.Any(t => t.Usuario == usuario))
            {
                var nuevoUsuario = new USUARIO()
                {
                    Usuario = usuario,
                    Password = contrasenia,
                    Login = false
                };

                this.HomeSwitchDB.USUARIO.Add(nuevoUsuario);
                this.HomeSwitchDB.SaveChanges();

                return true;
            }
            else return false;            
        }           

    }
}