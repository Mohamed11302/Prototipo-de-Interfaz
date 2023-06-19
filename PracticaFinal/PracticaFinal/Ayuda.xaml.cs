using DatosUsuario;
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

namespace PracticaFinal
{
    /// <summary>
    /// Lógica de interacción para Ayuda.xaml
    /// </summary>
    public partial class Ayuda : Window
    {
        public Usuario usuario;
        public Ayuda(Usuario usuario)
        {
            InitializeComponent();
            this.usuario = usuario;
        }
        private void CerrarConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            Inicio inicio = new Inicio(usuario);
            inicio.Show();
            this.Close();
        }
    }
}
