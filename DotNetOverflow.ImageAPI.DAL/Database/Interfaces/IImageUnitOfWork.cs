namespace DotNetOverflow.ImageAPI.DAL.Database.Interfaces;

public interface IImageUnitOfWork
{
    IImageRepository ImageRepository { get; }
}