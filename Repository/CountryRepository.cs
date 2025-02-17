using BasicWebAPI.Data;
using BasicWebAPI.Models;
using BasicWebAPI.Repository.IRepository;

namespace BasicWebAPI.Repository
{
    public class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _db;
        public CountryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Country obj)
        {
            _db.Countries.Update(obj);
        }
    }
}
