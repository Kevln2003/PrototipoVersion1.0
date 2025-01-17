using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

namespace PrototipoVersion1._0.Visual.Deporte
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Ingreso : Page
    {
        private SomatocartaCreada somatocarta;
        public Ingreso()
        {
            this.InitializeComponent();
            // Asegúrate de que el contenedor XAML no sea nulo
            if (SomatocartaContainer != null)
            {
                somatocarta = new SomatocartaCreada(SomatocartaContainer); // Pasar el Grid al constructor
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("El contenedor somatocartaContainer no está inicializado.");
            }
        }

        private void Pliegues_TextChanged(object sender, TextChangedEventArgs e)
        {
            ActualizarSomatocarta();
        }

        private void ActualizarSomatocarta()
        {
            try
            {
                double endomorfia = CalcularEndomorfia();
                double mesomorfia = CalcularMesomorfia();
                double ectomorfia = CalcularEctomorfia();

                somatocarta.UpdateSomatotype(endomorfia, mesomorfia, ectomorfia);
            }
            catch (Exception ex)
            {
                // Puedes mostrar un mensaje de error si lo deseas
                System.Diagnostics.Debug.WriteLine($"Error al actualizar somatocarta: {ex.Message}");
            }
        }

        private double CalcularEndomorfia()
        {
            // Verificar si los TextBox tienen valores válidos
            if (double.TryParse(tricipitalTextBox.Text, out double tricipital) &&
                double.TryParse(subescapularTextBox.Text, out double subescapular) &&
                double.TryParse(suprailiacoTextBox.Text, out double suprailiaco))
            {
                // Fórmula para calcular endomorfia
                double suma = tricipital + subescapular + suprailiaco;
                return -0.7182 + (0.1451 * suma) - (0.00068 * suma * suma) + (0.0000014 * suma * suma * suma);
            }
            return 0.0;
        }

        private double CalcularMesomorfia()
        {
            // Aquí deberías implementar la fórmula real para mesomorfia
            // Esta es una implementación simplificada
            if (double.TryParse(musloTextBox.Text, out double muslo) &&
                double.TryParse(pantorrillaTextBox.Text, out double pantorrilla))
            {
                return (muslo + pantorrilla) / 20.0; // Ejemplo simplificado
            }
            return 0.0;
        }

        private double CalcularEctomorfia()
        {
            // Aquí deberías implementar la fórmula real para ectomorfia
            // Esta es una implementación simplificada
            if (double.TryParse(abdominalTextBox.Text, out double abdominal))
            {
                return abdominal / 30.0; // Ejemplo simplificado
            }
            return 0.0;
        }

    }
}
