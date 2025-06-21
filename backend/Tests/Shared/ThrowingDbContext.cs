using Food_Creator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace FoodCreator.Tests.Shared
{
    /// <summary>
    /// Kontekst bazodanowy, który zawsze rzuca wyjątek przy zapisie – używany do testów błędów.
    /// </summary>
    public class ThrowingDbContext : ApplicationDbContext
    {
        public ThrowingDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            throw new Exception("Simulated database failure");
        }
    }
}
