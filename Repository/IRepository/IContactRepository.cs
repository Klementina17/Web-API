using BasicWebAPI.Models;

namespace BasicWebAPI.Repository.IRepository
{
    public interface IContactRepository : IRepository<Contact>
    {
        void Update(Contact obj);
    }
}
