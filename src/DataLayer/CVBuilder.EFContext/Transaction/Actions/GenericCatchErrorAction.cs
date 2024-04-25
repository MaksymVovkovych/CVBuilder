using System;

namespace CVBuilder.EFContext.Transaction.Actions;

public class GenericCatchErrorAction : ActionBase
{
    private readonly string _catchErrorMessage;

    public GenericCatchErrorAction(Action action, string catchErrorMessage)
    {
        if (string.IsNullOrWhiteSpace(catchErrorMessage)) throw new ArgumentNullException(nameof(catchErrorMessage));

        _catchErrorMessage = catchErrorMessage;

        Action = action
                 ?? throw new ArgumentNullException(nameof(action));
    }

    private Action Action { get; }

    public override void Execute()
    {
        try
        {
            Action();
        }
        catch (Exception ex)
        {
            //LogHolder.MainLog.Error(ex, CatchErrorMessage);
        }
    }
}