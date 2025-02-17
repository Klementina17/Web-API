using BasicWebAPI.Models;

namespace BasicWebAPI.Repository.IRepository
{
    public interface ICountryRepository : IRepository<Country>
    {
        void Update(Country obj);
    }
}
