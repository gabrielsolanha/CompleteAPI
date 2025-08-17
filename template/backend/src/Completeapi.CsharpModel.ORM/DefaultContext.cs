using Completeapi.CsharpModel.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Completeapi.CsharpModel.ORM;

public class DefaultContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SaleItems { get; set; }

    public DefaultContext(DbContextOptions<DefaultContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}

public class YourDbContextFactory : IDesignTimeDbContextFactory<DefaultContext>
{
    public DefaultContext CreateDbContext(string[] args)
    {
        string basePath = Directory.GetCurrentDirectory();
        string devSettingsPath = Path.Combine(basePath, "appsettings.Development.json");
        IConfigurationBuilder builderConfig = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        if (File.Exists(devSettingsPath))
        {
            // Sobrepõe valores de appsettings.json com os do appsettings.Development.json
            builderConfig.AddJsonFile(
                "appsettings.Development.json",
                optional: true,
                reloadOnChange: true
            );
        }

        IConfigurationRoot configuration = builderConfig.Build();

        DbContextOptionsBuilder<DefaultContext> builder = new();
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        builder.UseNpgsql(
            connectionString,
            b => b.MigrationsAssembly("Completeapi.CsharpModel.WebApi")
        );

        return new DefaultContext(builder.Options);
    }
}
