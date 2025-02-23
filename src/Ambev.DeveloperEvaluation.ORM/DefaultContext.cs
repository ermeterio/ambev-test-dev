using Ambev.DeveloperEvaluation.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Ambev.DeveloperEvaluation.ORM;

public class DefaultContext(DbContextOptions<DefaultContext> options) : DbContext(options)
{
    public DbSet<User>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
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

        return new DefaultContext(builder.Options);
    }
}