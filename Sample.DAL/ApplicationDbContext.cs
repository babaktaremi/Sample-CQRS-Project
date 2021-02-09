using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Sample.DAL.Model;
using Sample.DAL.Model.WriteModels;

namespace Sample.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Movie_Write> Movies { get; set; }
    }
}
