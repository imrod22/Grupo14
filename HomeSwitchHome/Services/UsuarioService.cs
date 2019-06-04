﻿using HomeSwitchHome.ViewModels;
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

        public bool RegistrarNuevoCliente(ClienteViewModel nuevoCliente)
        {   
            if(!this.ExisteCliente(nuevoCliente) && this.CrearUsuario(nuevoCliente.Usuario, nuevoCliente.Password))
            {                               
                var clienteACrear = new CLIENTE();

                clienteACrear.Nombre = nuevoCliente.Nombre;
                clienteACrear.Apellido = nuevoCliente.Apellido;

                clienteACrear.IdUsuario = this.HomeSwitchDB.USUARIO.Where(t => t.Usuario == nuevoCliente.Usuario).Select(t => t.IdUsuario).FirstOrDefault();

                clienteACrear.Banco = nuevoCliente.Banco;
                clienteACrear.CBU = nuevoCliente.CBU;
                clienteACrear.DomicioFiscal = nuevoCliente.DomicioFiscal;
                clienteACrear.FechaDeNacimiento = nuevoCliente.FechaDeNacimiento;
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

        public PREMIUM ObtenerInformacionPremium(int IdCliente) {

            var premiumActual = this.HomeSwitchDB.PREMIUM.Where(t => t.IdCliente == IdCliente).FirstOrDefault();
            return premiumActual;
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
                    Password = contrasenia
                };

                this.HomeSwitchDB.USUARIO.Add(nuevoUsuario);
                this.HomeSwitchDB.SaveChanges();

                return true;
            }
            else return false;            
        }

    }
}