using System;

namespace Recurrence
{
    public class RecurrenceApp
    {
        public void Run()
        {
            var quest = new Quest
            {
                Title = "A daily quest",
                Created = DateTime.Now,
                RecurrenceAmount = RecurrenceAmount.Daily
            };
            ScheduleDueDate(quest);
            Console.WriteLine(quest);
        }

        internal Quest CreateQuest(string title)
        {
            var quest = new Quest
            {
                Title = title,
                Created = DateTime.Now,
                RecurrenceAmount = RecurrenceAmount.Daily
            };
            ScheduleDueDate(quest);
            return quest;
        }

        private void ScheduleDueDate(Quest quest)
        {
            switch (quest.RecurrenceAmount)
            {
                case RecurrenceAmount.None:
                    break;
                case RecurrenceAmount.Daily:
                    // The most naiive implementation possible - just add one day to the date added
                    quest.CompleteBy = quest.Created.AddDays(1);
                    break;
            }
        }
    }
}
