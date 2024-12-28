using System.ComponentModel.DataAnnotations;

namespace ToDoAPI.Model
{
    public class AddToDoModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string TaskName { get; set; }
        public string? TaskDescription { get; set; }
        [Required]
        public DateTime? TaskStartDateTime { get; set; }
        public DateTime? TaskEndDateTime { get; set; }
        public bool? IsPriorityLow { get; set; }
        public bool? IsPriorityMedium { get; set; }
        public bool? IsPriorityHigh { get; set; }
        public bool? IsCompleted { get; set; }
        public int UserId { get; set; }
    }
}
