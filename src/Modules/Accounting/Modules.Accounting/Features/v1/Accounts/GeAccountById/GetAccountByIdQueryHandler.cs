using FSH.Framework.Core.Exceptions;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Data;

using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GetAccountById;

public sealed class GetAccountByIdQueryHandler(
    AccountingDbContext dbContext)
    : IQueryHandler<GetAccountByIdQuery, AccountDto>
{
    public async ValueTask<AccountDto> Handle(GetAccountByIdQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var account = await dbContext.Accounts.AsNoTracking()
             
            .FirstOrDefaultAsync(
                i => i.Id == query.accountId,
                cancellationToken)
            .ConfigureAwait(false)
            ?? throw new NotFoundException($"Account {query.accountId} not found.");

        return account.ToDto();
    }
}
