namespace HomeSwitchHome.Services
{
    public class ImagenViewModel
    {
        public int idImagen;

        public int idPropiedad;

        public string path;        

        public ImagenViewModel() { }

        public ImagenViewModel ToViewModel(IMAGEN imagen)
        {
            this.idImagen = imagen.IdImagen;
            this.idPropiedad = imagen.IdPropiedad;
            this.path = imagen.Path;

            return this;
        }
    }
}