using Azure.AI.OpenAI;
using FinalYearProject.Models;
using FinalYearProject.Data;
using Microsoft.Extensions.Configuration;
using OpenAI;

namespace FinalYearProject.Services
{
    public class ChatService : IChatService
    {
        private readonly OpenAIClient _openAIClient;
        private readonly string _deploymentName;
        private readonly FypContext _context;

        public ChatService(IConfiguration configuration, FypContext context)
        {
            var endpoint = configuration["AzureOpenAI:Endpoint"];
            var key = configuration["AzureOpenAI:Key"];
            _deploymentName = configuration["AzureOpenAI:DeploymentName"];
            _openAIClient = new OpenAIClient(new Uri(endpoint), new AzureKeyCredential(key));
            _context = context;
        }

        public async Task<ChatMessage> SendMessageAsync(string userId, string message)
        {
            var chatCompletionsOptions = new ChatCompletionsOptions()
            {
                Messages =
                {
                    new ChatMessage(ChatRole.System, "You are a helpful bakery assistant who helps customers with questions about our bakery products, orders, and services."),
                    new ChatMessage(ChatRole.User, message)
                },
                MaxTokens = 800
            };

            var response = await _openAIClient.GetChatCompletionsAsync(_deploymentName, chatCompletionsOptions);
            var responseMessage = response.Value.Choices[0].Message.Content;

            var chatMessage = new ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Content = responseMessage,
                Timestamp = DateTime.UtcNow
            };

            _context.ChatMessages.Add(chatMessage);
            await _context.SaveChangesAsync();

            return chatMessage;
        }

        public async Task<IEnumerable<ChatMessage>> GetChatHistoryAsync(string userId)
        {
            return await _context.ChatMessages
                .Where(m => m.UserId == userId)
                .OrderBy(m => m.Timestamp)
                .ToListAsync();
        }
    }
}