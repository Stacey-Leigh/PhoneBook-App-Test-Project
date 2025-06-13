using Microsoft.EntityFrameworkCore;
using PhonebookApp.Models;

namespace PhonebookApp.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Contact> Contacts { get; set; }
}