namespace my_books.Data.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public bool IsRead { get; set; }
		public DateTime? DateRead { get; set; } //if it isn't read than we don't need dateread

		public int? Rate { get; set; } //also if we've read
		public string Genre { get; set; }
		public string CoverUrl { get; set; }
		public DateTime DateAdded { get; set; }


		//Navigation Properties
		public int PublisherId { get; set; }
		public Publisher Publisher { get; set; }
		
		public List<Book_Author> Book_Authors { get; set; }
	}
}
