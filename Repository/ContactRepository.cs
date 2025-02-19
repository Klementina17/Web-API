using BasicWebAPI.Data;
using BasicWebAPI.Models;
using BasicWebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BasicWebAPI.Repository
{
    public class ContactRepository : Repository<Contact>, IContactRepository
    {
        private readonly ApplicationDbContext _db;

        public ContactRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Contact obj)
        {
            _db.Contacts.Update(obj);
        }

    }
}
