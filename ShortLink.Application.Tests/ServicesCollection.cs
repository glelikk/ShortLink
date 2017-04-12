using Xunit;

namespace ShortLink.Application.Tests
{
    [CollectionDefinition("Application collection")]
    public class ServicesCollection : ICollectionFixture<ServicesFixture>
    {
    }
}
