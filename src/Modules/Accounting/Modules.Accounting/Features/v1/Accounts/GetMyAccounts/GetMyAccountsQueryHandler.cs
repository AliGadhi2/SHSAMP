using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts.Dtos;
using FSH.Modules.Accounting.Contracts.v1.Accounts;
using FSH.Modules.Accounting.Data;
using Mediator;
using Microsoft.EntityFrameworkCore;

namespace FSH.Modules.Accounting.Features.v1.Accounts.GetMyAccounts;

public sealed class GetMyAccountsQueryHandler(
    AccountingDbContext dbContext)
    : IQueryHandler<GetMyAccountsQuery, PagedResponse<AccountDto>>
{
    public async ValueTask<PagedResponse<AccountDto>> Handle(GetMyAccountsQuery query, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(query);

        var q = dbContext.Accounts.AsNoTracking()
            ;
        if (query.status is not null)
        {
            q = q.Where(i => i.Status == query.status);
        }
        if (query.AccountFather is not null)
        {
            q = q.Where(i => i.AccountFather == query.AccountFather);
        }
        if (query.AccountType is not null)
        {
            q = q.Where(i => i.AccountType == query.AccountType);
        }

        var total = await q.LongCountAsync(cancellationToken).ConfigureAwait(false);
        var invoices = await q
            .OrderByDescending(i => i.CreatedAtUtc)
            .Skip((query.PageNumber - 1) * query.PageSize)
            .Take(query.PageSize)
            .ToListAsync(cancellationToken).ConfigureAwait(false);

        return new PagedResponse<AccountDto>
        {
            Items = invoices.Select(i => i.ToDto()).ToList(),
            PageNumber = query.PageNumber,
            PageSize = query.PageSize,
            TotalCount = total,
            TotalPages = (int)Math.Ceiling(total / (double)query.PageSize)
        };
    }
}
