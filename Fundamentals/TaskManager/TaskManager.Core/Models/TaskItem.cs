using System;
using System.ComponentModel.DataAnnotations;

namespace TaskManager.Core.Models
{
    public class TaskItem
    {
        public long Id { get; set; }
        [Required]
        [MaxLength(50, ErrorMessage = "Please Enter Valid Title")]
        public string Title { get; set; }
        [MaxLength(200, ErrorMessage = "Please Enter Valid Description")]
        public string Description { get; set; }
        public TaskPriorityType Priority { get; set; }
        public TaskStatusType Status { get; set; }
        [Display(Name ="Due Date")]
        public DateTime? DueBy { get; set; }
    }
}
