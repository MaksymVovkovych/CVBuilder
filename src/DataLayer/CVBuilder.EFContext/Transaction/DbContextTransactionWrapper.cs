using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace CVBuilder.EFContext.Transaction;

public class DbContextTransactionWrapper : TransactionWrapperBase
{
    public DbContextTransactionWrapper(DbContext context)
    {
        InnerTransaction = context.Database.BeginTransaction();
    }

    public IDbContextTransaction InnerTransaction { get; private set; }

    protected override void DoCommit()
    {
        InnerTransaction.Commit();
    }

    protected override void DoRollback()
    {
        InnerTransaction.Rollback();
    }

    protected override void DoDispose()
    {
        if (InnerTransaction == null) return;

        try
        {
            InnerTransaction.Dispose();
        }
        finally
        {
            InnerTransaction = null;
        }
    }
}