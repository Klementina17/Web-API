using BasicWebAPI.Models;

namespace BasicWebAPI.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void Update(Company obj);
    }
}
