using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var usuarioActual = this.HomeSwitchDB.USUARIO.Where(t => t.Usuario == usuario && t.Password == password).FirstOrDefault();
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

        public PREMIUM ObtenerSolicitudPremium(int IdCliente)
        {
            return this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).SingleOrDefault();
        }


        public bool EsAdmin(int IdUsuario)
        {
            return this.HomeSwitchDB.ADMINISTRADOR.Any(t => t.IdUsuario == IdUsuario);
        }

        public string RegistrarNuevoCliente(ClienteViewModel nuevoCliente)
        {
            if (this.ExisteCliente(nuevoCliente))
                return string.Format("Ya existe un usuario registrado con la cuenta de correo '{0}'.", nuevoCliente.Email);


            if (this.CrearUsuario(nuevoCliente.Usuario, nuevoCliente.Password))
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

                return string.Format("OK");
            }

            return string.Format("El nombre de usuario ingresado: '{0}' no está disponible.", nuevoCliente.Usuario);

        }

        public string RegistrarComoPremium(int IdCliente)
        {
            var tieneSolicitudPremium =  this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).Any();

            if (!tieneSolicitudPremium)
            {
                var nuevoPremium = new PREMIUM();
                nuevoPremium.IdCliente = IdCliente;
                nuevoPremium.Aceptado = "NO";

                this.HomeSwitchDB.PREMIUM.Add(nuevoPremium);
                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return string.Format("OK");
            }
            
            return string.Format("No se pudo obtener la información del usuario seleccionado. Ha ocurrido un error en el servidor.");

        }

        public string ConfirmarNuevoCliente(int idCliente)
        {
            var nuevoCliente = this.HomeSwitchDB.CLIENTE.SingleOrDefault(t => t.IdCliente == idCliente);

            if (nuevoCliente != null)
            {
                var nuevoUsuario = this.HomeSwitchDB.USUARIO.SingleOrDefault(t => t.IdUsuario == nuevoCliente.USUARIO.IdUsuario);
                nuevoUsuario.Login = true;

                this.HomeSwitchDB.SaveChanges();

                CacheHomeSwitchHome.RemoveOnCache("Clientes");
                return string.Format("OK");
            }

            else return string.Format("No se pudo obtener la información del usuario seleccionado. Ha ocurrido un error en el servidor.");
        }

        public string ConfirmarPremium(int IdCliente)
        {
            var premiumAceptado = this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).FirstOrDefault();

            if (premiumAceptado != null)
            {
                premiumAceptado.Aceptado = "SI";
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return string.Format("OK");
            }
            else
                return string.Format("No se pudo obtener la información del usuario seleccionado. Ha ocurrido un error en el servidor.");
        }

        public string RechazarSolicitudNuevoCliente(int rechazado)
        {
            var solicitudNuevoCliente = this.HomeSwitchDB.CLIENTE.Where(t => t.IdCliente == rechazado).FirstOrDefault();

            if (solicitudNuevoCliente != null)
            {
                var solicitudNuevoUsuario = this.HomeSwitchDB.USUARIO.Where(t => t.Usuario == solicitudNuevoCliente.USUARIO.Usuario).FirstOrDefault(); ; 

                this.HomeSwitchDB.CLIENTE.Remove(solicitudNuevoCliente);
                this.HomeSwitchDB.USUARIO.Remove(solicitudNuevoUsuario);

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return string.Format("OK");
            }
            else
                return string.Format("No se pudo obtener la información del usuario seleccionado. Ha ocurrido un error en el servidor.");
        }

        public string RechazarSolicitudPremium(int rechazado)
        {
            var premiumRechazado = this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == rechazado).FirstOrDefault();

            if (premiumRechazado != null)
            {
                this.HomeSwitchDB.PREMIUM.Remove(premiumRechazado);

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return string.Format("OK");
            }
            else
                return string.Format("No se pudo obtener la información del usuario seleccionado. Ha ocurrido un error en el servidor.");
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

        public bool ActualizarContrasenia(int idCliente, string nuevaPass)
        {
            var cliente = this.HomeSwitchDB.CLIENTE.Where(t => t.IdCliente == idCliente).SingleOrDefault();

            if (cliente != null)
            {
                var usuario = this.HomeSwitchDB.USUARIO.Where(t => t.IdUsuario == cliente.IdUsuario).SingleOrDefault();

                usuario.Password = nuevaPass;

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("Clientes");

                return true;
            }

            return false;            
        }

        public ClienteViewModel ObtenerInformacionDeUsuario(string usuario)
        {
            var cliente = this.ObtenerListaDeClientes().Where(t => t.Usuario == usuario);
            return cliente.FirstOrDefault();
        }

        public bool ExisteUsuario(string usuario)
        {
            var cliente = this.ObtenerListaDeClientes().Where(t => t.Usuario == usuario);
            return cliente.Any();
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