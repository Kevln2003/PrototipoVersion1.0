using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using PrototipoVersion1._0.Visual.Plantilla.ViewModels;
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
    public sealed partial class Factura : Page
    {
        public FacturaViewModel ViewModel { get; }

        public Factura()
        {
            this.InitializeComponent();
            ViewModel = new FacturaViewModel();
            this.DataContext = ViewModel;
        }
    }
}
