using BankAccountOOPMultiuser.Infrastructure.Contracts.Entities;

internal interface IAccountRepositoryHelpers
{
    List<Tuple<DateTime, decimal>> MovementCollectionToList(Account account);
}