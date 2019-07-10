using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSwitchHome.ViewModels
{
    public class NovedadViewModel
    {
        public int IdNovedadPropiedad { get; set; }

        public int ClienteId { get; set; }

        public int PropiedadId { get; set; }

        public ClienteViewModel Cliente { get; set; }

        public PropiedadViewModel Propiedad { get; set; }

        public NovedadViewModel() { }

        public NovedadViewModel ToViewModel(NOVEDAD_PROPIEDAD novedad)
        {
            this.Cliente = new ClienteViewModel().ToViewModel(novedad.CLIENTE);
            this.Propiedad = new PropiedadViewModel().ToViewModel(novedad.PROPIEDAD);

            return this;
        }
    }
}