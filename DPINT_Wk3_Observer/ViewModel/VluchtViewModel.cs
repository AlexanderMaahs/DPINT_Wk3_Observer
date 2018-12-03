using DPINT_Wk3_Observer.Model;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.ViewModel
{
    public class VluchtViewModel : ViewModelBase, IObserver<Vlucht>
    {
        private string _vertrokkenVanuit;
        public string VertrokkenVanuit
        {
            get { return _vertrokkenVanuit; }
            set { _vertrokkenVanuit = value; RaisePropertyChanged("VertrokkenVanuit"); }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; RaisePropertyChanged("AantalKoffers"); }
        }

        private string _wachtTijd;
        public string WachtTijd
        {
            get { return _wachtTijd; }
            private set { _wachtTijd = value; RaisePropertyChanged("WachtTijd"); }
        }

        public VluchtViewModel(Vlucht vlucht)
        {
            vlucht.Subscribe(this);
            OnNext(vlucht);
        }

        public void OnNext(Vlucht vlucht)
        {
            VertrokkenVanuit = vlucht.VertrokkenVanuit;
            AantalKoffers = vlucht.AantalKoffers;
            WachtTijd = vlucht.TimeWaiting.ToString();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }
    }
}
