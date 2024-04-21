namespace BookbrowseAPI.Models
{
    /// <summary>
    /// Represents the table in database.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// The identification of the book.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The title of the book.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The author of the book.
        /// </summary>
        public string? Author { get; set; }
        /// <summary>
        /// The publication year of the book.
        /// </summary>
        public int Year { get; set; }
    }
}
