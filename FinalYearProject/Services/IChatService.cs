using FinalYearProject.Models;
using Azure.AI.OpenAI;

namespace FinalYearProject.Services
{
    public interface IChatService
    {
        Task<ChatMessage> SendMessageAsync(string userId, string message);
        Task<IEnumerable<Microsoft.Extensions.AI.ChatMessage>> GetChatHistoryAsync(string userId);
    }
}