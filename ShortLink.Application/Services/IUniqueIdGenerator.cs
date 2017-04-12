namespace ShortLink.Application.Services
{
    /// <summary>
    /// Unique key generator
    /// </summary>
    public interface IUniqueIdGenerator
    {
        /// <summary>
        /// Get new unique id
        /// </summary>
        /// <returns></returns>
        string GetId();
    }
}
