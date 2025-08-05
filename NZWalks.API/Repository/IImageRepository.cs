using NZWalks.API.Model.Domain;

namespace NZWalks.API.Repository
{
    public interface IImageRepository
    {
       Task<Image> Upload(Image image);
    }
}
