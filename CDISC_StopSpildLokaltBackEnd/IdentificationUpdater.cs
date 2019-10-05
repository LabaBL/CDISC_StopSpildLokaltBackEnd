using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace CDISC_StopSpildLokaltBackEnd {
    public class IdentificationUpdater : IHostedService {

        private static string cron = "0 0 3 1/1 * ? *";
        // https://thinkrethink.net/2018/05/31/run-scheduled-background-tasks-in-asp-net-core/

        public IdentificationUpdater() {
        }

        public Task StartAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }
    }
}
