using Microsoft.EntityFrameworkCore;
using User.Model.Models;

namespace User.Infraestructure.Base.Context
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {

        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
