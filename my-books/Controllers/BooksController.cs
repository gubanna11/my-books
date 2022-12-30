﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BooksController : ControllerBase
	{
		public BooksService _bookService;
		public BooksController(BooksService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet("get-all-books")]
		public IActionResult GetAllBooks()
		{
			var allBooks = _bookService.GetAllBooks();
			return Ok(allBooks);
		}

		[HttpGet("get-book-by-id/{id}")]
		public IActionResult GetBookById(int id)
		{
			var book = _bookService.GetBookById(id);
			return Ok(book);
		}


		//[HttpPost]
		[HttpPost("add-book-with-authors")]
		public IActionResult AddBook([FromBody] BookVM book)
		{
			_bookService.AddBookWithAuthors(book);
			return Ok();
		}


		[HttpPut("update-book-by-id/{id}")]
		public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
		{
			var updatedBook = _bookService.UpdateBookById(id, book);

			return Ok(updatedBook);
		}

		[HttpDelete("delete-book-by-id/{id}")]
		public IActionResult DeleteBookById(int id)
		{
			_bookService.DeleteBookById(id);
			return Ok();
		}

	}
}
