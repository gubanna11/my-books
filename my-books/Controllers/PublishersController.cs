using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my_books.ActionResults;
using my_books.Data.Models;
using my_books.Data.Services;
using my_books.Data.ViewModels;
using my_books.Exceptions;

namespace my_books.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PublishersController : ControllerBase
	{
		private PublishersService _publisherService;
		private readonly ILogger<PublishersController> _logger;
		public PublishersController(PublishersService publisherService, ILogger<PublishersController> logger)
		{
			_publisherService = publisherService;
			_logger = logger;	
		}

		[HttpGet("get-all-publishers")]
		public IActionResult GetAllPublishers(string? sortBy, string? searchString, int pageNumber)
		{
			//throw new Exception("This is an exception thrown from GetAllPiblishers()");
			try
			{
				_logger.LogInformation("This is just a log in GetAllPiblishers()");

				var _result = _publisherService.GetAllPublishers(sortBy, searchString, pageNumber);

				return Ok(_result);
			}
			catch (Exception)
			{
				return BadRequest("Sorry, we could not load the publishers");
			}
		}


		[HttpPost("add-publisher")]
		public IActionResult AddPublisher([FromBody]PublisherVM publisher)
		{
			try
			{
                var newPublisher = _publisherService.AddPublisher(publisher);
                //return Ok();
                return Created(nameof(AddPublisher), newPublisher);
            }
			catch (PublisherNameException ex)
			{
				return BadRequest($"{ex.Message}, Publisher name: {ex.PublisherName}");
			}
			catch(Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		[HttpGet("get-publisher-books-with-authors/{id}")]
		public IActionResult GetPublisherData(int id)
		{
			var _response = _publisherService.GetPublisherData(id);
			return Ok(_response);
		}


        [HttpGet("get-publisher-by-id/{id}")]
		public IActionResult GetPublisherById(int id)
		{
            var _response = _publisherService.GetPublisherById(id);

			if (_response != null)
			{
				return Ok(_response);
			}
			else
			{
				return NotFound();
			}
		}

        [HttpDelete("delete-publisher-by-id/{id}")]
		public IActionResult DeletePublisherById(int id)
		{
			try
			{
				_publisherService.DeletePublisherById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




		//[HttpGet("get-publisher-by-id/{id}")]
		////public IActionResult GetPublisherById(int id)
		////public Publisher GetPublisherById(int id)
		////public ActionResult<Publisher> GetPublisherById(int id)
		//public CustomActionResult GetPublisherById(int id)
		//{
		//	//throw new Exception("This is an exception that will be handled by middleware");

		//	var _response = _publisherService.GetPublisherById(id);

		//	if (_response != null)
		//	{
		//		//return Ok(_response);
		//		//return _response;
		//		//return _response;

		//		var _responseObj = new CustomActionResultVM()
		//		{
		//			Publisher = _response
		//		};

		//		return new CustomActionResult(_responseObj);
		//	}

		//	else
		//	{
		//		//return NotFound();
		//		//return null;
		//		//return NotFound();

		//		var _responseObj = new CustomActionResultVM()
		//		{
		//			Exception = new Exception("This is coming from publishers controller")
		//		};

		//		return new CustomActionResult(_responseObj);
		//	}
		//}

	}
}
