using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataConcurrency
{
    public class AppDbContext:DbContext
    {
        public DbSet<Person> Persons { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasKey(x => x.PersonID);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost,5030;Initial Catalog=Person; User ID=SA; Password=Password123; TrustServerCertificate=true");
        }
    }

    public class Person
    {
        public int PersonID { get; set; }
        [ConcurrencyCheck]
        public string PersonName { get; set; }
    }
}
