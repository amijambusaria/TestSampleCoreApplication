using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleCoreApplication.Models
{
    public class EventContext : DbContext
    {
        public EventContext(DbContextOptions<EventContext> options):base(options)
        {

        }

        public DbSet<Event> Event { get; set; }

        public DbSet<Buyer> Buyer { get; set; }
    }
}
