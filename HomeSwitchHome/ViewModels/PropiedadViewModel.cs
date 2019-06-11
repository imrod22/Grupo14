using HomeSwitchHome.Services;
using System.Collections.Generic;
using System.Linq;

namespace HomeSwitchHome.ViewModels
{
    public class PropiedadViewModel
    {
        public int IdPropiedad { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public string Pais { get; set; }

        public List<ImagenViewModel> Imagenes { get; set; }

        public PropiedadViewModel() { }

        public PropiedadViewModel ToViewModel(PROPIEDAD propiedad) {

            this.IdPropiedad = propiedad.IdPropiedad;
            this.Nombre = propiedad.Nombre;
            this.Descripcion = propiedad.Descripcion;
            this.Pais = propiedad.Pais;
            this.Imagenes = new List<ImagenViewModel>();

            foreach (var imagen in propiedad.IMAGEN)
            {                
                this.Imagenes.Add(new ImagenViewModel().ToViewModel(imagen));
            }

            if (!this.Imagenes.Any()) {

                var imagenDefault = new ImagenViewModel();
                imagenDefault.path = "/app-content/noimage_residencia.png";

                this.Imagenes.Add(imagenDefault);
            } 


            return this;
        }

    }
}