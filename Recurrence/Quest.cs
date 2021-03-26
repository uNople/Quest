using System;

namespace Recurrence
{
    public class Quest
    {
        public string Title {get; set;}
        public DateTime Created {get; set;}
        public DateTime CompleteBy {get; set;}
        public RecurrenceAmount RecurrenceAmount {get; set;}
        public override string ToString()
        {
            return $"Title: '{Title}', Added: {Created:g}, Due: {CompleteBy:g}, Recurrence: {RecurrenceAmount}";
        }
    }
}
