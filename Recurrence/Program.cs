using System;
using System.Collections.Generic;

namespace Recurrence
{
    class Program
    {
        static void Main(string[] args)
        {
            var recurrenceApp = new RecurrenceApp();
            recurrenceApp.Run();
            var quests = new List<Quest>();
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=============");
                Console.WriteLine("Type any text to create a quest.\nls to list all quests.\nq to quit");
                var title = Console.ReadLine();
                if (title == "q")
                    return;
                if (title == "ls")
                {
                    DisplayQuests(quests);
                    continue;
                }
                    
                quests.Add(recurrenceApp.CreateQuest(title));
                DisplayQuests(quests);
            }
        }

        private static void DisplayQuests(List<Quest> quests)
        {
            foreach (var quest in quests)
            {
                Console.WriteLine(quest);
            }
        }
    }
}
