using MVCCrud.Models;
using Microsoft.EntityFrameworkCore;

namespace MVCCrud.Data
{
    public class GeneralDbContext:DbContext
    {
        public GeneralDbContext(DbContextOptions<GeneralDbContext> options) : base(options) // `DbContext` used for accessing the whole application(like connection object).
        {

        }

        // We need to create separate `DbSet` for each table in the database/for each model class
        public DbSet<EmployeeInfo> EmployeeInfo { get; set; } // `DbSet` is used for accesing the table in the database/model class 
        public DbSet<ProductsInfo> ProductsInfo { get; set; }
    }
}
