namespace OnlineVetAPI.Interfaces
{
    public interface IImageRepository
    {
        public Task<string> Upload(IFormFile file, string fileName);
    }
}
