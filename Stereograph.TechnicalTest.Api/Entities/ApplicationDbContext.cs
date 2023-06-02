using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Models;

namespace Stereograph.TechnicalTest.Api.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Person> People { get; set; }
}