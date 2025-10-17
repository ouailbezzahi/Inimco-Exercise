using backend.Models;

namespace backend.Services
{
    public interface IPersonService
    {
        PersonAnalysis AnalyzePerson(Person person);
        void SavePerson(Person person);
    }
}