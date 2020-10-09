using System.Threading.Tasks;

namespace Blog.Core.IRepository.Data
{
    public interface ISeedData
    {
        Task SeedAsync();
    }
}
