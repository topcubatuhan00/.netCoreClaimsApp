using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities.BackgroundServices
{
    public class TestTimeWrite : IHostedService, IDisposable
    {
        private Timer timer;

        public void Dispose()
        {
            timer = null;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(WriteTimeOnScreen, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite,0);
            return Task.CompletedTask;
        }

        private void WriteTimeOnScreen(object? state)
        {
            Console.WriteLine($"Time is : {DateTime.Now.ToLongTimeString()}");
        }
    }
}
