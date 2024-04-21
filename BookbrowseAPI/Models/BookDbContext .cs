using Microsoft.EntityFrameworkCore;

namespace BookbrowseAPI.Models
{
    /// <summary>
    /// Represents a session with the database and can be used to query and save instances of <see cref="Book" />.
    /// </summary>
    public class BookDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BookDbContext" /> class.
        /// </summary>
        /// <param name="options">The options for this context.</param>
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        /// <summary>
        /// A <see cref="DbSet{TEntity}" /> can be used to query and save instances of <see cref="Book" />.
        /// </summary>
        public DbSet<Book> Books { get; set; }

        /// <summary>
        /// Configures the database (and other options) to be used.
        /// </summary>
        /// <param name="optionsBuilder">A builder is used to create or modify options for this context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=book.db");
            }
        }
    }
}
