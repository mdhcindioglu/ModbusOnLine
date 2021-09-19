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
            short DeviceAddress = 1;
            string IP = "127.0.0.1";
            int ReadingTime = 10000; //1 second = 1000
            ModbusConnetion modbus = new(DeviceAddress, IP);
            Logger<BgWorker> logger = new Logger<BgWorker>(new LoggerFactory());

            using var scope = serviceProvider.CreateScope();
            var db = scope.ServiceProvider.GetService<DataContext>();
            logger = scope.ServiceProvider.GetService<Logger<BgWorker>>();

            while(!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    if(!modbus.IsConnected) modbus.Connect();
                    var response = modbus.ReadHolding(0x1000, 6);
                    if(response != null && response.Registers != null)
                    {
                        PlcData plcData = new();
                        plcData.D1 = response.Registers[0];
                        plcData.D2 = response.Registers[1];
                        plcData.D3 = response.Registers[2];
                        plcData.D4 = response.Registers[3];
                        plcData.D5 = response.Registers[4];
                        plcData.D6 = response.Registers[5];
                        plcData.CreationDate = DateTime.Now;

                        await db.PlcDatas.AddAsync(plcData, stoppingToken);
                        await db.SaveChangesAsync(stoppingToken);
                    }

                    await Task.Delay(ReadingTime, stoppingToken);
                }
                catch(Exception ex) { logger.LogError(ex.Message); }
            }
        }
    }
}
