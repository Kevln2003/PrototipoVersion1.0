using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PrototipoVersion1._0.Visual.Deporte;
using PrototipoVersion1._0.Visual.Plantilla;
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

namespace PrototipoVersion1._0.Visual.Pediatrico
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Pediatria : Page
    {
        public Pediatria()
        {
            this.InitializeComponent();
            SideMenu.NavigationRequested += OnNavigationRequested;
        }
        private void OnNavigationRequested(object sender, string pageType)
        {
            if (pageType == "Ingreso")
            {
                ContentFrame.Navigate(typeof(IngresoPediatrico));
            }
            if (pageType == "Factura")
            {
                ContentFrame.Navigate(typeof(Factura));
            }
            if (pageType == "Receta")
            {
                ContentFrame.Navigate(typeof(Receta));
            }
            if (pageType == "Busqueda")
            {
                ContentFrame.Navigate(typeof(Busqueda));
            }


        }
    }
}
