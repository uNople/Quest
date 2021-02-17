using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TodoUi.Data;

namespace TodoUi.Database
{
    public class FileSystemDatabase : ITodoDbContext
    {
        private const string _fileName = "database.json";
        private bool _isLoaded { get; set; } = false;
        public List<Todo> Todos { get; set; } = new List<Todo>();
        public int _identity { get; set; }
        public async Task Add(Todo todo)
        {
            todo.Id = _identity++;
            await Task.Run(() => Todos.Add(todo));
            Save();
        }

        public async Task Delete(Todo todo)
        {
            await Task.Run(() => Todos.Remove(todo));
            Save();
        }

        public async Task<List<Todo>> Get()
        {
            if (!_isLoaded)
            {
                if (File.Exists(_fileName))
                {
                    var file = File.ReadAllText(_fileName);
                    Todos = JsonConvert.DeserializeObject<List<Todo>>(file);
                }

                if (Todos.Any())
                {
                    _identity = Todos.Max(x => x.Id + 1);
                }
                else
                {
                    _identity = 1;
                }
            }
            return await Task.Run(() => Todos);
        }

        public void Migrate()
        {

        }

        private void Save()
        {
            var json = JsonConvert.SerializeObject(Todos, Formatting.Indented);
            File.WriteAllText(_fileName, json);
        }

        public void Dispose()
        {
        }
    }
}
