using Microsoft.EntityFrameworkCore;
using TaskSistem.Data;
using TaskSistem.Data.Map;
using TaskSistem.Models;

namespace TaskSistem.Data {
    public class TaskSistemDBContext : DbContext {
        public TaskSistemDBContext(DbContextOptions<TaskSistemDBContext> options) : base(options) {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new TaskMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}

// Migration command 
// Add-Migration "migration-name" -Context "context-class-name"