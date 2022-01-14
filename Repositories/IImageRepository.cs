namespace OnlineVetAPI.Repositories
{
    public interface IImageRepository
    {
        public Task<string> Upload(IFormFile file, string fileName);
    }
}
