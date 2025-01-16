using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace PrototipoVersion1._0.Visual.Models
{
    public class ItemDeReceta : INotifyPropertyChanged
    {
        private string _rp = "";
        private string _indicaciones = "";
        private int _indexNumber;

        public int IndexNumber
        {
            get => _indexNumber;
            set
            {
                _indexNumber = value;
                OnPropertyChanged();
            }
        }

        public string Rp
        {
            get => _rp;
            set
            {
                _rp = value;
                OnPropertyChanged();
            }
        }

        public string Indicaciones
        {
            get => _indicaciones;
            set
            {
                _indicaciones = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}