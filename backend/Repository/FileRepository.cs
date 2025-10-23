using backend.Models;
using System.Text.Json;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace backend.Repository
{
    public class FileRepository : IFileRepository
    {
        private readonly string _filePath;

        public FileRepository(IWebHostEnvironment env)
        {
            _filePath = Path.Combine(env.ContentRootPath, "people.json");
        }

        public void Save(Person person)
        {
            var dir = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(dir))
            {
                Directory.CreateDirectory(dir);
            }

            List<Person> persons = new();

            if (File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        var trimmed = json.TrimStart();
                        if (trimmed.StartsWith("{"))
                        {
                            var single = JsonSerializer.Deserialize<Person>(json);
                            if (single is not null) persons.Add(single);
                        }
                        else
                        {
                            persons = JsonSerializer.Deserialize<List<Person>>(json) ?? new List<Person>();
                        }
                    }
                    catch (JsonException)
                    {
                        persons = new List<Person>();
                    }
                }
            }

            persons.Add(person);

            var updatedJson = JsonSerializer.Serialize(persons, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(_filePath, updatedJson);
        }
    }
}
