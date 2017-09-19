using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
namespace Crowe.Models
{
    public class MessagesContext : DbContext
    {
        public MessagesContext() { }

        public MessagesContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Messages> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            if (!optionsBuilder.Options.Extensions.Any(provider => provider is InMemoryOptionsExtension))
            {
                optionsBuilder.UseSqlite("Data Source=Crowe.db");
            }  
        }
    }
}
