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
using System.Xml;
using DatosExcursionista;
using DatosRutas;
using DatosUsuario;
using DatosGuia;
using System.Collections;
using System.Media;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Ventana_Guias.xaml
    /// </summary>
    public partial class Ventana_Guias : Window
    {
        Usuario usuario;
        List<Guia> listado_guias;
        List<Ruta> listadoRutas;
        public Ventana_Guias(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            listado_guias = CargarGuiasXML();
            lstGuias.ItemsSource = listado_guias;
            listadoRutas = CargarRutasXML();
        }

        private List<Guia> CargarGuiasXML()
        {
            List<Guia> listado = new List<Guia>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Guias.xml", UriKind.Relative)); doc.Load(fichero.Stream);
            XmlNodeList nodes = doc.SelectNodes("Guias/Guia");
            foreach (XmlNode node in nodes)
            {
                var nuevoGuia = new Guia();
                nuevoGuia.id = (Convert.ToInt32(node.Attributes[0].Value));
                nuevoGuia.Nombre = node.ChildNodes[0].InnerText;
                nuevoGuia.Apellido = node.ChildNodes[1].InnerText;
                nuevoGuia.Edad = (Convert.ToInt32(node.ChildNodes[2].InnerText));
                string[] telefonos = node.ChildNodes[3].InnerText.Split(',');
                for (int i = 0; i < telefonos.Length; i++)
                {
                    nuevoGuia.Telefono.Add(Convert.ToInt32(telefonos[i]));
                }
                string[] correos = node.ChildNodes[4].InnerText.Split(',');
                for (int i = 0; i<correos.Length; i++)
                {
                    nuevoGuia.Correo.Add(correos[i]);
                }
                string[] idiomas = node.ChildNodes[5].InnerText.Split(',');
                for (int i = 0; i<idiomas.Length; i++)
                {
                    nuevoGuia.idiomas.Add(idiomas[i]);
                }
                string[] ruta1 = node.ChildNodes[7].InnerText.Split(',');
                for (int i = 0; i < ruta1.Length; i++)
                {
                    nuevoGuia.Rutas1.Add(ruta1[i]);
                }
                string[] ruta2 = node.ChildNodes[8].InnerText.Split(',');
                for (int i = 0; i < ruta2.Length; i++)
                {
                    nuevoGuia.Rutas2.Add(ruta2[i]);
                }
                string[] lunes = node.ChildNodes[9].InnerText.Split(',');
                for (int i = 0; i < lunes.Length; i++)
                {
                    nuevoGuia.lunes.Add(lunes[i]);
                }
                string[] martes = node.ChildNodes[10].InnerText.Split(',');
                for (int i = 0; i < martes.Length; i++)
                {
                    nuevoGuia.martes.Add(martes[i]);
                }
                string[] miercoles = node.ChildNodes[11].InnerText.Split(',');
                for (int i = 0; i < miercoles.Length; i++)
                {
                    nuevoGuia.miercoles.Add(miercoles[i]);
                }
                string[] jueves = node.ChildNodes[12].InnerText.Split(',');
                for (int i = 0; i < jueves.Length; i++)
                {
                    nuevoGuia.jueves.Add(jueves[i]);
                }
                string[] viernes = node.ChildNodes[13].InnerText.Split(',');
                for (int i = 0; i < viernes.Length; i++)
                {
                    nuevoGuia.viernes.Add(viernes[i]);
                }
                string[] sabado = node.ChildNodes[14].InnerText.Split(',');
                for (int i = 0; i < sabado.Length; i++)
                {
                    nuevoGuia.sabado.Add(sabado[i]);
                }
                string[] domingo = node.ChildNodes[15].InnerText.Split(',');
                for (int i = 0; i < domingo.Length; i++)
                {
                    nuevoGuia.domingo.Add(domingo[i]);
                }
                nuevoGuia.Caratula = (new Uri(node.ChildNodes[6].InnerText, UriKind.Relative));
                listado.Add(nuevoGuia);
            }
            return listado;
        }

        private List<Ruta> CargarRutasXML()
        {
            List<Ruta> listado = new List<Ruta>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Rutas.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            XmlNodeList nodes = doc.SelectNodes("Rutas/Ruta");
            foreach (XmlNode node in nodes)
            {
                var nuevaRuta = new Ruta(0, "", "", "", 0, 0, "", 0, "", 0, null);
                nuevaRuta.id =(Convert.ToInt32(node.Attributes[0].Value));
                nuevaRuta.Nombre = node.ChildNodes[0].InnerText;
                nuevaRuta.Origen = node.ChildNodes[1].InnerText;
                nuevaRuta.Destino = node.ChildNodes[2].InnerText;
                nuevaRuta.Distancia = (Convert.ToInt32(node.ChildNodes[3].InnerText));
                nuevaRuta.Altitud = (Convert.ToInt32(node.ChildNodes[4].InnerText));
                nuevaRuta.Guia = node.ChildNodes[5].InnerText;
                nuevaRuta.maxParticipantes = (Convert.ToInt32(node.ChildNodes[6].InnerText));
                nuevaRuta.fecha = Convert.ToDateTime(node.ChildNodes[7].InnerText);
                nuevaRuta.hora = node.ChildNodes[8].InnerText;
                nuevaRuta.duracion = (Convert.ToInt32(node.ChildNodes[9].InnerText));
                nuevaRuta.foto = new Uri(node.ChildNodes[10].InnerText, UriKind.Relative);
                listado.Add(nuevaRuta);
            }
            return listado;
        }

        private void rellenarHorario(Guia guia)
        {
            ArrayList horas = new ArrayList();
            horas.Add("8:30");
            horas.Add("10:30");
            horas.Add("12:30");
            horas.Add("14:30");
            horas.Add("16:30");
            horas.Add("18:30");
            horas.Add("20:30");
            dg_horas.Items.Clear();
            dg_rutasExcursionistas1.Items.Clear();
            for (int i = 0; i<guia.lunes.Count; i++)
            {

                dg_rutasExcursionistas1.Items.Add(new
                {
                    lunes = guia.lunes[i].ToString(),
                    martes = guia.martes[i].ToString(),
                    miercoles = guia.miercoles[i].ToString(),
                    jueves = guia.jueves[i].ToString(),
                    viernes = guia.viernes[i].ToString(),
                    sabado = guia.sabado[i].ToString(),
                    domingo = guia.domingo[i].ToString(),

                });
                dg_horas.Items.Add(new
                {
                    hora = horas[i].ToString()
                });
            }
        }

        private void btn_excursionistas_Click_1(object sender, RoutedEventArgs e)
        {
            Ventana_Excursionistas ventana_excursionistas = new Ventana_Excursionistas(usuario);
            ventana_excursionistas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_excursionistas.Show();
            this.Close();
        }

        private void btn_configuracion_Click(object sender, RoutedEventArgs e)
        {
            Configuracion config = new Configuracion(usuario);
            config.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            config.Show();
            this.Close();
        }

        private void btn_cerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }

        private void lstGuias_SelectionNull(object sender, MouseButtonEventArgs controlUnderMouse)
        {
            if (controlUnderMouse.GetType() != typeof(ListBoxItem))
            {
                lstGuias.SelectedItem = null;
                cb_correos.Items.Clear();
                cb_telefonos.Items.Clear();
                tbDatos.Visibility = Visibility.Hidden;
                bd_opcionesdatos.Visibility = Visibility.Hidden;
                grid_NoSeleccionado.Visibility = Visibility.Visible;
            }
        }

        private void lstGuias_SelectionChanged2(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex != -1)
            {
                tbDatos.Visibility = Visibility.Visible;
                bd_opcionesdatos.Visibility = Visibility.Visible;
                grid_NoSeleccionado.Visibility = Visibility.Hidden;
                cb_correos.SelectedIndex = 0;
                cb_telefonos.SelectedIndex = 0;

                Guia ex = (Guia)lstGuias.Items[lstGuias.SelectedIndex];
                rellenarHorario(ex);
                txt_nombre.Text = ex.Nombre;
                txt_apellidos.Text = ex.Apellido;
                txt_edad.Text = ex.Edad.ToString();
                Comprobar3Campos(txt_nombre, txt_apellidos, txt_edad, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
                if (ex.Caratula != null)
                {
                    imgExcursionista.Source = new BitmapImage(ex.Caratula);
                }
                else
                    imgExcursionista.Source = null;

                cb_correos.Items.Clear();
                cb_telefonos.Items.Clear();
                cb_correos.Items.Clear();
                cb_idiomas.Items.Clear();
                for (int i = 0; i < ex.Correo.Count; i++)
                {
                    cb_correos.Items.Add(ex.Correo[i]);
                }
                for (int i = 0; i< ex.Telefono.Count; i++)
                {
                    cb_telefonos.Items.Add(ex.Telefono[i]);
                }
                for (int j = 0; j < ex.idiomas.Count; j++)
                {
                    cb_idiomas.Items.Add(ex.idiomas[j]);
                }
                List<Ruta> listadoRutasDelGuia = new List<Ruta>();
                for (int i = 0; i < ex.Rutas1.Count; i++)
                {
                    int valor = Convert.ToInt32(ex.Rutas1[i]);
                    Ruta ruta = listadoRutas.ElementAt(valor);
                    ruta.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                    listadoRutasDelGuia.Add(ruta);
                }
                dg_rutasExcursionistas.ItemsSource = null;
                dg_rutasExcursionistas.ItemsSource = listadoRutasDelGuia;
            }
            else
            {
                lstGuias.SelectedItem = null;
            }
        }

        private void btn_anadirguia_Click_1(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_anadirGuia_1.Visibility = Visibility.Visible;
            dg_anadirruta_NuevoUsuario.ItemsSource = CargarRutasXML();
            dg_NU_seleccionar.ItemsSource = CargarRutasXML();

        }

        private void btn_cancelarNuevoUsuario1_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Visible;
            SystemSounds.Beep.Play();
        }

        private void btn_cancelarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Visible;
            SystemSounds.Beep.Play();
        }

        private void btn_NU_Permanecer_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Hidden;
        }

        private void btn_NU_Salir_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Hidden;
            Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_anadirGuia_1.Visibility = Visibility.Hidden;
            border_anadirGuia_2.Visibility = Visibility.Hidden;
            cb_Anadir_correos.Items.Clear();
            cb_Anadir_telefonos.Items.Clear();
            txt_Anadir_nombre.Text = "";
            lbl_NU_ErrorDatos_Nombre.Visibility = Visibility.Hidden;
            txt_Anadir_nombre.BorderBrush = new SolidColorBrush(color_default);
            txt_Anadir_edad.Text = "";
            lbl_NU_ErrorDatos_Edad.Visibility = Visibility.Hidden;
            txt_Anadir_edad.BorderBrush = new SolidColorBrush(color_default);
            txt_Anadir_apellidos.Text = "";
            lbl_NU_ErrorDatos_Apellidos.Visibility = Visibility.Hidden;
            txt_Anadir_apellidos.BorderBrush = new SolidColorBrush(color_default);
            img_Anadir_Guia.Source = null;
            dg_anadirruta_NuevoUsuario.ItemsSource = null;
        }

        private void btn_siguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            {
                border_anadirGuia_2.Visibility = Visibility.Visible;
                border_anadirGuia_1.Visibility = Visibility.Hidden;
            }
        }

        private Boolean Comprobar3Campos(TextBox nombre, TextBox apellido, TextBox edad, Label mensajeErrorNombre, Label mensajeErrorApellido, Label mensajeErrorEdad)
        {
            Boolean comprobar = true;
            Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
            if (nombre.Text == String.Empty)
            {
                nombre.BorderBrush = new SolidColorBrush(Colors.Red);
                mensajeErrorNombre.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                mensajeErrorNombre.Visibility = Visibility.Hidden;
                nombre.BorderBrush = new SolidColorBrush(color_default);
            }
            if (apellido.Text == String.Empty)
            {
                apellido.BorderBrush = new SolidColorBrush(Colors.Red);
                mensajeErrorApellido.Visibility = Visibility.Visible;
                comprobar = false;
            }
            else
            {
                mensajeErrorApellido.Visibility = Visibility.Hidden;
                apellido.BorderBrush = new SolidColorBrush(color_default);
            }
            if (ComprobarEnteroEdad(edad) == false)
            {
                edad.BorderBrush = new SolidColorBrush(Colors.Red);
                mensajeErrorEdad.Visibility = Visibility.Visible;
                mensajeErrorEdad.Content = "La edad no es entero";
                comprobar = false;
            }
            else if (edad.Text == String.Empty)
            {
                edad.BorderBrush = new SolidColorBrush(Colors.Red);
                mensajeErrorEdad.Visibility = Visibility.Visible;
                mensajeErrorEdad.Content = "Introduce la edad";
                comprobar = false;
            }
            else
            {
                mensajeErrorEdad.Visibility = Visibility.Hidden;
                edad.BorderBrush = new SolidColorBrush(color_default);
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

        private void btn_anterior_Click(object sender, RoutedEventArgs e)
        {
            border_anadirGuia_1.Visibility = Visibility.Visible;
            border_anadirGuia_2.Visibility = Visibility.Hidden;
        }

        private void btn_cerrarsesion_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }

        private void btn_Inicio_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio(usuario);
            this.Visibility = Visibility.Hidden;
            inicio.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inicio.Show();
        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayuda = new Ayuda(usuario);
            ayuda.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ayuda.Show();
            this.Close();
        }

        private void bt_Anadir_añadirCorreo_Click(object sender, RoutedEventArgs e)
        {
            border_anadirGuia_1.IsEnabled = false;
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Visible;
        }

        private void bt_Anadir_añadirTelefono_Click(object sender, RoutedEventArgs e)
        {
            border_anadirGuia_1.IsEnabled = false;
            border_NuevoUsuario_AñadirTlf.Visibility = Visibility.Visible;
        }

        private void btn_Anadir_CargarImagen_Click(object sender, RoutedEventArgs e)
        {
            CargarImagen(img_Anadir_Guia);
        }

        private void btnCargarImagen_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage imagen = CargarImagen(imgExcursionista);
            imgExcursionista.Source = imagen;
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

        private void btn_NU_cancelarcorreo_Click(object sender, RoutedEventArgs e)
        {
            border_anadirGuia_1.IsEnabled = true;
            txt_NU_anadircorreo.Text = "";
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_NU_anadircorreo_Click(object sender, RoutedEventArgs e)
        {
            cb_Anadir_correos.Items.Add(txt_NU_anadircorreo.Text);
            txt_NU_anadircorreo.Text = "";
            border_anadirGuia_1.IsEnabled = true;
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Hidden;
        }


        private void btn_NU_cancelartlf_Click_1(object sender, RoutedEventArgs e)
        {
            border_anadirGuia_1.IsEnabled = true;
            border_NuevoUsuario_AñadirTlf.Visibility = Visibility.Hidden;
            txt_NU_anadirtlf.Text = "";
        }

        private void btn_NU_anadirtlf_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                int telefono = Convert.ToInt32(txt_NU_anadirtlf.Text);
                cb_Anadir_telefonos.Items.Add(telefono);
            }
            catch (Exception exception)
            {
                MessageBox.Show("No es un numero entero", "Error formato", MessageBoxButton.OK, MessageBoxImage.Error);


            }
            txt_NU_anadirtlf.Text = "";
            border_anadirGuia_1.IsEnabled = true;
            border_NuevoUsuario_AñadirTlf.Visibility = Visibility.Hidden;
        }

        private void bt_eliminarCorreo_Click(object sender, RoutedEventArgs e)
        {
            if (cb_correos.SelectedIndex != -1)
            {
                cb_correos.Items.RemoveAt(cb_correos.SelectedIndex);
                cb_correos.Items.Refresh();
                cb_correos.SelectedIndex = 0;
            }
        }

        private void bt_eliminarIidoma_Click(object sender, RoutedEventArgs e)
        {
            if (cb_idiomas.SelectedIndex != -1)
            {
                cb_idiomas.Items.RemoveAt(cb_telefonos.SelectedIndex);
                cb_idiomas.Items.Refresh();
                cb_idiomas.SelectedIndex = 0;
            }
        }

        private void bt_Anadir_eliminarCorreo_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Anadir_correos.SelectedIndex != -1)
            {
                cb_Anadir_correos.Items.RemoveAt(cb_Anadir_correos.SelectedIndex);
                cb_Anadir_correos.Items.Refresh();
                cb_Anadir_correos.SelectedIndex = 0;
            }
        }

        private void bt_Anadir_eliminartlf_Click(object sender, RoutedEventArgs e)
        {
            if (cb_Anadir_telefonos.SelectedIndex != -1)
            {
                cb_Anadir_telefonos.Items.RemoveAt(cb_Anadir_telefonos.SelectedIndex);
                cb_Anadir_telefonos.Items.Refresh();
                cb_Anadir_telefonos.SelectedIndex = 0;
            }
        }

        private void bt_añadirIdioma_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirIdioma.Visibility = Visibility.Visible;
            txt_anadircorreo.Focus();
        }

        private void bt_añadirCorreo_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirCorreo.Visibility = Visibility.Visible;
            txt_anadircorreo.Focus();
        }

        private void bt_añadirTelefono_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirTlf.Visibility = Visibility.Visible;
            txt_anadirtlf.Focus();
        }


        private void btn_cancelarcorreo_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            txt_anadircorreo.Text = "";
            border_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_anadircorreo_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex !=-1)
            {
                Guia ex = (Guia)lstGuias.SelectedItem;
                cb_correos.Items.Add(txt_anadircorreo.Text);
            }
            txt_anadircorreo.Text = "";
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_cancelartlf_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirTlf.Visibility = Visibility.Hidden;
            txt_anadirtlf.Text = "";
        }

        private void btn_anadirtlf_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex != -1)
            {
                Guia ex = (Guia)lstGuias.SelectedItem;
                try
                {
                    cb_telefonos.Items.Add(txt_anadirtlf.Text);
                }
                catch (Exception exception)
                {
                    MessageBox.Show("No es un numero", "Error formato", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }
            txt_anadirtlf.Text = "";
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirTlf.Visibility = Visibility.Hidden;
        }

        private void btn_cancelarIdioma_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            txt_anadirIdioma.Text = "";
            border_AñadirIdioma.Visibility = Visibility.Hidden;
        }

        private void btn_anadirIidoma_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex !=-1)
            {
                Guia ex = (Guia)lstGuias.SelectedItem;
                cb_idiomas.Items.Add(txt_anadirIdioma.Text);
            }
            txt_anadirIdioma.Text = "";
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirIdioma.Visibility = Visibility.Hidden;
        }

        private void bt_eliminartlf_Click(object sender, RoutedEventArgs e)
        {
            if (cb_telefonos.SelectedIndex != -1)
            {
                cb_telefonos.Items.RemoveAt(cb_telefonos.SelectedIndex);
                cb_telefonos.Items.Refresh();
                cb_telefonos.SelectedIndex = 0;
            }
        }

        private void bt_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex != -1)
            {
                border_eliminarGuia.Visibility = Visibility.Visible;
                Grid_menu.IsEnabled = false;
                border_datos.IsEnabled = false;
                bd_opcionesdatos.IsEnabled = false;
                SystemSounds.Beep.Play();
            }
        }

        private void bt_cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (lstGuias.SelectedIndex != -1)
            {
                Guia ex = (Guia)listado_guias[lstGuias.SelectedIndex];
                txt_nombre.Text = ex.Nombre;
                txt_apellidos.Text = ex.Apellido;
                txt_edad.Text = ex.Edad.ToString();
                Comprobar3Campos(txt_nombre, txt_apellidos, txt_edad, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
                imgExcursionista.Source = new BitmapImage(ex.Caratula);
                cb_correos.Items.Clear();
                for (int i = 0; i<ex.Correo.Count; i++)
                {
                    cb_correos.Items.Add(ex.Correo[i].ToString());
                }
                cb_correos.Items.Refresh();
                cb_correos.SelectedIndex = 0;

                cb_telefonos.Items.Clear();
                for (int i = 0; i < ex.Telefono.Count; i++)
                {
                    cb_telefonos.Items.Add(ex.Telefono[i]);
                }
                cb_telefonos.Items.Refresh();
                cb_telefonos.SelectedIndex = 0;

                cb_idiomas.Items.Clear();
                for (int i = 0; i < ex.idiomas.Count; i++)
                {
                    cb_idiomas.Items.Add(ex.idiomas[i]);
                }
                cb_idiomas.Items.Refresh();
                cb_idiomas.SelectedIndex = 0;

                List<Ruta> listadoRutasDelExcursionista = new List<Ruta>();
                for (int i = 0; i < ex.Rutas1.Count; i++)
                {
                    int valor = Convert.ToInt32(ex.Rutas1[i]);
                    Ruta ruta = listadoRutas.ElementAt(valor);
                    ruta.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                    listadoRutasDelExcursionista.Add(ruta);
                }
                dg_rutasExcursionistas.ItemsSource = listadoRutasDelExcursionista;
            }
        }

        private void btn_rutas_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Rutas ventana_rutas = new Ventana_Rutas(usuario);
            ventana_rutas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_rutas.Show();
            this.Close();
        }


        private void bt_guardar_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Rutas.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            Boolean campos = Comprobar3Campos(txt_nombre, txt_apellidos, txt_edad, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
            if (campos == false)
            {
                tbDatos.SelectedIndex = 0;
            }
            if (lstGuias.SelectedIndex != -1 && campos)
            {
                Guia ex = (Guia)lstGuias.SelectedItem;
                ex.Nombre = txt_nombre.Text;
                ex.Apellido = txt_apellidos.Text;
                ex.Edad = Convert.ToInt32(txt_edad.Text);
                ex.Telefono.Clear();
                ex.idiomas.Clear();
                string telefonos = "";
                for (int i = 0; i < cb_telefonos.Items.Count - 1; i++)
                {
                    telefonos += cb_telefonos.Items.GetItemAt(i) + ",";
                    ex.Telefono.Add(Convert.ToInt32(cb_telefonos.Items.GetItemAt(i)));
                }
                telefonos += cb_telefonos.Items.GetItemAt(cb_telefonos.Items.Count - 1);
                ex.Telefono.Add(Convert.ToInt32(cb_telefonos.Items.GetItemAt(cb_telefonos.Items.Count - 1)));
                string idiomas = "";
                for (int i = 0; i < cb_idiomas.Items.Count - 1; i++)
                {
                    idiomas += cb_idiomas.Items.GetItemAt(i) + ",";
                    ex.idiomas.Add(cb_idiomas.Items.GetItemAt(i));
                }
                if (cb_idiomas.Items.Count>0)
                {
                    idiomas += cb_idiomas.Items.GetItemAt(cb_idiomas.Items.Count - 1);
                    ex.idiomas.Add(cb_idiomas.Items.GetItemAt(cb_idiomas.Items.Count - 1));
                }
                ex.Correo.Clear();
                string correos = "";
                for (int i = 0; i < cb_correos.Items.Count -1; i++)
                {
                    correos += cb_correos.Items.GetItemAt(i)  + ",";
                    ex.Correo.Add(cb_correos.Items.GetItemAt(i));
                }
                correos += cb_correos.Items.GetItemAt(cb_correos.Items.Count-1);
                ex.Correo.Add(cb_correos.Items.GetItemAt(cb_correos.Items.Count - 1));
                ex.Caratula = new Uri(imgExcursionista.Source.ToString(), UriKind.Absolute);

                listado_guias[lstGuias.SelectedIndex] = ex;
                lstGuias.ItemsSource = listado_guias;
                lstGuias.Items.Refresh();

                ex.Rutas1.Clear();
                ex.Rutas2.Clear();
                for (int i = 0; i < dg_rutasExcursionistas.Items.Count; i++)
                {
                    Ruta r = (Ruta)dg_rutasExcursionistas.Items[i];
                    ex.Rutas1.Add(r.id);
                    ex.Rutas2.Add(r.Realizada);
                }

                List<Ruta> listadoRutasDelExcursionista = rutasdelguia(ex);
                dg_rutasExcursionistas.ItemsSource = listadoRutasDelExcursionista;
                dg_rutasExcursionistas.Items.Refresh();
                MessageBox.Show("Datos guardados con exito", "Guardar datos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private List<Ruta> rutasdelguia(Guia ex)
        {
            List<Ruta> listadoRutasDelGuia = new List<Ruta>();
            for (int i = 0; i < ex.Rutas1.Count; i++)
            {
                int valor = Convert.ToInt32(ex.Rutas1[i]);
                Ruta ruta = listadoRutas.ElementAt(valor);
                ruta.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                listadoRutasDelGuia.Add(ruta);
            }
            return listadoRutasDelGuia;
        }

        private void btn_cancelarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            border_eliminarGuia.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }

        private void btn_eliminarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            Guia ex = (Guia)lstGuias.SelectedItem;
            listado_guias.Remove(ex);
            lstGuias.ItemsSource = null;
            lstGuias.ItemsSource = listado_guias;
            lstGuias.Items.Refresh();
            lstGuias.SelectedIndex = -1;
            tbDatos.Visibility = Visibility.Hidden;
            bd_opcionesdatos.Visibility = Visibility.Hidden;
            grid_NoSeleccionado.Visibility = Visibility.Visible;
            border_eliminarGuia.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }

        private void bt_eliminarIdioma_Click(object sender, RoutedEventArgs e)
        {
            if (cb_idiomas.SelectedIndex != -1)
            {
                cb_idiomas.Items.RemoveAt(cb_idiomas.SelectedIndex);
                cb_idiomas.Items.Refresh();
                cb_idiomas.SelectedIndex = 0;
            }
        }

        private void btn_guardarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            Boolean Guardado_correcto = true;
            Guia ex = new Guia();
            int id = lstGuias.Items.Count;
            ex.id = id+1;
            ex.Nombre = txt_Anadir_nombre.Text;
            ex.Apellido = txt_Anadir_apellidos.Text;
            lstGuias.ItemsSource = null;
            try
            {
                ex.Edad = Convert.ToInt32(txt_Anadir_edad.Text);
            }
            catch (Exception exception)
            {
                Guardado_correcto=false;
                MessageBox.Show("El campo edad no es un numero entero", "Error formato", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            for (int i = 0; i < cb_Anadir_correos.Items.Count; i++)
            {
                ex.Correo.Add(cb_Anadir_correos.Items[i]);
            }
            for (int i = 0; i < cb_Anadir_telefonos.Items.Count; i++)
            {
                ex.Correo.Add(Convert.ToInt32(cb_Anadir_telefonos.Items[i]));
            }
            if (img_Anadir_Guia.Source != null)
            {
                ex.Caratula = new Uri(img_Anadir_Guia.Source.ToString());

            }
            for (int i = 0; i < dg_anadirruta_NuevoUsuario.Items.Count; i++)
            {
                Ruta r = (Ruta)dg_anadirruta_NuevoUsuario.Items[i];
                Ruta r2 = (Ruta)dg_NU_seleccionar.Items[i];
                if (r2.Seleccionada)
                {
                    ex.Rutas1.Add(r.id);
                    ex.Rutas2.Add(r.Realizada);
                }
            }
            if (Guardado_correcto)
            {
                MessageBox.Show("Nuevo usuario registrado con exito", "Nuevo usuario", MessageBoxButton.OK, MessageBoxImage.Information);
                listado_guias.Add(ex);
                lstGuias.ItemsSource = listado_guias;
                lstGuias.Items.Refresh();
                btn_NU_Salir_Click(sender, e);
            }
        }
    }
}
