using Microsoft.AspNetCore.Mvc;
using FinalYearProject.Services;
using FinalYearProject.Models;
using System.Security.Claims;

namespace FinalYearProject.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be empty");
            }

            //var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            //if (string.IsNullOrEmpty(userId))
            //{
            //    return Unauthorized();
            //}

            try
            {
                var response = await _chatService.SendMessageAsync("1", message);
                return Json(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your message");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetChatHistory()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            try
            {
                var history = await _chatService.GetChatHistoryAsync(userId);
                return Json(history);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving chat history");
            }
        }
    }
}