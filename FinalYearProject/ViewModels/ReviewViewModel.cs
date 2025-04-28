using System.ComponentModel.DataAnnotations;

namespace FinalYearProject.ViewModels
{
    public class ReviewViewModel
    {
        public int BakeId { get; set; }
        public string Description { get; set; }
        public int Rating { get; set; }
        public string UserId { get; set; }
    }
}