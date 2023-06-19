using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatosUsuario
{
    public class Usuario
    {
        public int id { set; get; }
        public string usuario { set; get; }
        public string contraseña { set; get; }
        public string UltimoAcceso { set; get; }
        public string nombre { set; get; }
        public string apellidos { set; get; }
        public string FechaNacimiento { set; get; }
        public int edad { set; get; }
        public ArrayList Correos  = new ArrayList();
        public ArrayList telefono = new ArrayList();

        public Uri FotoPerfil { set; get; }

        public Usuario(string usuario, string contraseña)
        {
            this.usuario = usuario;
            this.contraseña = contraseña;
        }
        public Usuario(string usuario, string contraseña, string nombre, string apellidos, int edad, Uri fotoPerfil)
        {
            this.usuario = usuario;
            this.contraseña = contraseña;
            this.nombre = nombre;
            this.apellidos = apellidos;
            this.edad = edad;
            this.FotoPerfil =fotoPerfil;
        }
    }
}
