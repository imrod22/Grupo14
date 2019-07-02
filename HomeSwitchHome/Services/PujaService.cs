using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSwitchHome.Services
{
    public class PujaService : IPujaService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public PujaService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public List<PUJA> ObtenerUltimaPuja()
        {
            throw new NotImplementedException();
        }

        public void RegistrarPuja(int idSubasta, int idCliente, decimal monto)
        {
            PUJA registroPuja = new PUJA();

            registroPuja.IdCliente = idCliente;
            registroPuja.Monto = monto;
            registroPuja.IdSubasta = idSubasta;

            this.HomeSwitchDB.PUJA.Add(registroPuja);
        }
    }
}