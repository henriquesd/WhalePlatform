namespace WP.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}