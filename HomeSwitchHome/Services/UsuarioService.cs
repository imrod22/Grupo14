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
            return this.HomeSwitchDB.PREMIUM.Any(t => t.IdCliente == IdCliente);
        }

        public bool EsAdmin(int IdUsuario)
        {
            return this.HomeSwitchDB.ADMINISTRADOR.Any(t => t.IdUsuario == IdUsuario);
        }

        public void RegistrarNuevoCliente(ClienteViewModel nuevoCliente)
        {
            var clienteACrear = new CLIENTE();

            clienteACrear.Nombre = nuevoCliente.Nombre;
            clienteACrear.Apellido = nuevoCliente.Apellido;
            clienteACrear.Banco = nuevoCliente.Banco;
            clienteACrear.CBU = nuevoCliente.CBU;
            clienteACrear.DomicioFiscal = nuevoCliente.DomicioFiscal;
            clienteACrear.FechaDeNacimiento = nuevoCliente.FechaDeNacimiento;
            clienteACrear.MedioDePago = nuevoCliente.MedioDePago;
            clienteACrear.DNI = nuevoCliente.DNI;

            this.HomeSwitchDB.CLIENTE.Add(clienteACrear);
            this.HomeSwitchDB.SaveChanges();

            CacheHomeSwitchHome.RemoveOnCache("Clientes");

        }

        public void RegistrarComoPremium(int IdCliente)
        {
            var nuevoPremium = new PREMIUM();
            nuevoPremium.IdCliente = IdCliente;

            this.HomeSwitchDB.PREMIUM.Add(nuevoPremium);
            this.HomeSwitchDB.SaveChanges();
        }

        public List<ClienteViewModel> ObtenerListaDeClientes()
        {
            List<ClienteViewModel> clientes;

            if (!CacheHomeSwitchHome.ExistOnCache("Clientes"))
            {
                clientes = new List<ClienteViewModel>();

                foreach (var cliente in this.HomeSwitchDB.CLIENTE.ToList())
                {
                    clientes.Add(new ClienteViewModel().ToViewModel(cliente));
                }

                CacheHomeSwitchHome.SaveToCache("Clientes", clientes);
            }
            
            clientes = (List<ClienteViewModel>)CacheHomeSwitchHome.GetFromCache("Clientes");

            return clientes;
        }

    }
}