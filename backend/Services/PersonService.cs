using backend.Repository;
using backend.Models;
using System.Text.Json;

namespace backend.Services
{
    public class PersonService : IPersonService
    {
        private readonly IFileRepository _repository;

        public PersonService(IFileRepository repository)
        {
            _repository = repository;
        }

        public PersonAnalysis AnalyzePerson(Person person)
        {
            var fullName = $"{person.FirstName} {person.LastName}";
            var nameWithoutSpaces = fullName.Replace(" ", "").ToLower();

            var vowels = "aeiou";
            var consonants = "bcdfghjklmnpqrstvwxyz";

            var vowelsCount = nameWithoutSpaces.Count(c => vowels.Contains(c));
            var consonantsCount = nameWithoutSpaces.Count(c => consonants.Contains(c));

            var reversedName = new string(fullName.Reverse().ToArray());

            var analysis = new PersonAnalysis
            {
                VowelsCount = vowelsCount,
                ConsonantsCount = consonantsCount,
                FullName = fullName,
                ReversedFullName = reversedName,
                Person = person
            };

            return analysis;
        }

        public void SavePerson(Person person)
        {
            _repository.Save(person);
        }
    }
}
