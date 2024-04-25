using System;

namespace CVBuilder.EFContext.Transaction.Actions;

public class GenericAction : ActionBase
{
    public GenericAction(Action action)
    {
        Action = action
                 ?? throw new ArgumentNullException(nameof(action));
    }

    private Action Action { get; }

    public override void Execute()
    {
        Action();
    }
}