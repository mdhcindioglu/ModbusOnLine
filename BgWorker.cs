using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Modbus.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Modbus
{
    public class BgWorker : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;
        public BgWorker(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Logger<BgWorker> logger = new Logger<BgWorker>(new LoggerFactory());

            try
            {
                using var scope = serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetService<DataContext>();
                logger = scope.ServiceProvider.GetService<Logger<BgWorker>>();

                while(!stoppingToken.IsCancellationRequested)
                {
                    PlcData plcData = new();
                    plcData.D1 = My.Random(4);
                    plcData.D2 = My.Random(4);
                    plcData.D3 = My.Random(4);
                    plcData.D4 = My.Random(4);
                    plcData.D5 = My.Random(4);
                    plcData.D6 = My.Random(4);
                    plcData.CreationDate = DateTime.Now;

                    await db.PlcDatas.AddAsync(plcData, stoppingToken);
                    await db.SaveChangesAsync(stoppingToken);

                    await Task.Delay(10000, stoppingToken);
                }
            }
            catch(Exception ex) { logger.LogError(ex.Message); }
        }
    }
}
