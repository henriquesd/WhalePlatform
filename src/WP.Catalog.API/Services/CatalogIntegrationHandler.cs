using WP.Catalog.API.Models;
using WP.Catalog.API.Repositories;
using WP.Core.DomainObjects;
using WP.Core.Messages.Integration;
using WP.MessageBus;

namespace WP.Catalog.API.Services
{
   public class CatalogIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public CatalogIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<AuthorizedOrderIntegrationEvent>("AuthorizedOrder", async request =>
                await ReduceStock(request));
        }

        private async Task ReduceStock(AuthorizedOrderIntegrationEvent message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var productsWithStock = new List<Product>();
                var productRepository = scope.ServiceProvider.GetRequiredService<IProductRepository>();

                var idsProducts = string.Join(",", message.Items.Select(c => c.Key));
                var products = await productRepository.GetProductsById(idsProducts);

                if (products.Count != message.Items.Count)
                {
                    CancelOrderWithoutStock(message);
                    return;
                }

                foreach (var product in products)
                {
                    var productQuantity = message.Items.FirstOrDefault(p => p.Key == product.Id).Value;

                    if (product.IsAvailable(productQuantity))
                    {
                        product.RemoveStock(productQuantity);
                        productsWithStock.Add(product);
                    }
                }

                if (productsWithStock.Count != message.Items.Count)
                {
                    CancelOrderWithoutStock(message);
                    return;
                }

                foreach (var product in productsWithStock)
                {
                    productRepository.Update(product);
                }

                if (!await productRepository.UnitOfWork.Commit())
                {
                    throw new DomainException($"Problems updating stock for order {message.OrderId}");
                }

                var orderStockReduced = new OrderStockReducedIntegrationEvent(message.ClientId, message.OrderId);
                await _bus.PublishAsync(orderStockReduced);
            }
        }

        public async void CancelOrderWithoutStock(AuthorizedOrderIntegrationEvent message)
        {
            var orderCancelled = new OrderCancelledIntegrationEvent(message.ClientId, message.OrderId);
            await _bus.PublishAsync(orderCancelled);
        }
    }
}