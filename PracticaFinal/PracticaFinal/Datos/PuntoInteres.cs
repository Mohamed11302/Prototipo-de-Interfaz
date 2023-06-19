using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosPuntosInteres
{
    class PuntoInteres
    {
        public int Id { set; get; }
        public string Nombre { set; get; }
        public string Descripcion { set; get; }
        public string Tipologia { set; get; }
        public List<Uri> Imagenes = new List<Uri>();

        public int Ruta { set; get; }
        public PuntoInteres()
        {

        }
    }
}
