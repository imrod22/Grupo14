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

        public List<PujaViewModel> ObtenerPujas(int idSubasta)
        {
            List<PujaViewModel> pujas = new List<PujaViewModel>();

            var pujasDB = this.HomeSwitchDB.PUJA.Where(t => t.IdSubasta == idSubasta).OrderByDescending(t => t.Monto).ToList();

            foreach (var puja in pujasDB)
            {
                pujas.Add(new PujaViewModel().ToViewModel(puja));
            }

            return pujas;
        }

        public void RegistrarPuja(int idSubasta, int idCliente, decimal monto)
        {
            PUJA registroPuja = new PUJA();

            registroPuja.IdCliente = idCliente;
            registroPuja.Monto = monto;
            registroPuja.IdSubasta = idSubasta;

            this.HomeSwitchDB.PUJA.Add(registroPuja);
            this.HomeSwitchDB.SaveChanges();
        }

        public void RemoverMaximaPuja(int idSubasta)
        {
            var pujaRemover = this.HomeSwitchDB.PUJA.Where(t => t.IdSubasta == idSubasta).OrderByDescending(t => t.Monto).FirstOrDefault();
            this.HomeSwitchDB.PUJA.Remove(pujaRemover);
            this.HomeSwitchDB.SaveChanges();

        }
    }
}