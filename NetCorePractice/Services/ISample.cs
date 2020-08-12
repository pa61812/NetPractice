using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCorePractice.Services
{
    public interface ISample
    {
        int Id { get; }
    }
   public interface ISampleSingleton : ISample
    {
    }

    public interface ISampleTransient : ISample
    {
    }

    public interface ISampleScoped : ISample
    {
    }

 
    public class Sample : ISampleSingleton, ISampleTransient, ISampleScoped
    {
        private static int _counter;
        private int _id;

        public Sample()
        {
            _id = ++_counter;
        }

        public int Id => _id;
    }
}
