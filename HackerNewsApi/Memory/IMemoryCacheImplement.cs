using System;
using System.Threading.Tasks;

namespace HackerNewsApi.Memory
{
    public interface ICacheInterface<Story>
    {
        #region Public Methods

        Task<Story> GetOrCreate(object key, Func<Task<Story>> createItem);

        #endregion Public Methods
    }
}