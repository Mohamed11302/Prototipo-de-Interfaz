using PracticaFinal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatosUsuario;
using DatosExcursionista;
using System.Xml;
using DatosRutas;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Inicio.xaml
   /// </summary>

    public partial class Inicio : Window
    {
        public Usuario usuario;
        public Inicio(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            if (this.usuario.UltimoAcceso == null)
            {
                this.usuario = CargarUsuarioXML();
            }
            Inicializar();
        }
        public void Inicializar()
        {
            lbl_Usuario.Content = usuario.usuario.ToString();
            txt_nombre.Text = usuario.nombre.ToString();
            txt_apellidos.Text = usuario.apellidos.ToString();
            txt_acceso.Text = usuario.UltimoAcceso.ToString();
            txt_nacimiento.Text = usuario.FechaNacimiento.ToString();
            txt_edad.Text = usuario.edad.ToString();
            cb_correos.ItemsSource = usuario.Correos;
            cb_telefonos.ItemsSource = usuario.telefono;
            imgUsuario.Source = new BitmapImage(usuario.FotoPerfil);
        }
        private void btn_Inicio_Click(object sender, RoutedEventArgs e)
        {
        }

        private void btn_rutas_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guias_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Guias ventanaGuias = new Ventana_Guias(usuario);
            this.Visibility = Visibility.Hidden;
            ventanaGuias.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventanaGuias.Show();
        }

        private void btn_excursionistas_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Excursionistas ventanaExcursionistas = new Ventana_Excursionistas(usuario);
            this.Visibility = Visibility.Hidden;
            ventanaExcursionistas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventanaExcursionistas.Show();
        }

        private void btn_configuracion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_cerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        private Usuario CargarUsuarioXML()
        {
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Usuarios.xml", UriKind.Relative)); 
            doc.Load(fichero.Stream);
            var nuevousuario = new Usuario("", "", "", "", 0, null);
            XmlNodeList nodes = doc.SelectNodes("Usuarios/Usuario");
            foreach (XmlNode node in nodes)
            {
                nuevousuario.id = Convert.ToInt32(node.Attributes[0].Value);
                nuevousuario.usuario = node.ChildNodes[0].InnerText;
                nuevousuario.contraseña = node.ChildNodes[1].InnerText;
                if (nuevousuario.usuario.Equals(usuario.usuario) == false || nuevousuario.contraseña.Equals(usuario.contraseña)== false)
                {
                    break;
                }
                nuevousuario.nombre = node.ChildNodes[2].InnerText;
                nuevousuario.apellidos = node.ChildNodes[3].InnerText;
                nuevousuario.UltimoAcceso = node.ChildNodes[4].InnerText;
                nuevousuario.FechaNacimiento = node.ChildNodes[5].InnerText;
                nuevousuario.edad = (Convert.ToInt32(node.ChildNodes[6].InnerText));
                //nuevousuario.Correos = node.ChildNodes[7].InnerText;
                //nuevousuario.telefono = (Convert.ToInt32(node.ChildNodes[8].InnerText));
                string[] correos = node.ChildNodes[7].InnerText.Split(',');
                for (int i = 0; i < correos.Length; i++)
                {
                    nuevousuario.Correos.Add(correos[i]);
                }
                string[] telefonos = node.ChildNodes[8].InnerText.Split(',');
                for (int i = 0; i < telefonos.Length; i++)
                {
                    nuevousuario.telefono.Add(Convert.ToInt32(telefonos[i]));
                }
                //cb_correos.Items.Add(node.ChildNodes[7].InnerText);
                //cb_telefonos.Items.Add(node.ChildNodes[8].InnerText);
                nuevousuario.FotoPerfil = (new Uri(node.ChildNodes[9].InnerText, UriKind.Relative));
                

            }
            return nuevousuario;
        }

        private void btn_configuracion_Click_1(object sender, RoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion(usuario);
            configuracion.Show();
            this.Close();
        }

        private void btn_cerrarsesion_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
            this.Close();
        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayuda = new Ayuda(usuario);
            ayuda.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ayuda.Show();
            this.Close();
        }

        private void btn_rutas_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Rutas ventana_rutas = new Ventana_Rutas(usuario);
            ventana_rutas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_rutas.Show();
            this.Close();
        }
    }
}
