using Microsoft.EntityFrameworkCore;
using NovusLiberus.Api.Entities;

namespace NovusLiberus.Api.Data;

public class DataContext: DbContext
{
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    public DbSet<Loan> Loans { get; set; } = null!;
    public DbSet<Review> Reviews { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;


    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Review>()
            .ToTable(b => b.HasCheckConstraint("CK_Review_Rating_Between_1_and_5", "Rating >= 1 AND Rating <= 5"));
        
        modelBuilder.Entity<Loan>()
            .Property(p=>p.LoanDate)
            .HasDefaultValueSql("GETDATE()");
        modelBuilder.Entity<Loan>()
            .Property(p=>p.DueDate)
            .HasDefaultValueSql("DATEADD(DAY, 14, GETDATE())");
    }
    
}