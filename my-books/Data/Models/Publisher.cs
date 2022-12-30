namespace my_books.Data.Models
{
	public class Publisher
	{
		public int Id { get; set; }
		public string Name { get; set; }


		//Navigartion Properties
		public List<Book> Books { get; set; }

	}
}
