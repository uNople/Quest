using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuestUi.Data
{
    [Table("Quests")]
    public class Quest
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        private bool _isCompleted;

        public bool IsCompleted
        {
            get { return _isCompleted; }
            set
            {
                _isCompleted = value;
                OnChanged?.Invoke();
            }
        }

        public event OnChangedEventHandler OnChanged;
        public delegate void OnChangedEventHandler();
    }
}
