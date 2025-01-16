using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml;
using PrototipoVersion1._0.Visual.Plantilla.Models;
using PrototipoVersion1._0.Visual.Models;
using Windows.UI.Xaml.Controls;

namespace PrototipoVersion1._0.Visual.ViewModels
{
    public class RecetaViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ItemDeReceta> _itemDeRecetas;
        private DateTime _fechaDeLaReceta;
        private string _diagnostico;

        public RecetaViewModel()
        {
            Rp = new ObservableCollection<ItemDeReceta>();
            FechaReceta = DateTime.Now;
            AgregarRpCommand = new RelayCommand(AgregarRp);
            EliminarRpCommand = new RelayCommand<ItemDeReceta>(EliminarRp);
            GenerarRecetaCommand = new RelayCommand(GenerarReceta, CanGenerarReceta);
        }

        public ObservableCollection<ItemDeReceta> Rp
        {
            get => _itemDeRecetas;
            set
            {
                _itemDeRecetas = value;
                OnPropertyChanged();
            }
        }

        public DateTime FechaReceta
        {
            get => _fechaDeLaReceta;
            set
            {
                _fechaDeLaReceta = value;
                OnPropertyChanged();
            }
        }

        public string Diagnostico
        {
            get => _diagnostico;
            set
            {
                _diagnostico = value;
                OnPropertyChanged();
                (GenerarRecetaCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public ICommand AgregarRpCommand { get; }
        public ICommand EliminarRpCommand { get; }
        public ICommand GenerarRecetaCommand { get; }

        private void AgregarRp()
        {
            var rp = new ItemDeReceta
            {
                IndexNumber = Rp.Count + 1
            };
            Rp.Add(rp);
            (GenerarRecetaCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        private void EliminarRp(ItemDeReceta item)
        {
            if (item != null)
            {
                Rp.Remove(item);
                ActualizarIndices();
                (GenerarRecetaCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        private void ActualizarIndices()
        {
            for (int i = 0; i < Rp.Count; i++)
            {
                Rp[i].IndexNumber = i + 1;
            }
        }

        private bool CanGenerarReceta()
        {
            return !string.IsNullOrWhiteSpace(Diagnostico) && Rp.Count > 0;
        }

        private async void GenerarReceta()
        {
            // Validar que haya contenido
            if (string.IsNullOrWhiteSpace(Diagnostico) || Rp.Count == 0)
            {
                var errorDialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Debe ingresar un diagnóstico y al menos un medicamento",
                    CloseButtonText = "OK"
                };
                await errorDialog.ShowAsync();
                return;
            }

            // Aquí irá la lógica para generar la receta
            var dialog = new ContentDialog
            {
                Title = "Receta Generada",
                Content = $"Receta generada con éxito\nFecha: {FechaReceta.ToShortDateString()}\nCantidad de medicamentos: {Rp.Count}",
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

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute, Func<T, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter) => _canExecute?.Invoke((T)parameter) ?? true;

        public void Execute(object parameter) => _execute((T)parameter);

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
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
