using System.ComponentModel.DataAnnotations;
using TestProject.Entity.Enum;

namespace TestProject.ViewModels
{
    public class TaskViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "DueDate is required")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Status is required")]
        public EStatus Status { get; set; }
    }
}
