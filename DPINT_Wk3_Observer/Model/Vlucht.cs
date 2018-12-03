using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DPINT_Wk3_Observer.Model
{
    public class Vlucht : Observable<Vlucht>
    {
        private Timer _waitingTimer;

        private TimeSpan _timeWaiting;
        public TimeSpan TimeWaiting
        {
            get { return _timeWaiting; }
            set { _timeWaiting = value; Notify(this); }
        }

        private string _vertrokkenVanuit;
        public string VertrokkenVanuit
        {
            get { return _vertrokkenVanuit; }
            set { _vertrokkenVanuit = value; Notify(this); }
        }

        private int _aantalKoffers;
        public int AantalKoffers
        {
            get { return _aantalKoffers; }
            set { _aantalKoffers = value; Notify(this); }
        }
        public Vlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            _waitingTimer = new Timer();
            _waitingTimer.Interval = 1000;
            _waitingTimer.Tick += (sender, args) => TimeWaiting = TimeWaiting.Add(new TimeSpan(0, 0, 1));
            _waitingTimer.Start();
            VertrokkenVanuit = vertrokkenVanuit;
            AantalKoffers = aantalKoffers;
        }

        public void StopWaiting()
        {
            _waitingTimer.Stop();
            _waitingTimer.Dispose();
        }
    }
}
