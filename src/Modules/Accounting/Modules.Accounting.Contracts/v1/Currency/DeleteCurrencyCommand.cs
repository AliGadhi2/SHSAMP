using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FSH.Modules.Accounting.Contracts.v1.Currencies;

public sealed record DeleteCurrencyCommand(Guid CurrencyId) : ICommand<Unit>;
