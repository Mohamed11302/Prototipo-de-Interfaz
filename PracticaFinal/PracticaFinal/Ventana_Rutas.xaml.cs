using DatosExcursionista;
using DatosGuia;
using DatosRutas;
using DatosPuntosInteres;
using DatosUsuario;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Ventana_Rutas.xaml
    /// </summary>
    public partial class Ventana_Rutas : Window
    {
        List<Ruta> listadoRutas;
        public Usuario usuario;
        List<Uri> listadoImagenes_pi = new List<Uri>();
        int imagenmostrada = 0;
        public Ventana_Rutas(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
            listadoRutas = new List<Ruta>();
            listadoRutas = CargarRutasXML();
            lstRutas.ItemsSource = listadoRutas;
            cb_dificultad.Items.Add("Facil");
            cb_dificultad.Items.Add("Medio");
            cb_dificultad.Items.Add("Dificil");
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
                nuevaRuta.dificultad = node.ChildNodes[11].InnerText;
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

        private void btn_guias_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Guias ventana_guias = new Ventana_Guias(usuario);
            ventana_guias.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_guias.Show();
            this.Close();
        }

        private void btn_excursionistas_Click(object sender, RoutedEventArgs e)
        {
            Ventana_Excursionistas ventana_excursionistas = new Ventana_Excursionistas(usuario);
            ventana_excursionistas.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ventana_excursionistas.Show();
            this.Close();
        }

        private void btn_Ayuda_Click(object sender, RoutedEventArgs e)
        {
            Ayuda ayuda = new Ayuda(usuario);
            ayuda.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ayuda.Show();
            this.Close();
        }

        private void btn_configuracion_Click(object sender, RoutedEventArgs e)
        {
            Configuracion configuracion = new Configuracion(usuario);
            configuracion.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            configuracion.Show();
            this.Close();
        }

        private void btn_cerrarsesion_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            this.Visibility = Visibility.Hidden;
            m.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            m.Show();
        }

        private void lstRutas_SelectionNull(object sender, MouseButtonEventArgs controlUnderMouse)
        {
            if (controlUnderMouse.GetType() != typeof(ListBoxItem))
            {
                lstRutas.SelectedItem = null;
                //cb_correo.Items.Clear();
                //cb_telefonos.Items.Clear();
                tbDatos.Visibility = Visibility.Hidden;
                bd_opcionesdatos.Visibility = Visibility.Hidden;
                grid_NoSeleccionado.Visibility = Visibility.Visible;
            }
        }

        private void lstRutas_SelectionChanged2(object sender, RoutedEventArgs e)
        {
            if (lstRutas.SelectedIndex != -1)
            {
                tbDatos.Visibility = Visibility.Visible;
                bd_opcionesdatos.Visibility = Visibility.Visible;
                grid_NoSeleccionado.Visibility = Visibility.Hidden;

                Ruta ex = (Ruta)lstRutas.Items[lstRutas.SelectedIndex];
                txt_nombre.Text = ex.Nombre;
                txt_origen.Text = ex.Origen;
                txt_destino.Text = ex.Destino;
                txt_distancia.Text = ex.Distancia.ToString();
                txt_altitud.Text = ex.Altitud.ToString();
                //Comprobar3Campos(txt_nombre, txt_origen, txt_destino, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
                txt_maxParticipantes.Text = ex.maxParticipantes.ToString();
                txt_fecha.Text = ex.fecha.ToString();
                txt_hora.Text = ex.hora.ToString();
                txt_duracion.Text = ex.duracion.ToString() + " min";
                cb_dificultad.SelectedItem = ex.dificultad;
                lstPuntosInteres.SelectedIndex = -1;
                img_PuntoInteres.Source = null;
                TabRutas.Focus();
                tbImagenes.SelectedIndex = 0;
                txt_nombre_pi.Text = "";
                txt_descripcion_pi.Text = "";
                txt_tipologia_pi.Text = "";
                imagenmostrada = 0;
                MostrarExcursionistas(ex);
                MostrarGuia(ex);
                MostrarPuntosDeInteres(ex);
            }
            else
            {
                lstRutas.SelectedItem = null;
            }
        }
        private void MostrarPuntosDeInteres(Ruta ruta)
        {
            List<PuntoInteres> puntosinteres = CargarPuntosDeInteresXML();
            List<PuntoInteres> listadodepuntos = new List<PuntoInteres>();
            lstPuntosInteres.ItemsSource = null;
            for (int i=0; i<puntosinteres.Count(); i++)
            {
                if (ruta.id == puntosinteres[i].Ruta)
                {
                    listadodepuntos.Add(puntosinteres[i]);
                }
            }
            lstPuntosInteres.ItemsSource = listadodepuntos;
        }


        private List<PuntoInteres> CargarPuntosDeInteresXML()
        {
            List<PuntoInteres> listado = new List<PuntoInteres>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("Datos/PuntoInteres.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            XmlNodeList nodes = doc.SelectNodes("PuntosInteres/PuntoInteres");
            foreach (XmlNode node in nodes)
            {
                var nuevoPuntoInteres = new PuntoInteres();
                nuevoPuntoInteres.Id = (Convert.ToInt32(node.Attributes[0].Value));
                nuevoPuntoInteres.Nombre = node.ChildNodes[0].InnerText;
                nuevoPuntoInteres.Descripcion = node.ChildNodes[1].InnerText;
                nuevoPuntoInteres.Tipologia = node.ChildNodes[2].InnerText;
                nuevoPuntoInteres.Ruta = (Convert.ToInt32(node.ChildNodes[4].InnerText));

                string[] imagenes = node.ChildNodes[3].InnerText.Split(',');
                for (int i = 0; i < imagenes.Length; i++)
                {
                    nuevoPuntoInteres.Imagenes.Add(new Uri(imagenes[i].ToString(), UriKind.Relative));
                }
                listado.Add(nuevoPuntoInteres);
            }
            return listado;
        }
        private void MostrarGuia(Ruta ruta)
        {
            List<Guia> guias = CargarGuiasXML();
            for (int i = 0; i < guias.Count; i++)
            {
                
                if (guias[i].id.ToString() == ruta.Guia.ToString())
                {
                    txt_nombre_guia.Text = guias[i].Nombre.ToString();
                    txt_apellidos_guia.Text = guias[i].Apellido.ToString();
                    txt_edad_guia.Text = guias[i].Edad.ToString();
                    cb_correos_guia.SelectedIndex = 0;
                    if (guias[i].Caratula != null)
                    {
                        imgGuia.Source = new BitmapImage(guias[i].Caratula);
                    }
                    else
                        imgGuia.Source = null;

                    cb_correos_guia.Items.Clear();
                    cb_telefonos_guia.Items.Clear();
                    cb_correos_guia.SelectedIndex = 0;
                    cb_telefonos_guia.SelectedIndex = 0;
                    for (int j = 0; j < guias[i].Correo.Count; j++)
                    {
                        cb_correos_guia.Items.Add(guias[i].Correo[j]);
                    }
                    for (int j = 0; j < guias[i].Telefono.Count; j++)
                    {
                        cb_telefonos_guia.Items.Add(guias[i].Telefono[j]);
                    }
                }
            }

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
                for (int i = 0; i < correos.Length; i++)
                {
                    nuevoGuia.Correo.Add(correos[i]);
                }
                string[] idiomas = node.ChildNodes[5].InnerText.Split(',');
                for (int i = 0; i < idiomas.Length; i++)
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

        private void MostrarExcursionistas(Ruta ruta)
        {
            List<Excursionista> excursionistas = CargarExcursionistasXML();
            dg_rutas_Excursionistas.Items.Clear();
            for (int i=0; i<excursionistas.Count(); i++)
            {
                for (int j = 0; j < excursionistas[i].Rutas1.Count; j++)
                {
                    if (ruta.id == Convert.ToInt32(excursionistas[i].Rutas1[j]))
                    {
                        dg_rutas_Excursionistas.Items.Add(new
                        {
                            Nombre = excursionistas[i].Nombre,
                            Apellido = excursionistas[i].Apellido,
                            Edad = excursionistas[i].Edad,
                            Telefono = excursionistas[i].Telefono[0],
                            Correo = excursionistas[i].Correo[0]
                        });
                    }
                }

            }
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
                string[] telefonos = node.ChildNodes[3].InnerText.Split(',');
                for (int i = 0; i < telefonos.Length; i++)
                {
                    nuevoExcursionista.Telefono.Add(Convert.ToInt32(telefonos[i]));
                }
                string[] correos = node.ChildNodes[4].InnerText.Split(',');
                for (int i = 0; i < correos.Length; i++)
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
        private void btn_añadirRuta_Click(object sender, RoutedEventArgs e)
        {
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            border_anadirRuta.Visibility = Visibility.Visible;
            //dg_anadirruta_NuevoUsuario.ItemsSource = CargarRutasXML();
            //dg_NU_seleccionar.ItemsSource = CargarRutasXML();
        }

        private void btn_cancelarNuevoUsuario_Click(object sender, RoutedEventArgs e)
        {
            border_NU_Cancelar.Visibility = Visibility.Visible;
            SystemSounds.Beep.Play();
        }

        private void btn_siguiente_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
                border_anadirRuta_2.Visibility = Visibility.Visible;
                border_anadirRuta.Visibility = Visibility.Hidden;
           // }
        }

        private void btn_anterior_2_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
            border_anadirRuta_2.Visibility = Visibility.Hidden;
            border_anadirRuta.Visibility = Visibility.Visible;
            // }
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
            border_anadirRuta.Visibility = Visibility.Hidden;
            border_anadirRuta_2.Visibility = Visibility.Hidden;
            cb_Anadir_dificultad.Items.Clear();
            txt_anadir_nombre.Text = "";
            lbl_NU_ErrorDatos_Nombre.Visibility = Visibility.Hidden;
            txt_anadir_nombre.BorderBrush = new SolidColorBrush(color_default);
            lbl_NU_ErrorDatos_Edad.Visibility = Visibility.Hidden;
            //txt_Anadir_edad.BorderBrush = new SolidColorBrush(color_default);
            //txt_Anadir_apellidos.Text = "";
            lbl_NU_ErrorDatos_Apellidos.Visibility = Visibility.Hidden;
            //txt_Anadir_apellidos.BorderBrush = new SolidColorBrush(color_default);
            //img_Anadir_Excursionista.Source = null;
            //dg_anadirruta_NuevoUsuario.ItemsSource = null;
        }

        private void btn_anterior_3_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
            border_anadirRuta_3.Visibility = Visibility.Hidden;
            border_anadirRuta_2.Visibility = Visibility.Visible;
            // }
        }

        private void btn_siguiente_2_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
            border_anadirRuta_3.Visibility = Visibility.Visible;
            border_anadirRuta_2.Visibility = Visibility.Hidden;
            // }
        }

        private void btn_anterior_4_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
            border_anadirRuta_4.Visibility = Visibility.Hidden;
            border_anadirRuta_3.Visibility = Visibility.Visible;
            // }
        }

        private void btn_siguiente_3_Click(object sender, RoutedEventArgs e)
        {
            //if (Comprobar3Campos(txt_Anadir_nombre, txt_Anadir_apellidos, txt_Anadir_edad, lbl_NU_ErrorDatos_Nombre, lbl_NU_ErrorDatos_Apellidos, lbl_NU_ErrorDatos_Edad))
            //{
            border_anadirRuta_4.Visibility = Visibility.Visible;
            border_anadirRuta_3.Visibility = Visibility.Hidden;
            // }
        }
        private void TabCambio(object sender, RoutedEventArgs e)
        {
            if (tbDatos.SelectedIndex == 2)
            {
                grid_datosGuia.Visibility = Visibility.Visible;
            }
            else
            {
                if (grid_datosGuia != null)
                    grid_datosGuia.Visibility = Visibility.Hidden;
            }
           
        }

        private void lstPuntosInteres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstPuntosInteres.SelectedIndex!= -1)
            {
                PuntoInteres pi = (PuntoInteres)lstPuntosInteres.SelectedItem;
                txt_nombre_pi.Text = pi.Nombre;
                txt_tipologia_pi.Text = pi.Tipologia;
                txt_descripcion_pi.Text= pi.Descripcion;
                listadoImagenes_pi = pi.Imagenes;
                imagenmostrada = 0;
                img_PuntoInteres.Source = new BitmapImage(listadoImagenes_pi[imagenmostrada]);
                if (pi.Imagenes.Count == 1)
                {
                    btn_siguienteFoto.Visibility = Visibility.Hidden;
                }
                else{
                    btn_siguienteFoto.Visibility = Visibility.Visible;
                }
                btn_anteriorFoto.Visibility = Visibility.Hidden;
            }
        }

        private void btn_anteriorFoto_Click(object sender, RoutedEventArgs e)
        {
            if (imagenmostrada - 1 >= 0)
            {
                btn_siguienteFoto.Visibility = Visibility.Visible;
                imagenmostrada = imagenmostrada - 1;
                img_PuntoInteres.Source = new BitmapImage(listadoImagenes_pi[imagenmostrada]);
            }
            if (imagenmostrada == 0)
            {
                btn_anteriorFoto.Visibility = Visibility.Hidden;
            }
        }

        private void btn_siguienteFoto_Click(object sender, RoutedEventArgs e)
        {
            
            if (imagenmostrada + 1 <=listadoImagenes_pi.Count())
            {
                btn_anteriorFoto.Visibility = Visibility.Visible;
                imagenmostrada = imagenmostrada + 1;
                img_PuntoInteres.Source = new BitmapImage(listadoImagenes_pi[imagenmostrada]);
            }
            if (imagenmostrada == listadoImagenes_pi.Count()-1)
            {
                btn_siguienteFoto.Visibility = Visibility.Hidden;
            }
        }

        private void bt_cancelar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRutas.SelectedIndex != -1)
            {
                Ruta ex = (Ruta)lstRutas.SelectedItem;
                txt_nombre.Text = ex.Nombre;
                txt_origen.Text = ex.Origen;
                txt_destino.Text = ex.Destino;
                txt_distancia.Text = ex.Distancia.ToString();
                txt_altitud.Text = ex.Altitud.ToString();
                //Comprobar3Campos(txt_nombre, txt_origen, txt_destino, lbl_ErrorNombre, lbl_ErrorApellido, lbl_ErrorEdad);
                txt_maxParticipantes.Text = ex.maxParticipantes.ToString();
                txt_fecha.Text = ex.fecha.ToString();
                txt_hora.Text = ex.hora.ToString();
                txt_duracion.Text = ex.duracion.ToString() + " min";
                cb_dificultad.SelectedItem = ex.dificultad;
                lstPuntosInteres.SelectedIndex = -1;
                img_PuntoInteres.Source = null;
                TabRutas.Focus();
                tbImagenes.SelectedIndex = 0;
                txt_nombre_pi.Text = "";
                txt_descripcion_pi.Text = "";
                txt_tipologia_pi.Text = "";
                imagenmostrada = 0;
                MostrarExcursionistas(ex);
                MostrarGuia(ex);
                MostrarPuntosDeInteres(ex);
            }
        }

        private void bt_eliminar_Click(object sender, RoutedEventArgs e)
        {
            border_eliminarRuta.Visibility = Visibility.Visible;
            Grid_menu.IsEnabled = false;
            border_datos.IsEnabled = false;
            bd_opcionesdatos.IsEnabled = false;
            SystemSounds.Beep.Play();
        }

        private void bt_guardar_Click(object sender, RoutedEventArgs e)
        {
            if (lstRutas.SelectedIndex != -1)
            {
                Ruta ex = (Ruta)lstRutas.SelectedItem;
                ex.Nombre = txt_nombre.Text;
                ex.Origen = txt_origen.Text;
                ex.Destino = txt_destino.Text;
                ex.Altitud = Convert.ToInt32(txt_altitud.Text);
                ex.Distancia = Convert.ToInt32(txt_distancia.Text);
                ex.maxParticipantes = Convert.ToInt32(txt_maxParticipantes.Text);
                //ex.fecha = txt_fecha.Text;
                ex.hora = txt_hora.Text;
                
                ex.duracion = Convert.ToInt32(txt_duracion.Text.Substring(0, txt_duracion.Text.Length-4));
                ex.dificultad = cb_dificultad.SelectedItem.ToString();
                listadoRutas[lstRutas.SelectedIndex] = ex;
                lstRutas.ItemsSource = listadoRutas;
                lstRutas.Items.Refresh();


                MessageBox.Show("Datos guardados con exito", "Guardar datos", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btn_cancelarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            border_eliminarRuta.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }

        private void btn_eliminarDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            Ruta ex = (Ruta)lstRutas.SelectedItem;
            listadoRutas.Remove(ex);
            lstRutas.ItemsSource = null;
            lstRutas.ItemsSource = listadoRutas;
            lstRutas.Items.Refresh();
            lstRutas.SelectedIndex = -1;
            tbDatos.Visibility = Visibility.Hidden;
            bd_opcionesdatos.Visibility = Visibility.Hidden;
            grid_NoSeleccionado.Visibility = Visibility.Visible;
            border_eliminarRuta.Visibility = Visibility.Hidden;
            Grid_menu.IsEnabled = true;
            border_datos.IsEnabled = true;
            bd_opcionesdatos.IsEnabled = true;
        }
    }
}
