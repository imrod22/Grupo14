using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HomeSwitchHome.ViewModels;

namespace HomeSwitchHome.Services
{
    public class HotsaleService : IHotsaleService
    {
        private HomeSwitchHomeDB HomeSwitchDB;

        public HotsaleService()
        {
            this.HomeSwitchDB = new HomeSwitchHomeDB();
        }

        public bool BorrarSemanaHotSale(int idHotSale)
        {
            var hotsale = this.HomeSwitchDB.HOTSALE.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();

            if (hotsale != null)
            {
                this.HomeSwitchDB.HOTSALE.Remove(hotsale);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("HotSales");

                return true;
            }

            return false;
        }

        public bool CrearSemanaHotSale(HotSaleViewModel hotSaleModel)
        {
            var hotsales = this.ObtenerHotSalesPropiedad(hotSaleModel.IdPropiedad);

            if(hotsales.Any(t => DateTime.Parse(hotSaleModel.FechaDisponible).CompareTo(Convert.ToDateTime(t.FechaDisponible)) >= 0
                                     && DateTime.Parse(hotSaleModel.FechaDisponible).CompareTo(Convert.ToDateTime(t.FechaDisponible).AddDays(7)) <= 0))

                return false;
            else
            {
                var hotSaleNueva = new HOTSALE();
                hotSaleNueva.IdPropiedad = hotSaleModel.IdPropiedad;
                hotSaleNueva.FechaDisponible = DateTime.Parse(hotSaleModel.FechaDisponible);
                hotSaleNueva.Precio = hotSaleModel.Precio;
                hotSaleNueva.Estado = true;

                this.HomeSwitchDB.HOTSALE.Add(hotSaleNueva);
                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("HotSales");

                return true;
            }
        }

        public bool ModificarSemanaHotSale(int idHotSale, decimal nuevoValor)
        {
            var hotsale = this.HomeSwitchDB.HOTSALE.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();

            if (hotsale != null)
            {
                hotsale.Precio = nuevoValor;

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("HotSales");

                return true;
            }

            return false;
        }

        public List<HotSaleViewModel> ObtenerHotSalesFuturos()
        {
            var hotSales = this.ObtenerHotSales().Where(t => (DateTime.Now <= DateTime.Parse(t.FechaDisponible))
                                                              && t.Estado).OrderBy(t =>  DateTime.Parse(t.FechaDisponible));
            return hotSales.ToList();
        }

        public List<HotSaleViewModel> ObtenerHotSalesHistoricos()
        {
            var hotSales = this.ObtenerHotSales().Where(t => DateTime.Parse(t.FechaDisponible) <= DateTime.Now
                                                             || (DateTime.Now <= DateTime.Parse(t.FechaDisponible))
                                                              && !t.Estado).OrderBy(t => DateTime.Parse(t.FechaDisponible));
            return hotSales.ToList();
        }

        public List<HotSaleViewModel> ObtenerHotSalesPropiedad(int idPropiedad)
        {
            var hotSales = this.ObtenerHotSales();
            return hotSales.Where(t => t.IdPropiedad == idPropiedad).ToList();
        }

        public HotSaleViewModel ObtenerInformacionHotSale(int idHotSale)
        {
            var hotsales = this.ObtenerHotSales();
            return hotsales.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();
        }

        public bool OcuparHotSale(int idHotSale)
        {
            var hotsale = this.HomeSwitchDB.HOTSALE.Where(t => t.IdHotSale == idHotSale).SingleOrDefault();

            if (hotsale != null)
            {
                hotsale.Estado = false;

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("HotSales");

                return true;
            }

            return false;
        }

        public bool LiberarHotSale(DateTime fecha, int idPropiedad)
        {
            var hotsale = this.HomeSwitchDB.HOTSALE.Where(t => t.IdPropiedad == idPropiedad && t.FechaDisponible == fecha).SingleOrDefault();

            if (hotsale != null)
            {
                hotsale.Estado = true;

                this.HomeSwitchDB.SaveChanges();
                CacheHomeSwitchHome.RemoveOnCache("HotSales");

                return true;
            }

            return false;
        }

        private List<HotSaleViewModel> ObtenerHotSales()
        {
            List<HotSaleViewModel> hotSales;

            if (!CacheHomeSwitchHome.ExistOnCache("HotSales"))
            {
                hotSales = new List<HotSaleViewModel>();
                var hotSalesBD = HomeSwitchDB.HOTSALE.ToList();

                foreach (var hotsale in hotSalesBD)
                    hotSales.Add(new HotSaleViewModel().ToViewModel(hotsale));

                CacheHomeSwitchHome.SaveToCache("HotSales", hotSales);
            }

            hotSales = (List<HotSaleViewModel>)CacheHomeSwitchHome.GetFromCache("HotSales");

            return hotSales;
        }
    }
}