using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.ImageAPI.DAL.Database.Interfaces;

namespace DotNetOverflow.ImageAPI.DAL.Database.Repository;

public class ImageUnitOfWork(IImageRepository? imageRepository,
        AppDbContext appDbContext)
    : IImageUnitOfWork
{
    public IImageRepository ImageRepository
    {
        get
        {
            if (imageRepository is null)
                imageRepository = new ImageRepository(appDbContext);
            return imageRepository;
        }
    }
}