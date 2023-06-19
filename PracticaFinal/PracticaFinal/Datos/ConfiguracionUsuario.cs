using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosConfiguracionUsuario
{
    public class ConfiguracionUsuario
    {
        public string correorecuperacion;
        public ArrayList notificaciones = new ArrayList();
        public int tema;
        public int tamLetra;
        public string idioma;
        public ConfiguracionUsuario(string correorecuperacion, int tema, int tamLetra, string idioma)
        {
            this.correorecuperacion = correorecuperacion;
            this.tema = tema;
            this.tamLetra = tamLetra;
            this.idioma = idioma;
        }
    }
}
