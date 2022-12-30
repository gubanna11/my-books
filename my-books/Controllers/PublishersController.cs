using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.Data.Services;
using my_books.Data.ViewModels;

namespace my_books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{
		private PublishersService _publisherService;
		public PublishersController(PublishersService publisherService)
		{
			_publisherService = publisherService;
		}

		[HttpPost("add-publisher")]
		public IActionResult AddAuthor([FromBody]PublisherVM publisher)
		{
			_publisherService.AddPublisher(publisher);
			return Ok();
		}
	}
}
