namespace LatokenBot.Services;

internal class CancellationTokenService
{
    private readonly CancellationTokenSource _cts = new();

    public CancellationToken Token => _cts.Token;

    public void Cancel() => _cts.Cancel();
}