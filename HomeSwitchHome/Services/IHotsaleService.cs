using HomeSwitchHome.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeSwitchHome.Services
{
    public interface IHotsaleService
    {
        bool CrearSemanaHotSale(HotSaleViewModel hotSaleModel);

        bool BorrarSemanaHotSale(int idHotSale);

        bool ModificarSemanaHotSale(int idHotSale, decimal nuevoValor);

        List<HotSaleViewModel> ObtenerHotSalesHistoricos();

        List<HotSaleViewModel> ObtenerHotSalesFuturos();

        bool OcuparHotSale(int idHotSale);

        bool LiberarHotSale(DateTime fecha, int idPropiedad);
    }
}
