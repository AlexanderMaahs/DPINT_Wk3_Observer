using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public abstract class Observable<T> : IObservable<T>, IDisposable
    {
        private List<IObserver<T>> _observers;

        public Observable()
        {
            _observers = new List<IObserver<T>>();
        }

        /// <summary>
        /// Deze private class gebruiken we om terug te geven bij de Subscribe methode.
        /// </summary>
        private struct Unsubscriber : IDisposable
        {
            private Action _unsubscribe;
            public Unsubscriber(Action unsubscribe) { _unsubscribe = unsubscribe; }
            public void Dispose() { _unsubscribe(); }
        }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            // We moeten bijhouden wie ons in de gaten houdt.
            // Stop de observer dus in de lijst met observers. We weten dan welke objecten we allemaal een seintje moeten geven.
            _observers.Add(observer);


            // Daarna geven we een object terug.
            // Als dat object gedisposed wordt geven wij
            // de bovenstaande observer geen seintjes meer.
            return new Unsubscriber(() => _observers.Remove(observer));
        }

        /// <summary>
        /// Deze methode kunnen we aanroepen vanuit onze subklasses.
        /// Hier geven we dan een seintje aan al onze observers dat we veranderd zijn.
        /// </summary>
        /// <param name="subject">Dat is de "this" van onze subklasses</param>
        protected void Notify(T subject)
        {
            // Hier moeten we iedere observer die ons in de gaten houdt een seintje geven dat we een nieuwe waarde hebben. 
            // We roepen dus hun OnNext methode aan.
            foreach (IObserver<T> observer in _observers)
                observer.OnNext(subject);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
