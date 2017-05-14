using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Broker.Model;

namespace Broker.Models
{
    public class BrokerContext : DbContext
    {
        public BrokerContext (DbContextOptions<BrokerContext> options)
            : base(options)
        {
        }

        public DbSet<Broker.Model.Buyer> Buyer { get; set; }
        public DbSet<Broker.Model.Stock> Stock { get; set; }
        public DbSet<Broker.Model.Seller> Seller { get; set; }
    }
}
