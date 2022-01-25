namespace OnlineVetAPI.Interfaces
{
    public interface IUnitOfWork
    {
        IOwnerRepository OwnerRepository { get; }
        IPetRepository PetRepository { get; }
        IUserRepository UserRepository { get; }

        Task<bool> SaveAsync();
    }
}
