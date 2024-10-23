using Microsoft.EntityFrameworkCore;
using PatikaJWT.Entities;

namespace PatikaJWT.Context
{
    public class JWTDbContext : DbContext 
    {
        public JWTDbContext(DbContextOptions<JWTDbContext> options) : base(options)
        {
            
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
    
    }
}
