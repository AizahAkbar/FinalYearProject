using Microsoft.Extensions.AI;
using OpenAI;
using System.Text.Json;

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
                new(ChatRole.System, "You are a helpful bakery assistant who helps customers with questions about our bakery products, orders, and services."),
                new(ChatRole.System, "The products that we have are: " + JsonSerializer.Serialize(BakesCache.GetBakes()))
            };
        }

        public async Task<Models.ChatMessage> SendMessageAsync(string userId, string message)
        {
            chatHistory.Add(new ChatMessage(ChatRole.User, message));

            var response = await _openAIClient.GetResponseAsync(chatHistory);

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, response.Messages.Last().Text));

            var chatMessage = new Models.ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Content = response.Text,
                Timestamp = DateTime.UtcNow
            };

            return chatMessage;
        }

        public async Task<IEnumerable<Models.ChatMessage>> GetChatHistoryAsync(string userId)
        {
            return chatHistory
                .Where(x => x.Role != ChatRole.System)
                .Select(x =>
                {
                    return new Models.ChatMessage
                    {
                        UserId = userId,
                        Role = x.Role.ToString().ToLower(),
                        Content = x.Text,
                        Timestamp = DateTime.UtcNow
                    };
                });
        }
    }
}
