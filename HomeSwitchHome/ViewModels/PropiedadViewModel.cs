using HomeSwitchHome.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeSwitchHome.ViewModels
{
    public class PropiedadViewModel
    {
        public int idPropiedad;

        public string Nombre;

        public string Descripcion;

        public string Domicilio;

        public string Pais;

        public List<RESERVA> Reservas = new List<RESERVA>();

        public List<ImagenViewModel> Imagenes = new List<ImagenViewModel>();

        public PropiedadViewModel() { }

        public PropiedadViewModel ToViewModel(PROPIEDAD propiedad) {

            this.idPropiedad = propiedad.IdPropiedad;
            this.Nombre = propiedad.Nombre;
            this.Descripcion = propiedad.Descripcion;
            this.Domicilio = propiedad.Domicilio;
            this.Pais = propiedad.Pais;

            foreach (var reserva in propiedad.RESERVA)
            {
                this.Reservas.Add(reserva);
            }

            foreach (var imagen in propiedad.IMAGEN)
            {
                this.Imagenes.Add(new ImagenViewModel().ToViewModel(imagen));
            }

            return this;
        }

    }
}