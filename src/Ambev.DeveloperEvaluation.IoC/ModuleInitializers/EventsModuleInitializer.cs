using Ambev.DeveloperEvaluation.Domain.Events.Product;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers
{
    public static class EventsModuleInitializer
    {
        public static Task Initialize(IBus bus)
        {
            bus.Subscribe<CreateProductEvent>();
            bus.Subscribe<UpdateProductEvent>();

            return Task.CompletedTask;
        }
    }
}
