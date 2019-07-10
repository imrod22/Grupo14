using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeSwitchHome.ViewModels
{
    public class ClienteViewModel
    {
        public int IdCliente { get; set; }

        public string Usuario { get; set; }

        public string Password { get; set; }

        public bool Login { get; set; }

        public string Premium { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string FechaDeNacimiento { get; set; }

        public string DomicioFiscal { get; set; }

        public string MedioDePago { get; set; }

        public string Banco { get; set; }

        public string CBU { get; set; }

        public int? DNI { get; set; }

        public string Email { get; set; }
        
        public ClienteViewModel() { }

        public ClienteViewModel ToViewModel(CLIENTE cliente)
        {
            this.IdCliente = cliente.IdCliente;
            this.Nombre = cliente.Nombre;
            this.Apellido = cliente.Apellido;
            this.DNI = cliente.DNI;
            this.DomicioFiscal = cliente.DomicioFiscal;
            this.FechaDeNacimiento = Convert.ToString(cliente.FechaDeNacimiento.Date);
            this.CBU = cliente.CBU == 0? string.Empty : Convert.ToString(cliente.CBU);
            this.Banco = cliente.Banco;
            this.MedioDePago = cliente.MedioDePago;
            this.Email = cliente.Email;
            this.Usuario = cliente.USUARIO.Usuario;
            this.Password = cliente.USUARIO.Password;
            this.Login = cliente.USUARIO.Login;

            return this;
        }
    }
}
