using Microsoft.EntityFrameworkCore;
using SamsysDemo.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SamsysDemo.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var parameter = Expression.Parameter(entityType.ClrType, "IsRemoved");
                var deletedCheck = Expression.Lambda(Expression.Equal(Expression.Property(parameter, "IsRemoved"), Expression.Constant(false)), parameter);
                builder.Entity(entityType.ClrType).HasQueryFilter(deletedCheck);
            }
        }

        public virtual DbSet<Client> Clients { get; set; }
    }
}
