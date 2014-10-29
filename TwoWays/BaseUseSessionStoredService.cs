using System;

namespace TwoWays
{
    public abstract class BaseUseSessionStoredService : IService
    {
        protected readonly ISessionDataStore SesseionStore;

        protected BaseUseSessionStoredService(ISessionDataStore sesseionStore)
        {
            SesseionStore = sesseionStore;
        }

        protected T GetSessionValue<T>(Func<T> getDataFuncIfMissing, int expirationTimeout = 0)
        {
            var key = GetKey(typeof(T).Name);
            var data = SesseionStore.Get(key);

            if (data != null)
                return (T)data;
            var result = getDataFuncIfMissing();

            SesseionStore.Put(key, result, expirationTimeout);

            return result;
        }

        protected void SetSessionValue<T>(T data, int expirationTimeout = 0)
        {
            SesseionStore.Put(GetKey(typeof(T).Name), data, expirationTimeout);
        }

        protected virtual string GetKey(string simplekey)
        {
            return string.Format("{0}_{1}", GetType().Name, simplekey);
        }
    }
}
