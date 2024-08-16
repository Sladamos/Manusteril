using Emergency.Bus;
using Emergency.Messages;
using MassTransit;
using Ninject;
using Ninject.Modules;
using System.Reflection;
using System.Reflection.Emit;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Emergency
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
            var kernel = new StandardKernel();
            kernel.Load<Bindings>();
            //TODO create error handling layer
            //TODO create GUI
            IBusOperator busOperator = kernel.Get<IBusOperator>();
            IBusInstance busInstance = busOperator.CreateBusInstance();
            await busInstance.Start();
            await busInstance.Publish(new PatientUnregisteredMessage() { patientId = Guid.NewGuid() });
        }
    }
}
