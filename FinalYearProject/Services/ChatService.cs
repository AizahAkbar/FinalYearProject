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
                new(ChatRole.System, "You are a helpful bakery assistant who helps customers with questions about our bakery products, orders, and services. Always display prices in British pounds using the � symbol (e.g. �2.50) and have to 2 decimal places. Format your responses using HTML markup for proper display in the web interface. Dont try to include images in your response."),
                new(ChatRole.System, "The products that we have are: " + JsonSerializer.Serialize(BakesCache.GetBakes()) + ". Present product information using HTML tags like <ul>, <li>, <strong>, etc.")
            };
        }

        public async Task<Models.ChatMessage> SendMessageAsync(string userId, string message)
        {
            chatHistory.Add(new ChatMessage(ChatRole.User, message));

            var response = await _openAIClient.GetResponseAsync(chatHistory);
            var formattedResponse = response.Messages.Last().Text
                .Replace("**", "")
                .Trim(); // Trim any leading/trailing whitespace

            chatHistory.Add(new ChatMessage(ChatRole.Assistant, formattedResponse));

            var chatMessage = new Models.ChatMessage
            {
                UserId = userId,
                Role = "assistant",
                Content = formattedResponse,
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
