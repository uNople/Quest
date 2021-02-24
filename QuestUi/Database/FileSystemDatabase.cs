using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using QuestUi.Data;

namespace QuestUi.Database
{
    public class FileSystemDatabase : IQuestDbContext
    {
        private const string _fileName = "database.json";
        private bool _isLoaded { get; set; } = false;
        public List<Quest> Quests { get; set; } = new List<Quest>();
        public int _identity { get; set; }
        public async Task Add(Quest quest)
        {
            quest.Id = _identity++;
            await Task.Run(() => Quests.Add(quest));
            Save();
        }

        public async Task Delete(Quest quest)
        {
            await Task.Run(() => Quests.Remove(quest));
            Save();
        }

        public async Task<List<Quest>> Get()
        {
            if (!_isLoaded)
            {
                if (File.Exists(_fileName))
                {
                    var file = File.ReadAllText(_fileName);
                    Quests = JsonConvert.DeserializeObject<List<Quest>>(file);
                }

                if (Quests.Any())
                {
                    _identity = Quests.Max(x => x.Id + 1);
                }
                else
                {
                    _identity = 1;
                }
            }
            return await Task.Run(() => Quests);
        }

        public void Migrate()
        {

        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(Quests, Formatting.Indented);
            File.WriteAllText(_fileName, json);
        }

        public void Dispose()
        {
        }

        public async Task SaveChangesAsync()
        {
            await Task.Run(() => Save());
        }
    }
}
