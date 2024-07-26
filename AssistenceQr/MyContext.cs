using AssistanceQr.Models;
using Microsoft.EntityFrameworkCore;

namespace AssistanceQr
{
    public class MyContext: DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
        }
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Assistance> Assistances { get; set; } = null!;


    }
}
