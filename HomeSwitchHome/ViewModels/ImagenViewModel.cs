namespace HomeSwitchHome.Services
{
    public class ImagenViewModel
    {
        public int idImagen;

        public int idPropiedad;

        public string path;

        public string Nombre;

        public ImagenViewModel() { }

        public ImagenViewModel ToViewModel(IMAGEN imagen)
        {
            this.idImagen = imagen.IdImagen;
            this.idPropiedad = imagen.IdPropiedad;
            this.Nombre = imagen.Nombre;
            this.path = imagen.Path;

            return this;
        }
    }
}