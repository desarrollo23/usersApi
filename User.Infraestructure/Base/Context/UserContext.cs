using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using User.Model.Models;

namespace User.Infraestructure.Base.Context
{
    public class UserContext: IdentityDbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
