using System.Threading;
using System.Threading.Tasks;
using CeVIOAIErrorTest;

using var rikka = new KoharuRikka();

while (true)
{
    var cts = new CancellationTokenSource();
    _ = rikka.Speak("こんにちは、私は小春六花。", cts.Token);
    await Task.Delay(500);
    cts.Cancel();
}
