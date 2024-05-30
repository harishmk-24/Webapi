using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.Repository
{
    public class context:DbContext
    {
        public context()
        {
        }

        public context(DbContextOptions options) : base(options) { }


        public DbSet<Emp> emps {  get; set; }

        
    }
}
