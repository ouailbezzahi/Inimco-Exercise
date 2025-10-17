using backend.Models;

namespace backend.Repository
{
    public interface IFileRepository
    {
        void Save(Person person);
    }
}
