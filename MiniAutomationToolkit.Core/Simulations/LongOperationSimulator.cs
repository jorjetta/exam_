namespace MiniAutomationToolkit.Core.Simulations;


public class LongOperationSimulator
{
    private const int DelayMilliseconds = 2000;

    public string LongOperation()
    {
        Thread.Sleep(DelayMilliseconds);

        return "Done";
    }

    public async Task<string> LongOperationAsync()
    {
        await Task.Delay(DelayMilliseconds);

        return "Done";
    }
}
