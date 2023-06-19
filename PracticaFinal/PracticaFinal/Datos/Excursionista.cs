using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace DatosExcursionista
{
    class Excursionista
    {
        public int id { set; get; }
        public string Nombre { set; get; }

        public string Apellido { set; get; }

        public int Edad { set; get; }
        public ArrayList Telefono = new ArrayList();

        public ArrayList Correo = new ArrayList();
        public Uri Caratula { set; get; }

        public ArrayList Rutas1 = new ArrayList(); //Id ruta
        public ArrayList Rutas2 = new ArrayList(); //Realizada
        public Excursionista(string nombre, string apellido, int edad, Uri caratula)
        {
            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Edad = edad;
            this.Caratula = caratula;
        }
    }

}