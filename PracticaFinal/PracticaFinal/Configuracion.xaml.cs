using DatosUsuario;
using System;
using System.Collections;
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
using DatosConfiguracionUsuario;
using System.Xml;
using System.Media;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Configuración.xaml
    /// </summary>
    public partial class Configuracion : Window
    {
        public Usuario usuario;
        public ConfiguracionUsuario configuracionusuario;
        public Boolean TemaClaro = true;
        public Configuracion(Usuario usuario)
        {
            InitializeComponent();
            configuracionusuario = CargarConfiguracionUsuarioXML(usuario.id);
            this.usuario = usuario;
            this.usuario.FechaNacimiento = usuario.FechaNacimiento;
            Inicializar();

        }
        private ConfiguracionUsuario CargarConfiguracionUsuarioXML(int id)
        {
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/ConfiguracionUsuario.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            var nuevaconfiguracion = new ConfiguracionUsuario("", 0, 0, "");
            XmlNodeList nodes = doc.SelectNodes("Configuraciones/Configuracion");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes[0].InnerText == id.ToString())
                {
                    nuevaconfiguracion.correorecuperacion = node.ChildNodes[0].InnerText;
                    string[] notificaciones = node.ChildNodes[1].InnerText.Split(',');
                    for (int i = 0; i < notificaciones.Length; i++)
                    {
                        nuevaconfiguracion.notificaciones.Add(Convert.ToBoolean(notificaciones[i]));
                    }
                    nuevaconfiguracion.tema = Convert.ToInt32(node.ChildNodes[2].Value);
                    nuevaconfiguracion.tamLetra = Convert.ToInt32(node.ChildNodes[3].Value);
                    nuevaconfiguracion.idioma = node.ChildNodes[4].Value;
                }
            }
            return nuevaconfiguracion;
        }

        private void Tabconfiguracion_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (tabConfiguracion.SelectedIndex != -1)
            {
                TabItem tab = (TabItem)tabConfiguracion.SelectedItem;
                lbl_Pestana.Content = tab.Name.ToString();
            }
        }
        private void CerrarConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio(usuario);
            inicio.Show();
            this.Close();
        }
        private void Inicializar()
        {
            txt_nombreusuario.Text = usuario.usuario.ToString();
            txt_nombre.Text = usuario.nombre.ToString();
            txt_apellidos.Text = usuario.apellidos.ToString();
            txt_nacimiento.Text = usuario.FechaNacimiento.ToString();
            txt_edad.Text = usuario.edad.ToString();
            cb_correos.ItemsSource = usuario.Correos;
            cb_telefonos.ItemsSource = usuario.telefono;
            ImgUsuario.Source = new BitmapImage(usuario.FotoPerfil);

            txt_correoRecuperacion.Text = configuracionusuario.correorecuperacion.ToString();
            if (configuracionusuario.tema == 1)
            {
                Img_switchoff.Visibility = Visibility.Visible;
                Img_switchon.Visibility = Visibility.Hidden;
                lbl_Tema_Switch.Content = "Claro";
                TemaClaro = true;
            }
            else
            {
                Img_switchoff.Visibility = Visibility.Hidden;
                Img_switchon.Visibility = Visibility.Visible;
                lbl_Tema_Switch.Content = "Oscuro";
                TemaClaro = false;
            }
            Slider_tamaño.Value = configuracionusuario.tamLetra;
            Notificaciones1.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[0]);
            if (Notificaciones1.IsChecked == false)
            {
                Notificaciones2.IsEnabled = false;
                Notificaciones3.IsEnabled = false;
                Notificaciones4.IsEnabled = false;
                Notificaciones5.IsEnabled = false;
                Notificaciones6.IsEnabled = false;
                Notificaciones7.IsEnabled = false;

            }
            Notificaciones2.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[1]);
            Notificaciones3.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[2]);
            Notificaciones4.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[3]);
            Notificaciones5.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[4]);
            Notificaciones6.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[5]);
            Notificaciones7.IsChecked = Convert.ToBoolean(configuracionusuario.notificaciones[6]);
            if (configuracionusuario.idioma == "Castellano")
            {
                cb_idioma.SelectedIndex = 0;
            }
            else
            {
                cb_idioma.SelectedIndex = 1;
            }

        }
        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage imagen = CargarImagen(ImgUsuario);
            ImgUsuario.Source = imagen;
            //Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
            //ex.Caratula = imagen;
        }
        private BitmapImage CargarImagen(Image img)
        {
            var nuevaImagen = new Microsoft.Win32.OpenFileDialog();
            nuevaImagen.FileName = ""; // Default file name
            nuevaImagen.DefaultExt = ".jpg"; // Default file extension
            //nuevaImagen.Filter = "Image (.jpg)|*.jpg"; // Filter files by extension
            BitmapImage imageBitmap = null;
            bool? result = nuevaImagen.ShowDialog();

            if (result == true)
            {
                // Open document
                string filename = nuevaImagen.FileName;
                Uri imageUri = new Uri(filename, UriKind.Absolute);
                imageBitmap = new BitmapImage(imageUri);
                Image myImage = new Image();
                myImage.Source = imageBitmap;
                img.Source = imageBitmap;
            }
            return imageBitmap;
        }

        private void bt_guardar_Click(object sender, RoutedEventArgs e)
        {
            Boolean campos = Comprobar5Campos(txt_nombreusuario, lbl_Error_nombreusuario, txt_nombre, lbl_Error_nombre, txt_apellidos, lbl_Error_apellidos, txt_nacimiento, lbl_Error_fechanacimiento, txt_edad, lbl_Error_edad);
            Boolean nuevacontraseña = ComprobarNuevaContraseña();
            Boolean cambiarnuevacontraseña = false;
            if (nuevacontraseña && passAntiguaContraseña.Password.ToString() != String.Empty && passNuevaContraseña.Password.ToString() != String.Empty && passConfirmarContraseña.Password.ToString() != String.Empty)
            {
                cambiarnuevacontraseña = true;
            }
            if (nuevacontraseña == false)
            {
                tabConfiguracion.SelectedIndex = 3;
            }
            if (campos == false)
            {
                tabConfiguracion.SelectedIndex = 0;
            }
            if (campos && nuevacontraseña)
            {
                if (cambiarnuevacontraseña == true)
                {
                    this.usuario.contraseña = passConfirmarContraseña.Password.ToString();
                }
                this.usuario.usuario = txt_nombreusuario.Text;
                this.usuario.nombre = txt_nombre.Text;
                this.usuario.apellidos = txt_apellidos.Text;
                this.usuario.FechaNacimiento = txt_nacimiento.Text;
                this.usuario.edad = Convert.ToInt32(txt_edad.Text);
                this.usuario.Correos = (ArrayList) cb_correos.ItemsSource;
                this.usuario.telefono = (ArrayList) cb_telefonos.ItemsSource;
                this.usuario.FotoPerfil = new Uri(ImgUsuario.Source.ToString());
                MessageBox.Show("Datos guardados con exito", "Guardar datos",MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private Boolean Comprobar5Campos(TextBox usuario, Label lbl_usuario, TextBox nombre, Label lbl_nombre, TextBox apellido, Label lbl_apellido, TextBox fechanacimiento, Label lbl_fechanacimiento, TextBox edad, Label lbl_edad )
        {
            Boolean comprobar = true;
            Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
            if (nombre.Text == String.Empty)
            {
                nombre.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_nombre.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                lbl_nombre.Visibility = Visibility.Hidden;
                nombre.BorderBrush = new SolidColorBrush(color_default);
            }
            if (apellido.Text == String.Empty)
            {
                apellido.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_apellido.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                lbl_apellido.Visibility = Visibility.Hidden;
                apellido.BorderBrush = new SolidColorBrush(color_default);
            }
            if (ComprobarEnteroEdad(edad) == false)
            {
                edad.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_edad.Visibility = Visibility.Visible;
                lbl_edad.Content = "La edad no es entero";
                comprobar = false;
            }
            else if (edad.Text == String.Empty)
            {
                edad.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_edad.Visibility = Visibility.Visible;
                lbl_edad.Content = "Introduce la edad";
                comprobar = false;
            }
            else
            {
                lbl_edad.Visibility = Visibility.Hidden;
                edad.BorderBrush = new SolidColorBrush(color_default);
            }
            if (fechanacimiento.Text == String.Empty)
            {
                fechanacimiento.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_fechanacimiento.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                lbl_fechanacimiento.Visibility = Visibility.Hidden;
                fechanacimiento.BorderBrush = new SolidColorBrush(color_default);
            }
            if (usuario.Text == String.Empty)
            {
                usuario.BorderBrush = new SolidColorBrush(Colors.Red);
                lbl_usuario.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                lbl_usuario.Visibility = Visibility.Hidden;
                usuario.BorderBrush = new SolidColorBrush(color_default);
            }
            return comprobar;
        }
        private Boolean ComprobarEnteroEdad(TextBox edad)
        {
            Boolean entero = true;
            try
            {
                Convert.ToInt32(edad.Text);
            }
            catch (Exception exception)
            {
                entero = false;
            }
            return entero;
        }
        private void bt_cancelar_Click(object sender, RoutedEventArgs e)
        {
            Inicializar();
        }

        private void bt_eliminar_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = false;
            tabConfiguracion.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_eliminarUsuario.Visibility = Visibility.Visible;
            SystemSounds.Beep.Play();
        }

        private void bt_añadirCorreo_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = false;
            tabConfiguracion.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirCorreo.Visibility = Visibility.Visible;
            txt_anadircorreo.Focus();
        }
        private void btn_cancelarcorreo_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = true;
            tabConfiguracion.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            txt_anadircorreo.Text = "";
            border_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_anadircorreo_Click(object sender, RoutedEventArgs e)
        {
            ArrayList lista = (ArrayList)cb_correos.ItemsSource;
            lista.Add(txt_anadircorreo.Text);
            cb_correos.ItemsSource = null;
            cb_correos.ItemsSource = lista;
            cb_correos.Items.Refresh();
            cb_correos.SelectedIndex = 0;
            txt_anadircorreo.Text = "";
            CerrarConfiguracion.IsEnabled = true;
            tabConfiguracion.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void bt_eliminarCorreo_Click(object sender, RoutedEventArgs e)
        {
            if (cb_correos.SelectedIndex != -1)
            {
                ArrayList lista = (ArrayList)cb_correos.ItemsSource;
                lista.RemoveAt(cb_correos.SelectedIndex);
                cb_correos.ItemsSource = null;
                cb_correos.ItemsSource = lista;
                cb_correos.Items.Refresh();
                cb_correos.SelectedIndex = 0;
            }
        }

        private void bt_eliminartlf_Click(object sender, RoutedEventArgs e)
        {
            if (cb_telefonos.SelectedIndex != -1)
            {
                ArrayList lista = (ArrayList)cb_telefonos.ItemsSource;
                lista.RemoveAt(cb_telefonos.SelectedIndex);
                cb_telefonos.ItemsSource = null;
                cb_telefonos.ItemsSource = lista;
                cb_telefonos.Items.Refresh();
                cb_telefonos.Items.Refresh();
                cb_telefonos.SelectedIndex = 0;
            }
        }

        private void btn_anadirtlf_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                ArrayList lista = (ArrayList)cb_telefonos.ItemsSource;
                lista.Add(Convert.ToInt32(txt_anadirtlf.Text));
                cb_telefonos.ItemsSource = null;
                cb_telefonos.ItemsSource = lista;
                cb_telefonos.Items.Refresh();
                cb_telefonos.SelectedIndex = 0;
            }
            catch (Exception exception)
            {
                MessageBox.Show("No es un numero", "Error formato", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            txt_anadirtlf.Text = "";
            CerrarConfiguracion.IsEnabled = true;
            tabConfiguracion.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirTlf.Visibility = Visibility.Hidden;
        }

        private void btn_cancelartlf_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = true;
            tabConfiguracion.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            txt_anadirtlf.Text = "";
            border_AñadirTlf.Visibility = Visibility.Hidden;
        }

        private void bt_añadirtlf_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = false;
            tabConfiguracion.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirTlf.Visibility = Visibility.Visible;
            txt_anadirtlf.Focus();
        }

        private void btn_eliminarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }

        private void btn_cancelarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            CerrarConfiguracion.IsEnabled = true;
            tabConfiguracion.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            txt_anadirtlf.Text = "";
            border_eliminarUsuario.Visibility = Visibility.Hidden;
        }

        private void Boton_switch_Click(object sender, RoutedEventArgs e)
        {
            if (TemaClaro)
            {
                Img_switchoff.Visibility = Visibility.Hidden;
                Img_switchon.Visibility = Visibility.Visible;
                lbl_Tema_Switch.Content = "Oscuro";
                TemaClaro = false;
            }
            else
            {
                Img_switchoff.Visibility = Visibility.Visible;
                Img_switchon.Visibility = Visibility.Hidden;
                lbl_Tema_Switch.Content = "Claro";
                TemaClaro = true;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

            lbl_textomuestra.FontSize = Convert.ToInt32(Convert.ToDouble(Slider_tamaño.Value.ToString()));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Notificaciones1_Checked(object sender, RoutedEventArgs e)
        {
            if (Notificaciones1.IsChecked == false)
            {
                Notificaciones2.IsEnabled = false;
                Notificaciones3.IsEnabled = false;
                Notificaciones4.IsEnabled = false;
                Notificaciones5.IsEnabled = false;
                Notificaciones6.IsEnabled = false;
                Notificaciones7.IsEnabled = false;

            }
            else
            {
                Notificaciones2.IsEnabled = true;
                Notificaciones3.IsEnabled = true;
                Notificaciones4.IsEnabled = true;
                Notificaciones5.IsEnabled = true;
                Notificaciones6.IsEnabled = true;
                Notificaciones7.IsEnabled = true;
            }
        }
        private Boolean ComprobarNuevaContraseña()
        {
            Boolean comprobar = true;
            if (passAntiguaContraseña.Password.ToString() != String.Empty || passConfirmarContraseña.Password.ToString() != String.Empty || passConfirmarContraseña.Password.ToString() != String.Empty)
            {
                Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
                if (passAntiguaContraseña.Password.ToString() == usuario.contraseña.ToString())
                {
                    passAntiguaContraseña.BorderBrush = new SolidColorBrush(color_default);
                    lbl_ErrorAntiguaContraseña.Visibility = Visibility.Hidden;
                }
                else
                {
                    comprobar = false;
                    passAntiguaContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                    lbl_ErrorAntiguaContraseña.Visibility = Visibility.Visible;
                }
                if (passNuevaContraseña.Password.ToString() == passConfirmarContraseña.Password.ToString() && passConfirmarContraseña.Password.ToString() != String.Empty)
                {
                    passConfirmarContraseña.BorderBrush = new SolidColorBrush(color_default);
                    passConfirmarContraseña.SelectionBrush = new SolidColorBrush(color_default);
                    lbl_ErrorConfirmarContraseña.Visibility = Visibility.Hidden;
                }
                else if (passConfirmarContraseña.Password.ToString() == String.Empty)
                {
                    comprobar = false;
                    passConfirmarContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                    passConfirmarContraseña.SelectionBrush = new SolidColorBrush(Colors.Red);
                    lbl_ErrorConfirmarContraseña.Visibility = Visibility.Visible;
                    lbl_ErrorConfirmarContraseña.Content = "Confirme la contraseña";
                }
                else
                {
                    comprobar = false;
                    passConfirmarContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                    passConfirmarContraseña.SelectionBrush = new SolidColorBrush(Colors.Red);
                    lbl_ErrorConfirmarContraseña.Visibility = Visibility.Visible;
                    lbl_ErrorConfirmarContraseña.Content = "Las contraseñas no son iguales";
                }
                if (cb_1mayuscula.IsChecked ==true && cb_1minuscula.IsChecked == true)
                {
                    cb_1mayuscula.IsChecked = false;
                    cb_1minuscula.IsChecked = false;
                    lbl_condiciones.Visibility = Visibility.Hidden;
                    cb_1minuscula.Visibility = Visibility.Hidden;
                    cb_1mayuscula.Visibility = Visibility.Hidden;
                }
                else
                {
                    passNuevaContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                    passNuevaContraseña.SelectionBrush = new SolidColorBrush(Colors.Red);
                    comprobar = false;
                }
                if (passNuevaContraseña.Password.ToString() == String.Empty)
                {
                    passNuevaContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                    passNuevaContraseña.SelectionBrush = new SolidColorBrush(Colors.Red);
                    comprobar = false;
                    lbl_ErrorNuevaContraseña.Visibility = Visibility.Visible;
                }
                else
                {
                    passNuevaContraseña.BorderBrush = new SolidColorBrush(color_default);
                    passNuevaContraseña.SelectionBrush = new SolidColorBrush(color_default);
                    lbl_ErrorNuevaContraseña.Visibility = Visibility.Hidden;
                }
            }
            return comprobar;
        }
        private void passContraseña_Change(object sender, RoutedEventArgs e)
        {
            string contraseña = passNuevaContraseña.Password.ToString();
            if (contraseña != String.Empty)
            {
                Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
                lbl_ErrorNuevaContraseña.Visibility = Visibility.Hidden;
                passNuevaContraseña.BorderBrush = new SolidColorBrush(color_default);
                passNuevaContraseña.SelectionBrush = new SolidColorBrush(color_default);
                Boolean minuscula = false;
                Boolean mayuscula = false;
                lbl_condiciones.Visibility = Visibility.Visible;
                cb_1minuscula.Visibility = Visibility.Visible;
                cb_1mayuscula.Visibility = Visibility.Visible;
                for (int i = 0; i < contraseña.Length; i++)
                {
                    if (Char.IsUpper(contraseña[i]))
                    {
                        mayuscula = true;
                    }
                    if (Char.IsLower(contraseña[i]))
                    {

                        minuscula = true;
                    }
                }
                if (mayuscula)
                {
                    cb_1mayuscula.IsChecked = true;
                    cb_1mayuscula.Foreground = new SolidColorBrush(Colors.Green);

                }
                else
                {
                    cb_1mayuscula.IsChecked = false;
                    cb_1mayuscula.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (minuscula)
                {
                    cb_1minuscula.IsChecked = true;
                    cb_1minuscula.Foreground = new SolidColorBrush(Colors.Green);
                }
                else
                {
                    cb_1minuscula.IsChecked = false;
                    cb_1minuscula.Foreground = new SolidColorBrush(Colors.Red);
                }
                if (mayuscula && minuscula)
                {
                    passNuevaContraseña.BorderBrush = new SolidColorBrush(Colors.Green);
                    passNuevaContraseña.SelectionBrush = new SolidColorBrush(Colors.Green);
                }
            }
            else
            {
                cb_1mayuscula.IsChecked = false;
                cb_1minuscula.IsChecked = false;
                lbl_condiciones.Visibility = Visibility.Hidden;
                cb_1minuscula.Visibility = Visibility.Hidden;
                cb_1mayuscula.Visibility = Visibility.Hidden;
            }
        }
        private void passConfirmarContraseña_Changed(object sender, RoutedEventArgs e)
        {
            if (passNuevaContraseña.Password.ToString() == passConfirmarContraseña.Password.ToString() && passConfirmarContraseña.Password.ToString() != String.Empty)
            {
                lbl_ErrorConfirmarContraseña.Visibility = Visibility.Hidden;
                passConfirmarContraseña.BorderBrush = new SolidColorBrush(Colors.Green);
                passConfirmarContraseña.SelectionBrush = new SolidColorBrush(Colors.Green);
            }
            else
            {
                lbl_ErrorConfirmarContraseña.Visibility = Visibility.Visible;
                passConfirmarContraseña.BorderBrush = new SolidColorBrush(Colors.Red);
                passConfirmarContraseña.SelectionBrush = new SolidColorBrush(Colors.Red);
            }
        }
    }
}
