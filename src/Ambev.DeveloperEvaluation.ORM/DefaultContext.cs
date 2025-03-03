using Ambev.DeveloperEvaluation.Domain.Entities.Company;
using Ambev.DeveloperEvaluation.Domain.Entities.Product;
using Ambev.DeveloperEvaluation.Domain.Entities.Sale;
using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Ambev.DeveloperEvaluation.Domain.Events.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext(DbContextOptions<DefaultContext> options, IDomainEventDispatcher dispatcher) : DbContext(options)
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Sale>? Sales { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<Discount>? ProductDiscounts { get; set; }
    public DbSet<SaleDiscount>? SaleDiscounts { get; set; }
    public DbSet<Company>? Companies { get; set; }
    public DbSet<ProductHistory>? ProductHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var domainEntities = ChangeTracker.Entries<IEntityWithEvents>()
            .Where(x => x.Entity.DomainEvents.Any())
            .Select(x => x.Entity)
            .ToList();

        var domainEvents = domainEntities.SelectMany(x => x.DomainEvents).ToList();

        domainEntities.ForEach(x => x.ClearDomainEvents());

        var result = await base.SaveChangesAsync(cancellationToken);

        foreach (var domainEvent in domainEvents)
        {
            dispatcher.AddEvent(domainEvent);
        }

        await dispatcher.DispatchEventsAsync();

        return result;
    }
}
public class DefaultContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        var pathAssembly =
            $"{Directory.GetParent(Directory.GetCurrentDirectory())!.FullName}\\Ambev.DeveloperEvaluation.WebApi";

        var configuration = new ConfigurationBuilder()
            .SetBasePath(pathAssembly)
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder<DefaultContext>();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
               connectionString,
               b =>
               {
                   b.MigrationsAssembly(GetType().Assembly.GetName().Name);
                   b.EnableRetryOnFailure();
               }
        );

        return new DefaultContext(builder.Options, new DomainEventDispatcher());
    }
}