using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Modbus.Data
{
    public class DataContext : IdentityDbContext
    {
        public DbSet<PlcData> PlcDatas { get; set; }
      
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }
    }
}
