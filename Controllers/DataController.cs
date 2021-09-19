using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Modbus.Data;
using Modbus.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Modbus.Controllers
{
    [Authorize]
    public class DataController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly DataContext db;

        public DataController(ILogger<HomeController> logger, DataContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> DeleteAll()
        {
            var PlcDatas = await db.PlcDatas.ToListAsync();
            db.PlcDatas.RemoveRange(PlcDatas);
            await db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
