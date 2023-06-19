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
using System.Collections;
using System.Media;

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Excursionistas.xaml
    /// </summary>
    public partial class Ventana_Excursionistas : Window
    {
        List<Excursionista> listadoexcursionistas;
        List<Ruta> listadoRutas;
        public Usuario usuario;
        public Ventana_Excursionistas(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            listadoexcursionistas = new List<Excursionista>();
            listadoexcursionistas = CargarExcursionistasXML();
            listadoRutas = new List<Ruta>();
            listadoRutas = CargarRutasXML();

            lstExcursionistas.ItemsSource = listadoexcursionistas;
            //dg_rutasExcursionistas.ItemsSource = listadoRutas;
        }
        private List<Excursionista> CargarExcursionistasXML()
        {
            List<Excursionista> listado = new List<Excursionista>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/Excursionistas.xml", UriKind.Relative)); doc.Load(fichero.Stream);
            XmlNodeList nodes = doc.SelectNodes("Excursionistas/Excursionista");
            foreach (XmlNode node in nodes)
            {
                var nuevoExcursionista = new Excursionista("", "", 0, null);
                nuevoExcursionista.id = (Convert.ToInt32(node.Attributes[0].Value));
                nuevoExcursionista.Nombre = node.ChildNodes[0].InnerText;
                nuevoExcursionista.Apellido = node.ChildNodes[1].InnerText;
                nuevoExcursionista.Edad = (Convert.ToInt32(node.ChildNodes[2].InnerText));
                string [] telefonos = node.ChildNodes[3].InnerText.Split(',');
                for (int i = 0; i < telefonos.Length; i++)
                {
                    nuevoExcursionista.Telefono.Add(Convert.ToInt32(telefonos[i]));
                }
                string[] correos = node.ChildNodes[4].InnerText.Split(',');
                for (int i = 0; i<correos.Length; i++)
                {
                    nuevoExcursionista.Correo.Add(correos[i]);
                }
                string[] ruta1 = node.ChildNodes[6].InnerText.Split(',');
                string[] ruta2 = node.ChildNodes[7].InnerText.Split(',');
                for (int i = 0; i < ruta1.Length; i++)
                {
                    nuevoExcursionista.Rutas1.Add(ruta1[i]);
                    nuevoExcursionista.Rutas2.Add(ruta2[i]);
                }

                nuevoExcursionista.Caratula = (new Uri(node.ChildNodes[5].InnerText, UriKind.Relative));
                listado.Add(nuevoExcursionista);
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
                var nuevaRuta = new Ruta(0, "", "","", 0, 0, "", 0, "", 0, null);
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
        


        private void btn_Inicio_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio(usuario);
            this.Visibility = Visibility.Hidden;
            inicio.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inicio.Show();
        }

        private void btn_rutas_Click_1(object sender, RoutedEventArgs e)
        {
            Ventana_Rutas ventana_rutas = new Ventana_Rutas(usuario);
            ventana_rutas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_rutas.Show();
            this.Close();
        }

        private void btn_excursionistas_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_guias_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Guias ventana_guias = new Ventana_Guias(usuario);
            ventana_guias.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_guias.Show();
            this.Close();
        }

        private void btn_cerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }
        
        private void lstExcursionistas_SelectionNull(object sender, MouseButtonEventArgs controlUnderMouse)
        {
            if (controlUnderMouse.GetType() != typeof(ListBoxItem))
            {
                lstExcursionistas.SelectedItem = null;
                cb_correos.Items.Clear();
                cb_telefonos.Items.Clear();
                tbDatos.Visibility = Visibility.Hidden;
                bd_opcionesdatos.Visibility = Visibility.Hidden;
                grid_NoSeleccionado.Visibility = Visibility.Visible;
            }
        }
        
        private void lstExcursionistas_SelectionChanged2(object sender, RoutedEventArgs e)
        {
            if (lstExcursionistas.SelectedIndex != -1)
            {
                tbDatos.Visibility = Visibility.Visible;
                bd_opcionesdatos.Visibility = Visibility.Visible;
                grid_NoSeleccionado.Visibility = Visibility.Hidden;
                cb_correos.SelectedIndex = 0;
                cb_telefonos.SelectedIndex = 0;

                Excursionista ex = (Excursionista)lstExcursionistas.Items[lstExcursionistas.SelectedIndex];
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
                for (int i = 0; i < ex.Correo.Count; i++)
                {
                    cb_correos.Items.Add(ex.Correo[i]);
                }
                for (int i =0; i< ex.Telefono.Count; i++)
                {
                    cb_telefonos.Items.Add(ex.Telefono[i]);
                }
                List<Ruta> listadoRutasDelExcursionista = new List<Ruta>();
                for (int i = 0; i < ex.Rutas1.Count; i++)
                {
                    int valor = Convert.ToInt32(ex.Rutas1[i]);
                    Ruta ruta = listadoRutas.ElementAt(valor);
                    ruta.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                    listadoRutasDelExcursionista.Add(ruta);
                }
                dg_rutasExcursionistas.ItemsSource = null;
                dg_rutasExcursionistas.ItemsSource = listadoRutasDelExcursionista;
            }
            else
            {
                lstExcursionistas.SelectedItem = null;
            }
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
            if (lstExcursionistas.SelectedIndex != -1 && campos)
            {
                Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
                ex.Nombre = txt_nombre.Text;
                ex.Apellido = txt_apellidos.Text;
                ex.Edad = Convert.ToInt32(txt_edad.Text);
                ex.Telefono.Clear();
                string telefonos = "";
                for (int i = 0; i < cb_telefonos.Items.Count - 1; i++)
                {
                    telefonos += cb_telefonos.Items.GetItemAt(i) + ",";
                    ex.Telefono.Add(Convert.ToInt32(cb_telefonos.Items.GetItemAt(i)));
                }
                telefonos += cb_telefonos.Items.GetItemAt(cb_telefonos.Items.Count - 1);
                ex.Telefono.Add(Convert.ToInt32(cb_telefonos.Items.GetItemAt(cb_telefonos.Items.Count - 1)));
                ex.Correo.Clear();
                string correos = "";
                for (int i=0; i < cb_correos.Items.Count -1; i++)
                {
                    correos += cb_correos.Items.GetItemAt(i)  + ",";
                    ex.Correo.Add(cb_correos.Items.GetItemAt(i));
                }
                correos += cb_correos.Items.GetItemAt(cb_correos.Items.Count-1);
                ex.Correo.Add(cb_correos.Items.GetItemAt(cb_correos.Items.Count - 1));
                ex.Caratula = new Uri(imgExcursionista.Source.ToString(), UriKind.Absolute);

                listadoexcursionistas[lstExcursionistas.SelectedIndex] = ex;
                lstExcursionistas.ItemsSource = listadoexcursionistas;
                lstExcursionistas.Items.Refresh();

                ex.Rutas1.Clear();
                ex.Rutas2.Clear();
                for (int i=0; i < dg_rutasExcursionistas.Items.Count; i++)
                {
                    Ruta r = (Ruta)dg_rutasExcursionistas.Items[i];
                    ex.Rutas1.Add(r.id);
                    ex.Rutas2.Add(r.Realizada);
                }

                List<Ruta> listadoRutasDelExcursionista = rutasdelexcursionista(ex);
                dg_rutasExcursionistas.ItemsSource = listadoRutasDelExcursionista;
                dg_rutasExcursionistas.Items.Refresh();
                MessageBox.Show("Datos guardados con exito", "Guardar datos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void bt_cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (lstExcursionistas.SelectedIndex != -1)
            {
                Excursionista ex = (Excursionista)listadoexcursionistas[lstExcursionistas.SelectedIndex];
                txt_nombre.Text = ex.Nombre;
                txt_apellidos.Text = ex.Apellido;
                txt_edad.Text = ex.Edad.ToString();
                Comprobar3Campos(txt_nombre, txt_apellidos, txt_edad, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
                imgExcursionista.Source = new BitmapImage(ex.Caratula);
                cb_correos.Items.Clear();
                for (int i=0; i<ex.Correo.Count; i++)
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

        private void bt_eliminar_Click(object sender, RoutedEventArgs e)
        {
            if (lstExcursionistas.SelectedIndex != -1)
            {
                border_eliminarExcursioonista.Visibility = Visibility.Visible;
                Grid_menu.IsEnabled = false;
                border_datos.IsEnabled = false;
                bd_opcionesdatos.IsEnabled = false;
                SystemSounds.Beep.Play();
            }
        }

        private void bt_añadirCorreo_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirCorreo.Visibility = Visibility.Visible;
            txt_anadircorreo.Focus();
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
            if (lstExcursionistas.SelectedIndex !=-1)
            {
                Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
                cb_correos.Items.Add(txt_anadircorreo.Text);
            }
            txt_anadircorreo.Text = "";
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_AñadirCorreo.Visibility = Visibility.Hidden;

        }

        private void bt_añadirTelefono_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_AñadirTlf.Visibility = Visibility.Visible;
            txt_anadirtlf.Focus();

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
            if (lstExcursionistas.SelectedIndex != -1)
            {
                Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
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

        private void btn_EditarRuta_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_anadirruta.Visibility = Visibility.Visible;
            List<Ruta> listadoRutasSeleccionadas = null;
            List<Ruta> listadoRutasNoSeleccionadas = CargarRutasXML();
            if (lstExcursionistas.SelectedIndex != -1)
            {
                Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
                listadoRutasSeleccionadas = rutasdelexcursionista(ex);
                for (int w=0; w<listadoRutasNoSeleccionadas.Count; w++)
                {
                    Ruta r = (Ruta)listadoRutasNoSeleccionadas[w];
                    r.Seleccionada = false;
                }
                for (int z = 0; z<listadoRutasSeleccionadas.Count; z++)
                {
                    Ruta r1 = listadoRutasSeleccionadas[z];
                    Ruta r2 = listadoRutasNoSeleccionadas[(int)r1.id];
                    r2.Seleccionada = true;

                }
                for (int i = 0; i < ex.Rutas1.Count; i++)
                {
                    int valor = Convert.ToInt32(ex.Rutas1[i]);
                    Ruta r = listadoRutasNoSeleccionadas[valor];
                    //r.Seleccionada = true;
                    r.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                }

            }
            dg_anadirruta.ItemsSource = null;
            dg_anadirruta.ItemsSource = listadoRutasNoSeleccionadas;
            dg_seleccionar.ItemsSource = listadoRutasNoSeleccionadas;
            col_seleccionar.Width = 120;
            }

        private void btn_cancelarRuta_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_anadirruta.Visibility = Visibility.Hidden;
            dg_anadirruta.ItemsSource = null;
            dg_seleccionar.ItemsSource = null;
        }
        private void btn_anadirRuta_Click(object sender, RoutedEventArgs e)
        {
            int contador = 0;
            Excursionista ex=null;
            if (lstExcursionistas.SelectedIndex != -1)
            {
                ex = (Excursionista)lstExcursionistas.SelectedItem;
                
            }
            List<Ruta> rutasdelExcursionista = rutasdelexcursionista(ex);
            for (int i=0; i<dg_anadirruta.Items.Count; i++)
            {
                Ruta r = (Ruta)dg_anadirruta.Items[i];
                Ruta r2 = (Ruta)dg_seleccionar.Items[i];
                if (r2.Seleccionada == true)
                {
                    if (NoCopiarMismasRutas(rutasdelExcursionista, r) == false)
                    {
                        ex.Rutas1.Add(r.id);
                        ex.Rutas2.Add(r.Realizada);
                        rutasdelExcursionista.Add(r);
                        contador += 1;
                    }
                }
                else
                {
                    for (int j=0; j<rutasdelExcursionista.Count; j++)
                    {
                        Ruta r_eliminar = (Ruta)rutasdelExcursionista[j];
                        if (r_eliminar.id == r2.id)
                        {
                            rutasdelExcursionista.RemoveAt(j);
                            ex.Rutas1.RemoveAt(j);
                            ex.Rutas2.RemoveAt(j);
                        }

                    }
                }
            }
            dg_rutasExcursionistas.ItemsSource = null;
            dg_seleccionar.ItemsSource = null;
            dg_rutasExcursionistas.ItemsSource = rutasdelExcursionista;
            dg_rutasExcursionistas.Items.Refresh();
            dg_anadirruta.ItemsSource = null;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_anadirruta.Visibility = Visibility.Hidden;
        }
        private Boolean NoCopiarMismasRutas(List<Ruta> listado, Ruta r)
        {
            Boolean EnListado = false;
            for (int i =0; i< listado.Count && EnListado==false; i++)
            {
                if (r.id == listado[i].id)
                {
                    EnListado = true;
                }
            }

            return EnListado;

        }
        private List<Ruta> rutasdelexcursionista(Excursionista ex)
        {
            List<Ruta> listadoRutasDelExcursionista = new List<Ruta>();
            for (int i = 0; i < ex.Rutas1.Count; i++)
            {
                int valor = Convert.ToInt32(ex.Rutas1[i]);
                Ruta ruta = listadoRutas.ElementAt(valor);
                ruta.Realizada = Convert.ToBoolean(ex.Rutas2[i]);
                listadoRutasDelExcursionista.Add(ruta);
            }
            return listadoRutasDelExcursionista;
        }

        private void btn_añadirexcursionista_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_anadirExcursionista_1.Visibility = Visibility.Visible;
            dg_anadirruta_NuevoUsuario.ItemsSource = CargarRutasXML();
            dg_NU_seleccionar.ItemsSource = CargarRutasXML();

        }
        private void btn_cancelarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {

            border_NU_Cancelar.Visibility = Visibility.Visible;
            SystemSounds.Beep.Play();
        }

        private void btn_siguiente_Click(object sender, RoutedEventArgs e)
        {
            if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            {
                border_anadirExcursionista_2.Visibility = Visibility.Visible;
                border_anadirExcursionista_1.Visibility = Visibility.Hidden;
            }

        }

        private void bt_Anadir_añadirCorreo_Click(object sender, RoutedEventArgs e)
        {
            border_anadirExcursionista_1.IsEnabled = false;
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Visible;
        }

        private void bt_Anadir_añadirTelefono_Click(object sender, RoutedEventArgs e)
        {
            border_anadirExcursionista_1.IsEnabled = false;
            border_NuevoUsuario_AñadirTlf.Visibility = Visibility.Visible;
        }

        private void btn_Anadir_CargarImagen_Click(object sender, RoutedEventArgs e)
        {
            CargarImagen(img_Anadir_Excursionista);
        }

        private void btn_anterior_Click(object sender, RoutedEventArgs e)
        {
            border_anadirExcursionista_1.Visibility = Visibility.Visible;
            border_anadirExcursionista_2.Visibility = Visibility.Hidden;
        }

        private void btn_guardarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            Boolean Guardado_correcto = true;
            Excursionista ex = new Excursionista("", "", 0, null);
            int id = lstExcursionistas.Items.Count;
            ex.id = id+1;
            ex.Nombre = txt_Anadir_nombre.Text;
            ex.Apellido = txt_Anadir_apellidos.Text;
            lstExcursionistas.ItemsSource = null;
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
            if (img_Anadir_Excursionista.Source != null)
            {
                ex.Caratula = new Uri(img_Anadir_Excursionista.Source.ToString());

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
                listadoexcursionistas.Add(ex);
                lstExcursionistas.ItemsSource = listadoexcursionistas;
                lstExcursionistas.Items.Refresh();
                btn_NU_Salir_Click(sender, e);
            }
        }

        private void btn_NU_cancelarcorreo_Click(object sender, RoutedEventArgs e)
        {
            border_anadirExcursionista_1.IsEnabled = true;
            txt_NU_anadircorreo.Text = "";
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_NU_anadircorreo_Click(object sender, RoutedEventArgs e)
        {
            cb_Anadir_correos.Items.Add(txt_NU_anadircorreo.Text);
            txt_NU_anadircorreo.Text = "";
            border_anadirExcursionista_1.IsEnabled = true;
            border_NuevoUsuario_AñadirCorreo.Visibility = Visibility.Hidden;
        }

        private void btn_NU_cancelartlf_Click(object sender, RoutedEventArgs e)
        {
            border_anadirExcursionista_1.IsEnabled = true;
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
            border_anadirExcursionista_1.IsEnabled = true;
            border_NuevoUsuario_AñadirTlf.Visibility = Visibility.Hidden;
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
            else if (edad.Text == String.Empty) { 
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

        private void btn_eliminarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            Excursionista ex = (Excursionista)lstExcursionistas.SelectedItem;
            listadoexcursionistas.Remove(ex);
            lstExcursionistas.ItemsSource = null;
            lstExcursionistas.ItemsSource = listadoexcursionistas;
            lstExcursionistas.Items.Refresh();
            lstExcursionistas.SelectedIndex = -1;
            tbDatos.Visibility = Visibility.Hidden;
            bd_opcionesdatos.Visibility = Visibility.Hidden;
            grid_NoSeleccionado.Visibility = Visibility.Visible;
            border_eliminarExcursioonista.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }

        private void btn_cancelarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            border_eliminarExcursioonista.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }

        private void btn_NU_Salir_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Hidden;
            Color color_default = Color.FromArgb((byte)100, (byte)179, (byte)172, (byte)171);
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
            border_anadirExcursionista_1.Visibility = Visibility.Hidden;
            border_anadirExcursionista_2.Visibility = Visibility.Hidden;
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
            img_Anadir_Excursionista.Source = null;
            dg_anadirruta_NuevoUsuario.ItemsSource = null;
        }

        private void btn_NU_Permanecer_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Hidden;
        }
        private void ClickDerecho_cb_correos(object sender, MouseButtonEventArgs controlUnderMouse)
        {
            border_anadirruta.Visibility = Visibility.Visible;
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

        private void bt_eliminartlf_Click(object sender, RoutedEventArgs e)
        {
            if (cb_telefonos.SelectedIndex != -1)
            {
                cb_telefonos.Items.RemoveAt(cb_telefonos.SelectedIndex);
                cb_telefonos.Items.Refresh();
                cb_telefonos.SelectedIndex = 0;
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

        private void btn_configuracion_Click(object sender, RoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion(usuario);
            configuracion.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            configuracion.Show();
            this.Close();
        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayuda = new Ayuda(usuario);
            ayuda.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ayuda.Show();
            this.Close();
        }
    }

}