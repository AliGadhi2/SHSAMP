using FSH.Framework.Shared.Constants;

namespace FSH.Modules.Accounting.Contracts.Authorization;

public static class AccountingPermissions
{public static class Accounts
    {
        public const string Resource = "Accounting.Accounts";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }
    public static class Currencies
    {
        public const string Resource = "Accounting.Currencies";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }
    public static class ExtchangeRates
    {
        public const string Resource = "Accounting.ExchangeRates";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }
     public static class Spendings
    {
        public const string Resource = "Accounting.Spendings";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }
   public static class CashReceipts
    {
        public const string Resource = "Accounting.CashReceipts";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }
    public static class CashPayments
    {
        public const string Resource = "Accounting.CashPayments";
        public const string View    = $"Permissions.{Resource}.View";
        public const string Create  = $"Permissions.{Resource}.Create";
        public const string Update  = $"Permissions.{Resource}.Update";
        public const string Delete  = $"Permissions.{Resource}.Delete";
        public const string Restore = $"Permissions.{Resource}.Restore";
    }

    

    public static IReadOnlyList<FshPermission> All { get; } =
    [
        new("View Accounts",    ActionConstants.View,   Accounts.Resource, IsBasic: true),
        new("Create Accounts",  ActionConstants.Create, Accounts.Resource),
        new("Update Accounts",  ActionConstants.Update, Accounts.Resource),
        new("Delete Accounts",  ActionConstants.Delete, Accounts.Resource),
        new("Restore Accounts", "Restore",              Accounts.Resource),

        new("View Currencies",    ActionConstants.View,   Currencies.Resource, IsBasic: true),
        new("Create Currencies",  ActionConstants.Create, Currencies.Resource),
        new("Update Currencies",  ActionConstants.Update, Currencies.Resource),
        new("Delete Currencies",  ActionConstants.Delete, Currencies.Resource),
        new("Restore Currencies", "Restore",              Currencies.Resource),

        new("View Exchange Rates",    ActionConstants.View,   ExtchangeRates.Resource, IsBasic: true),
        new("Create Exchange Rates",  ActionConstants.Create, ExtchangeRates.Resource),
        new("Update Exchange Rates",  ActionConstants.Update, ExtchangeRates.Resource),
        new("Delete Exchange Rates",  ActionConstants.Delete, ExtchangeRates.Resource),
        new("Restore Exchange Rates", "Restore",              ExtchangeRates.Resource),
        new("View Spendings",    ActionConstants.View,   Spendings.Resource, IsBasic: true),
        new("Create Spendings",  ActionConstants.Create, Spendings.Resource),
        new("Update Spendings",  ActionConstants.Update, Spendings.Resource),
        new("Delete Spendings",  ActionConstants.Delete, Spendings.Resource),
        new("Restore Spendings", "Restore",              Spendings.Resource),
        new("View Cash Receipts",    ActionConstants.View,   CashReceipts.Resource, IsBasic: true),
        new("Create Cash Receipts",  ActionConstants.Create, CashReceipts.Resource),
        new("Update Cash Receipts",  ActionConstants.Update, CashReceipts.Resource),
        new("Delete Cash Receipts",  ActionConstants.Delete, CashReceipts.Resource),
        new("Restore Cash Receipts", "Restore",              CashReceipts.Resource),
        new("View Cash Payments",    ActionConstants.View,   CashPayments.Resource, IsBasic: true),
        new("Create Cash Payments",  ActionConstants.Create, CashPayments.Resource),
        new("Update Cash Payments",  ActionConstants.Update, CashPayments.Resource),
        new("Delete Cash Payments",  ActionConstants.Delete, CashPayments.Resource),
        new("Restore Cash Payments", "Restore",              CashPayments.Resource)

    ];
}

