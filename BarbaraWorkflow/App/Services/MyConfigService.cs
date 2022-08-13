using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BarbaraWorkflow.App.Services
{
    public class MyConfigService
    {
        public MyConfigService()
        {
        }

        public static MyConfigService Singleton { get; } = initSingleton();

        private static MyConfigService initSingleton()
        {
            return new MyConfigService();
        }

        public IObservable<int> FontSizeSS = Observable.Zip(
            Observable.Interval(TimeSpan.FromSeconds(0.5)), Observable.Range(5, 50),
            (a, b) => b);
    }
}
