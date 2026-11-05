using FSH.Framework.Shared.Persistence;
using FSH.Modules.Accounting.Contracts.Dtos;
using Mediator;

namespace FSH.Modules.Accounting.Contracts.v1.Currencies;

/// <summary>
/// Lists soft-deleted currencies. Bypasses the global IsDeleted query filter.
/// </summary>
public sealed record ListTrashedCurrenciesQuery(int PageNumber = 1, int PageSize = 20)
    : IQuery<PagedResponse<CurrencyDto>>;
