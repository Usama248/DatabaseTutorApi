using EntityLayer.DbContext.Entities;
using EntityLayer.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static EntityLayer.Helpers.ConnectionStringHelper;

namespace EntityLayer.DbContext
{
    public class DatabaseTutorDbContext : IdentityDbContext<DatabaseTutorUser, IdentityRole, string>
    {
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<AssignmentSolution> AssignmentSolution { get; set; }
        public DbSet<Class> Class { get; set; }
        public DbSet<StudentClass> StudentClass { get; set; }
        public DbSet<TeacherClass> TeacherClass { get; set; }
        public DbSet<StudentQuery> StudentQuery { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.DatabaseTutorConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
