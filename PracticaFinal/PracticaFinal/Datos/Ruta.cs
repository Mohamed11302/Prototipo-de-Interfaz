using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosRutas
{
    class Ruta
    {
        public int id {set;get;}
        public String Nombre { set; get; }
        public String Origen { set; get; }
        public String Destino { set; get; }
        public int Distancia { set; get; }
        public int Altitud { set; get; }
        public String Guia { set; get; }
        public int maxParticipantes { set; get; }
        public DateTime fecha { set; get; }
        public String hora { set; get; }
        public int duracion { set; get; }

        public String dificultad { set; get; }
        public Uri foto { set; get; }
        public Boolean Seleccionada { set; get; }
        public Boolean Realizada { set; get; }
        public Ruta(int id, string nombre, string origen, string destino, int distancia, int altitud, string guia, int maxParticipantes, string hora, int duracion, Uri foto)
        {
            this.id = id;
            this.Nombre = nombre;
            this.Origen = origen;
            this.Destino = destino;
            this.Distancia = distancia;
            this.Altitud = altitud;
            this.Guia = guia;
            this.maxParticipantes = maxParticipantes;
            this.hora = hora;
            this.duracion = duracion;
            this.foto = foto;
        }
        public Ruta()
        {

        }

    }
}