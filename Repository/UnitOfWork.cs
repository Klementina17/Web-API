using BasicWebAPI.Data;
using BasicWebAPI.Repository.IRepository;

namespace BasicWebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        public ICompanyRepository Company { get; private set; }
        public ICountryRepository Country { get; private set; }
        public IContactRepository Contact { get; private set; }

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Company = new CompanyRepository(_db);
            Country = new CountryRepository(_db);
            Contact = new ContactRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

    }
}
