using FSH.Modules.Accounting.Contracts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using FSH.Framework.Shared.Persistence;
using Mediator;
namespace FSH.Modules.Accounting.Contracts.v1.Accounts;

public sealed record UpdateAccountByIdCommand
(
    string? TenantId = null,
   AccountStatus? Status = null,
    long? accountFather = null,
    string? accountrefrense = null,
    int PageNumber = 1,
    int PageSize = 20) : IQuery<PagedResponse<AccountDto>>;

