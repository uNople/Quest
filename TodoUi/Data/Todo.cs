using System;
using System.ComponentModel.DataAnnotations;

namespace TodoUi.Data
{
    public class Todo
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
    }
}
