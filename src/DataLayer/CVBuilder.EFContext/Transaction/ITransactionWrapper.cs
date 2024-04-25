using System;
using CVBuilder.EFContext.Transaction.Actions;

namespace CVBuilder.EFContext.Transaction;

public interface ITransactionWrapper : IDisposable
{
    void RegisterAfterCommitAction(ActionBase action);
    void RegisterAfterRollbackAction(ActionBase action);

    void Commit();
}