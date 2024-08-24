using Emergency.Bus;
using Emergency.Command;
using Emergency.Messages;
using Emergency.Middleware;
using MassTransit;
using Ninject;
using Ninject.Modules;

namespace Emergency
{
    internal class Program
    {

        static void Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<Bindings>();
            IMenu menu = kernel.Get<IMenu>();
            MiddlewaresWrapper middlewares = kernel.Get<MiddlewaresWrapper>();
            middlewares.execute(menu.Start);
        }
    }
}
