using System.Collections.Generic;
using System.Threading.Tasks;

namespace VehicleTracking.Service.Cache
{
    public interface ICacheService
    {
        public Task<List<T>> GetMany<T>(string schema, string[] keys);

        public Task<string> Get(string schema, string key);

        public Task<T> Get<T>(string schema, string key);

        public Task<T> GetAndRemove<T>(string schema, string key);

        public Task<bool> Refresh(string schema, string key);

        public Task Remove(string schema, string key);

        public Task<bool> Store<T>(string schema, string key, T data, double? seconds = null);
    }
}
