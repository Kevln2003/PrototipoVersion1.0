using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using PrototipoVersion1._0.Visual.Plantilla.Models;

namespace PrototipoVersion1._0.Visual.Plantilla.ViewModels
{
    public class FacturaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<FacturaItem> _items;
        private decimal _total;
        private DateTime _fechaFactura;

        public FacturaViewModel()
        {
            Items = new ObservableCollection<FacturaItem>();
            FechaFactura = DateTime.Now;
            AgregarItemCommand = new RelayCommand(AgregarItem);
            GenerarFacturaCommand = new RelayCommand(GenerarFactura);

            // Agregar primer item por defecto
            AgregarItem();
        }

        public ObservableCollection<FacturaItem> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaFactura
        {
            get => _fechaFactura;
            set
            {
                _fechaFactura = value;
                OnPropertyChanged();
            }
        }

        public ICommand AgregarItemCommand { get; }
        public ICommand GenerarFacturaCommand { get; }

        private void AgregarItem()
        {
            var item = new FacturaItem();
            item.PropertyChanged += Item_PropertyChanged;
            Items.Add(item);
        }

        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FacturaItem.Costo))
            {
                CalcularTotal();
            }
        }

        private void CalcularTotal()
        {
            Total = Items.Sum(item => item.Costo);
        }

        private async void GenerarFactura()
        {
            // Aquí irá la lógica para generar la factura
            // Por ejemplo, guardar en base de datos, generar PDF, etc.
            var dialog = new Windows.UI.Xaml.Controls.ContentDialog
            {
                Title = "Factura Generada",
                Content = $"Factura generada con {Items.Count} items por un total de ${Total:F2}",
                CloseButtonText = "OK"
            };

            await dialog.ShowAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke() ?? true;

        public void Execute(object parameter) => _execute();

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
