using Log.Domain.Entities;
using Log.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Context
{
    public class AuditDbContext : DbContext
    {
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ActionType> ActionTypes { get; set; }

        public AuditDbContext(DbContextOptions<AuditDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<ActionType>().HasData(
       new ActionType { Id = (int)ActionTypeEnum.Add, Name = ActionTypeEnum.Add.ToString() },
       new ActionType { Id = (int)ActionTypeEnum.Update, Name = ActionTypeEnum.Update.ToString() },
       new ActionType { Id = (int)ActionTypeEnum.Delete, Name = ActionTypeEnum.Delete.ToString() }
   );

        
        }
    }
}
