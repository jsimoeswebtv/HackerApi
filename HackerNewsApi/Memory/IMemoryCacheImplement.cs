using System;
using System.Threading.Tasks;

namespace HackerNewsApi.Memory
{
    public interface IMemoryCacheImplement<Story>
    {
        Task<Story> GetOrCreate(object key, Func<Task<Story>> createItem);
    }
}