using System;

namespace FinalYearProject.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string Role { get; set; } // "user" or "assistant"
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }
        public string UserId { get; set; } // Optional: to track which user sent the message
        public User User { get; set; } // Navigation property
    }
}