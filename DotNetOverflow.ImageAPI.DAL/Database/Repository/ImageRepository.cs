using DotNetOverflow.Core.Entity.Image;
using DotNetOverflow.Identity.DAL.Database;
using DotNetOverflow.ImageAPI.DAL.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DotNetOverflow.ImageAPI.DAL.Database.Repository;

public class ImageRepository(AppDbContext appDbContext)
    : IImageRepository
{
    public async Task CreateImages(IEnumerable<ImageEntity> images)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            await appDbContext.Images.AddRangeAsync(images);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task CreateImage(ImageEntity image)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            await appDbContext.Images.AddAsync(image);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteImages(IEnumerable<ImageEntity> images)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            appDbContext.Images.RemoveRange(images);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IQueryable<ImageEntity>> GetImagesFromQuestion(Guid questionId)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var images = appDbContext
                .Images
                .Include(x=>x.Question)
                .Where(x => x.QuestionId == questionId);
            
            await appDbContext.Database.CommitTransactionAsync();

            return images;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<IQueryable<ImageEntity>> GetAllImages()
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var images = appDbContext
                .Images
                .Include(x=>x.Question);
            
            await appDbContext.Database.CommitTransactionAsync();

            return images;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task DeleteImage(ImageEntity imageEntity)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            appDbContext.Images.Remove(imageEntity);
            await appDbContext.SaveChangesAsync();
            
            await appDbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }

    public async Task<ImageEntity?> GetImageById(Guid id)
    {
        await appDbContext.Database.BeginTransactionAsync();
        try
        {
            var image = await appDbContext
                .Images
                .Include(x=>x.Question)
                .FirstOrDefaultAsync(x=>x.Id == id);
            
            await appDbContext.Database.CommitTransactionAsync();

            return image;
        }
        catch (Exception)
        {
            await appDbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }
}