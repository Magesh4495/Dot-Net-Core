using Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entity.DbAccessLayer
{
    public class DataBaseRepositoryContext : DbContext
    {
        private string _connectionString = null;
        public DataBaseRepositoryContext()
        {

        }
        public DataBaseRepositoryContext(DbContextOptions<DataBaseRepositoryContext> options)
            : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }
        public virtual DbSet<Category> GetCategories { get; set; }
    }
}
