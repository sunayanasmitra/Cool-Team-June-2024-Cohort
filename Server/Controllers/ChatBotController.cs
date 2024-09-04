using HealthCareApp.Server.Data;
using HealthCareApp.Server.Models;
using HealthCareApp.Shared.Dto.ChatBot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Runtime.InteropServices;
using System.Security.Policy;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthCareApp.Server.Controllers
{
    [Authorize]
    [Route("api/ChatBot")]
    public class ChatBotController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        private static List<ChatMessage> _chatHistory = new List<ChatMessage>();

        private List<string> headings = new List<string> { "Based on your symptoms, you likely have a ",
                                                           "It's likely that you are experiencing a ",
                                                           "There's a strong possibility that you have a ",
                                                           "From your message, I've deduced that you probably have a ",
                                                           "Your symptoms suggest that you might have a "};

        public ChatBotController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpGet("/Get-ChatBot")]
        public async Task<IActionResult> GetChatBot()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userId = _userManager.GetUserId(User);
                    if (userId == null)
                    {
                        return Unauthorized("User not authenticated");
                    }
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("/ChatBot/PostChatMessage")]
        public async Task<IActionResult> PostChatMessage([FromBody] string message)
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            if (message == null || string.IsNullOrWhiteSpace(message))
            {
                return BadRequest("Invalid user input");
            }
            ChatMessage userMessage = new ChatMessage(message, true);
            var response = await GetChatBotResponse(userMessage);
            _chatHistory.Add(userMessage);
            _chatHistory.Add(response);
            return Ok(response.Text);
        }

        [HttpGet("/ChatBot/GetChatHistory")]
        public async Task<ActionResult<List<ChatHistory>>> GetChatHistory()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }

            return Ok(_chatHistory);
        }

        [HttpDelete("/ChatBot/DeleteChatHistory")]
        public async Task<ActionResult<List<ChatHistory>>> DeleteChatHistory()
        {
            var userId = _userManager.GetUserId(User);
            if (userId == null)
            {
                return Unauthorized("User not authenticated");
            }
            _chatHistory.Clear();
            return Ok(_chatHistory);
        }

        [NonAction]
        public async Task<ChatMessage> GetChatBotResponse(ChatMessage userMessage)
        {
            string response = string.Empty;
            string URL = $"https://coolteammldiagnosis-production.up.railway.app/?query={userMessage.Text}";
            using (HttpClient http = new HttpClient())
            {
                var json = await http.GetFromJsonAsync<MlResultDto>(URL);
                if (json.diagnosis.Count() > 0)
                {
                    var random = new Random();
                    int rndIndex = random.Next(headings.Count());
                    response = $"{headings[rndIndex]}{json.diagnosis[0]}";
                }
                else
                {
                    response = "Not enough information to make a diagnosis, please enter more details";
                }
                
            }
            return new ChatMessage(response, false);
        }


        public class ChatMessage
        {
            public string Text { get; set; }
            public DateTime PostTime { get; }
            public bool IsUser { get; set; }

            public ChatMessage(string text, bool isUser)
            {
                Text = text;
                PostTime = DateTime.Now;
                IsUser = isUser;
            }

        }

        public class ChatHistory
        {
            public ChatMessage UserQuery { get; set; }
            public ChatMessage ChatBotResponse { get; set; }

            public ChatHistory(ChatMessage userQuery, ChatMessage chatBotResponse)
            {
                UserQuery = userQuery;
                ChatBotResponse = chatBotResponse;
            }
        }
    }
}
