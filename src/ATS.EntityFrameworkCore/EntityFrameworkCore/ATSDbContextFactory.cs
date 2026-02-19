using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ATS.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class ATSDbContextFactory : IDesignTimeDbContextFactory<ATSDbContext>
{
    public ATSDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        ATSEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<ATSDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new ATSDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ATS.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
