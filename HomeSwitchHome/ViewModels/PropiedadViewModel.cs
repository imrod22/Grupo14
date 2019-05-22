using HomeSwitchHome.Services;
using System.Collections.Generic;

namespace HomeSwitchHome.ViewModels
{
    public class PropiedadViewModel
    {
        public int IdPropiedad { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Domicilio { get; set; }

        public string Pais { get; set; }

        public List<RESERVA> Reservas { get; set; }

        public List<ImagenViewModel> Imagenes { get; set; }

        public PropiedadViewModel() { }

        public PropiedadViewModel ToViewModel(PROPIEDAD propiedad) {

            this.IdPropiedad = propiedad.IdPropiedad;
            this.Nombre = propiedad.Nombre;
            this.Descripcion = propiedad.Descripcion;
            this.Domicilio = propiedad.Domicilio;
            this.Pais = propiedad.Pais;
            this.Imagenes = new List<ImagenViewModel>();
            this.Reservas = new List<RESERVA>();

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