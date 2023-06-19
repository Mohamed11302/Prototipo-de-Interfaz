using DatosUsuario;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Boolean MostrarContraseña = false;
        /// false = pwd true = txt
        private Usuario usuario;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txtUsuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                lblEstado.Content = "Se pulso el Enter";
                passContrasena.IsEnabled = true;
                passContrasena.Focus();
                txtContrasena.IsEnabled = true;
            }
        }

        private void contrasena_keydown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            
            string contraseña;
            if (MostrarContraseña)
            {
                contraseña = txtContrasena.Text;
            }
            else
            {
                contraseña = passContrasena.Password.ToString();
            }
            if (ComprobarUsuario(contraseña)==true)
            {
            
            usuario = new Usuario(txtUsuario.Text, contraseña);
            
            //usuario = new Usuario("US1", "C1"); //BORRAR ESTA LINEA TAMBIEN
            Inicio inicio = new Inicio(usuario);
            this.Visibility = Visibility.Hidden;
            inicio.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inicio.Show();
            
            }
                else
                {
                    lblEstado.Content = "Usuario o contraseña incorrectos";
                    passContrasena.BorderBrush = new SolidColorBrush(Colors.Red);
                    txtContrasena.BorderBrush = new SolidColorBrush(Colors.Red);
                    txtUsuario.BorderBrush = new SolidColorBrush(Colors.Red);
                    lbl_ErrorIniciarSesion.Visibility = Visibility.Visible;
                }
            
            
        }

        private void btn_ojo_Click(object sender, RoutedEventArgs e)
        {
            if (MostrarContraseña == false)
            {
                txtContrasena.Text = passContrasena.Password;
                passContrasena.Visibility = Visibility.Hidden; 
                txtContrasena.Visibility = Visibility.Visible;
                MostrarContraseña = true;
                btn_ojo.ToolTip = "Ocultar contraseña";
            }
            else
            {
                passContrasena.Password = txtContrasena.Text;
                txtContrasena.Visibility = Visibility.Hidden;
                passContrasena.Visibility = Visibility.Visible;
                MostrarContraseña=false;
                btn_ojo.ToolTip = "Mostrar contraseña";
            }
        }
        private Boolean ComprobarUsuario(string contraseña)
        {
            // Cargar contenido de prueba
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Usuarios.xml", UriKind.Relative)); doc.Load(fichero.Stream);
            Boolean UsuarioCorrecto = false;
            XmlNodeList nodes = doc.SelectNodes("Usuarios/Usuario");
            foreach (XmlNode node in nodes)
            {
                if (node.ChildNodes[0].InnerText.Equals(txtUsuario.Text) && node.ChildNodes[1].InnerText.Equals(contraseña))
                {
                    UsuarioCorrecto = true;
                    break;
                }

            }
            return UsuarioCorrecto;
        }
    }
}

