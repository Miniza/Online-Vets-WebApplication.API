namespace OnlineVetAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IAppRepository AppRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> SaveAsync();
    }
}
