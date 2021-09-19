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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly DataContext db;

        public HomeController(ILogger<HomeController> logger, DataContext db)
        {
            this.logger = logger;
            this.db = db;
        }

        public async Task<IActionResult> Index()
        {
            var PlcDatas = await db.PlcDatas.Take(25).OrderByDescending(x => x.Id).ToListAsync();
            return View(PlcDatas);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
