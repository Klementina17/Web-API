namespace BasicWebAPI.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        ICountryRepository Country { get; }
        IContactRepository Contact { get; }
        void Save();
    }
}
