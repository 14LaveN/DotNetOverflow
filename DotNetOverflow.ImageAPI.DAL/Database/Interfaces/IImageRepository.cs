using DotNetOverflow.Core.Entity.Image;

namespace DotNetOverflow.ImageAPI.DAL.Database.Interfaces;

public interface IImageRepository
{
    Task CreateImages(IEnumerable<ImageEntity> images);

    Task CreateImage(ImageEntity image);
    
    Task DeleteImages(IEnumerable<ImageEntity> images);

    Task<IQueryable<ImageEntity>> GetImagesFromQuestion(Guid questionId);
    
    Task<IQueryable<ImageEntity>> GetAllImages();

    Task DeleteImage(ImageEntity imageEntity);

    Task<ImageEntity?> GetImageById(Guid id);
}