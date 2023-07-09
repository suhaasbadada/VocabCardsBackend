using magooshAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace magooshAPI
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options): base(options)
        {
        }
        public DbSet<Flashcard> Flashcards { get; set; }
       // public DbSet<User> Users { get; set; }
    }
}
