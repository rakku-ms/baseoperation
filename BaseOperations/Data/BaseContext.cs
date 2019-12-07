using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BaseOperations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BaseOperations.Data
{
    public class BaseContext: DbContext 
    {
        public BaseContext(DbContextOptions<BaseContext> options) : base(options)
        {

        }

        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
