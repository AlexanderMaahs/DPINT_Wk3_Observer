using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public class Aankomsthal : IObserver<Baggageband>
    {
        // TODO: Hier een ObservableCollection van maken, dan weten we wanneer er vluchten bij de wachtrij bij komen of afgaan.
        public ObservableCollection<Vlucht> WachtendeVluchten { get; private set; }
        public ObservableCollection<Baggageband> Baggagebanden { get; private set; }

        public Aankomsthal()
        {
            WachtendeVluchten = new ObservableCollection<Vlucht>();
            Baggagebanden = new ObservableCollection<Baggageband>();

            // Als baggageband Observable is, gaan we subscriben op band 1 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 1", 30));
            // Als baggageband Observable is, gaan we subscriben op band 2 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 2", 60));
            // Als baggageband Observable is, gaan we subscriben op band 3 zodat we updates binnenkrijgen.
            Baggagebanden.Add(new Baggageband("Band 3", 90));

            foreach (var band in Baggagebanden)
                band.Subscribe(this);
        }

        public void NieuweInkomendeVlucht(string vertrokkenVanuit, int aantalKoffers)
        {
            // Het proces moet automatisch gaan, dus als er lege banden zijn moet de vlucht niet in de wachtrij.
            // Dan moet de vlucht meteen naar die band.

            var vlucht = new Vlucht(vertrokkenVanuit, aantalKoffers);
            Baggageband legeBand = Baggagebanden.FirstOrDefault(b => b.AantalKoffers == 0);
            if (legeBand == null)   // Geen lege banden
                WachtendeVluchten.Add(vlucht);
            else                    // Er is een lege band
                legeBand.HandelNieuweVluchtAf(vlucht);

        }

        public void OnNext(Baggageband value)
        {
            if (value.AantalKoffers == 0 && WachtendeVluchten.Any())
            {
                Vlucht volgendeVlucht = WachtendeVluchten.FirstOrDefault();
                WachtendeVluchten.RemoveAt(0);
                value.HandelNieuweVluchtAf(volgendeVlucht);
                volgendeVlucht.StopWaiting();
            }
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
