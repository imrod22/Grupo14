using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Services
{
    public class CreditoService : ICreditoService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public CreditoService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }


        public bool DescontarCreditoCliente(int clienteId, int anio)
        {
            var creditoAActualizar =  this.HomeSwitchDB.CREDITO_CLIENTE.Where(t => t.IdCliente == clienteId && t.Anio == anio).SingleOrDefault();
            creditoAActualizar.Credito-= 1;
            this.HomeSwitchDB.SaveChanges();
            return true;
        }

        public bool DevolverCreditoCliente(int clienteId, int anio)
        {
            var creditoAActualizar = this.HomeSwitchDB.CREDITO_CLIENTE.Where(t => t.IdCliente == clienteId && t.Anio == anio).SingleOrDefault();
            creditoAActualizar.Credito+= 1;
            this.HomeSwitchDB.SaveChanges();
            return true;
        }

        public bool GenerarCreditosCliente(ClienteViewModel cliente)
        {
            for (int i = 0; i < 3; i++)
            {
                CREDITO_CLIENTE nuevoCredito = new CREDITO_CLIENTE();
                nuevoCredito.IdCliente = cliente.IdCliente;
                nuevoCredito.Credito = 2;
                nuevoCredito.Anio = DateTime.Now.Year + i;

                this.HomeSwitchDB.CREDITO_CLIENTE.Add(nuevoCredito);
            }

            this.HomeSwitchDB.SaveChanges();
            return true;
        }

        public ClienteCreditoViewModel ObtenerCreditosAnio(int anio, int clienteId)
        {
            var credito = this.HomeSwitchDB.CREDITO_CLIENTE.Where(t => t.Anio == anio && t.IdCliente == clienteId).FirstOrDefault();
            
            return new ClienteCreditoViewModel().ToViewModel(credito);
        }
    }
}