using HackerNewsApi.Models;
using System.Threading.Tasks;

namespace HackerNewsApi.HackerApi
{
    public interface IApiClientInterface
    {
        #region Public Methods

        Task<Story> Get(int StoryId);

        Task<int[]> GetBestStories();

        #endregion Public Methods
    }
}