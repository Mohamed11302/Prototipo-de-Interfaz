using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace DatosGuia
{
    public class Guia
    {
        public int id { set; get; }
        public string Nombre { set; get; }

        public string Apellido { set; get; }

        public int Edad { set; get; }
        public ArrayList Telefono = new ArrayList();

        public ArrayList Correo = new ArrayList();
        public ArrayList idiomas = new ArrayList();
        public Uri Caratula { set; get; }

        public ArrayList Rutas1 = new ArrayList();
        public ArrayList Rutas2 = new ArrayList();

        public ArrayList lunes = new ArrayList();
        public ArrayList martes = new ArrayList();
        public ArrayList miercoles = new ArrayList();
        public ArrayList jueves = new ArrayList();
        public ArrayList viernes = new ArrayList();
        public ArrayList sabado = new ArrayList();
        public ArrayList domingo = new ArrayList();
        public Guia()
        {
           
        }
    }

}
