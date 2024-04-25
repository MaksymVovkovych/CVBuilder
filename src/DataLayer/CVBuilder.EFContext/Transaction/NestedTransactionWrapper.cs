using CVBuilder.EFContext.Transaction.Actions;

namespace CVBuilder.EFContext.Transaction;

public class NestedTransactionWrapper : ITransactionWrapper
{
    private readonly TransactionWrapperBase _innerTransaction;
    private bool _isCommited;

    public NestedTransactionWrapper(TransactionWrapperBase innerTransaction)
    {
        _innerTransaction = innerTransaction;
    }

    public void RegisterAfterCommitAction(ActionBase action)
    {
        _innerTransaction.RegisterAfterCommitAction(action);
    }

    public void RegisterAfterRollbackAction(ActionBase action)
    {
        _innerTransaction.RegisterAfterRollbackAction(action);
    }

    public void Commit()
    {
        _isCommited = true;
    }

    public void Dispose()
    {
        if (!_isCommited) _innerTransaction.RollbackOnDispose();
    }
}