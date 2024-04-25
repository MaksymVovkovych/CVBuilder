using System;
using System.Collections.Generic;
using CVBuilder.EFContext.Transaction.Actions;

namespace CVBuilder.EFContext.Transaction;

public abstract class TransactionWrapperBase : ITransactionWrapper
{
    private List<ActionBase> _afterCommitActions = new();
    private List<ActionBase> _afterRollbackActions = new();
    private bool _isCommited;
    private bool _isRollbackOnDispose;

    public bool IsDisposed { get; private set; }

    public void RegisterAfterCommitAction(ActionBase action)
    {
        _afterCommitActions.Add(action);
    }

    public void RegisterAfterRollbackAction(ActionBase action)
    {
        _afterRollbackActions.Add(action);
    }

    public void Commit()
    {
        if (IsDisposed) throw new Exception("Transaction already disposed.");

        if (_isCommited) throw new Exception("Transaction already commited.");

        if (_isRollbackOnDispose) throw new Exception("TransactionAbortedException");
        //throw new TransactionAbortedException();
        DoCommit();
        _isCommited = true;
        _afterCommitActions.ForEach(a => a.Execute());
    }

    public void Dispose()
    {
        if (!_isCommited)
            try
            {
                DoRollback();
            }
            catch (Exception ex)
            {
                //LogHolder.MainLog.Error(ex, "Error transaction rollback.");
            }

        try
        {
            DoDispose();
        }
        catch (Exception ex)
        {
            //LogHolder.MainLog.Error(ex, "Error transaction dispose.");
        }

        IsDisposed = true;

        try
        {
            if (!_isCommited) _afterRollbackActions.ForEach(a => a.Execute());
        }
        finally
        {
            _afterCommitActions.Clear();
            _afterRollbackActions.Clear();

            _afterCommitActions = null;
            _afterRollbackActions = null;
        }
    }

    public void RollbackOnDispose()
    {
        if (IsDisposed) throw new Exception("Transaction already disposed.");

        if (_isCommited) throw new Exception("Transaction already commited.");

        _isRollbackOnDispose = true;
    }

    protected abstract void DoCommit();
    protected abstract void DoRollback();
    protected abstract void DoDispose();
}