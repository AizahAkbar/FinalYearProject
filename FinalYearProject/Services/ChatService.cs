using Azure.AI.OpenAI;
using FinalYearProject.Models;
using FinalYearProject.Data;
using Microsoft.Extensions.Configuration;
using OpenAI;
using Microsoft.Extensions.AI;
using FinalYearProject.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalYearProject.Services
{
    public class ChatService : IChatService 
    {
        private readonly IChatClient _openAIClient;
        private readonly string _deploymentName;
        private List<Microsoft.Extensions.AI.ChatMessage> chatHistory;

        public ChatService(IConfiguration configuration)
        {
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var key = "";
            _deploymentName = configuration["AzureOpenAI:DeploymentName"];
            _openAIClient = new OpenAIClient(key).AsChatClient("gpt-4o");

            chatHistory = new List<Microsoft.Extensions.AI.ChatMessage>
            {
                new Microsoft.Extensions.AI.ChatMessage(ChatRole.System, "You are a helpful bakery assistant who helps customers with questions about our bakery products, orders, and services.")
            };
        }

        public async Task<Models.ChatMessage> SendMessageAsync(string userId, string message)
        {
            chatHistory.Add(new Microsoft.Extensions.AI.ChatMessage(ChatRole.User, message));

            var response = await _openAIClient.GetResponseAsync(chatHistory);

            var chatMessage = new Models.ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Content = response.Text,
                Timestamp = DateTime.UtcNow
            };

            return chatMessage;
        }

        public async Task<IEnumerable<Microsoft.Extensions.AI.ChatMessage>> GetChatHistoryAsync(string userId)
        {
            return chatHistory;
        }
    }
}
