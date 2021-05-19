using System;
using System.Threading;
using System.Threading.Tasks;
using CeVIO.Talk.RemoteService2;

namespace CeVIOAIErrorTest
{
    public class KoharuRikka : IDisposable
    {
        const string CastName = "小春六花";

        readonly Talker2 _talker;

        public KoharuRikka()
        {
            ServiceControl2.StartHost(false);
            Console.WriteLine(ServiceControl2.HostVersion);
            _talker = new Talker2(CastName);
        }

        public void Dispose()
        {
            ServiceControl2.CloseHost();
        }

        public async Task Speak(string text, CancellationToken cancellationToken = new())
        {
            await Task.Run(async () =>
            {
                try
                {
                    var state = _talker.Speak(text);

                    // 適当に結果をポーリングで監視する
                    while (!state.IsCompleted) await Task.Delay(50, cancellationToken);
                }
                catch (OperationCanceledException)
                {
                    var result = _talker.Stop();
                    Console.WriteLine(result);
                    throw;
                }
            }, cancellationToken);
        }
    }
}
