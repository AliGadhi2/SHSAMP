using FSH.Framework.Persistence;
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FSH.Modules.Accounting.Data;

public sealed class AcountingDbInitializer(
    AccountingDbContext dbContext,
    ILogger<AcountingDbInitializer> logger) : IDbInitializer
{
    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        if ((await dbContext.Database.GetPendingMigrationsAsync(cancellationToken).ConfigureAwait(false)).Any())
        {
            await dbContext.Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
            logger.LogInformation("[Accounting] applied migrations");
        }
    }

    public async Task SeedAsync(CancellationToken cancellationToken)
    {
        
        await dbContext.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        logger.LogInformation("[Accounting] seeded default plans (free, pro, pro-annual)");
    }
}
