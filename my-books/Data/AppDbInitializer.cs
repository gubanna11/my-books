﻿using my_books.Data.Models;

namespace my_books.Data
{
	public class AppDbInitializer
	{
		public static void Seed(IApplicationBuilder applicationBuilder)
		{
			using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
			{
				var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

				if (!context.Publishers.Any())
				{
					context.Publishers.AddRange(
						new Publisher()
						{
							Name = "Publisher 1"
						},
						new Publisher()
						{
							Name = "Publisher 2"
						});
					context.SaveChanges();
				}

				if (!context.Books.Any())
				{
					context.Books.AddRange(new Book()
					{
						Title = "1st Book Title",
						Description = "1st Book Description",
						IsRead = true,
						DateRead = DateTime.Now.AddDays(-10),
						Rate = 4,
						Genre = "Biography",
						CoverUrl = "https...",
						DateAdded = DateTime.Now,
						PublisherId = 1
					},
					new Book()
					{
						Title = "2nd Book Title",
						Description = "2nd Book Description",
						IsRead = false,
						Genre = "Biography",
						CoverUrl = "https...",
						DateAdded = DateTime.Now,
						PublisherId = 2
					});
					context.SaveChanges();
				}
			}
		}
	}
}
