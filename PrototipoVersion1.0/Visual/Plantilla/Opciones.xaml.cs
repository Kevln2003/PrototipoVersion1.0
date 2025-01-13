using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PrototipoVersion1._0.Visual.Deporte;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrototipoVersion1._0.Visual.Plantilla
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Plantilla : Page
    {
        public event EventHandler<string> NavigationRequested;

        public Plantilla()
        {
            this.InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Ingreso");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationRequested?.Invoke(this, "Factura");
        }

        private void Button_Click_2(object sender, RoutedEventArgs e) => NavigationRequested?.Invoke(this, "Receta");

        private void Button_Click_3(object sender, RoutedEventArgs e) => NavigationRequested?.Invoke(this, "Busqueda");
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var frame = Window.Current.Content as Frame;
            if (frame != null)
            {
                frame.Navigate(typeof(MainPage));
            }
        }

    }
}
