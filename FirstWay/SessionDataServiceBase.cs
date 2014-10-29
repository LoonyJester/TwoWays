using System;

namespace FirstWay
{
    public abstract class SessionDataServiceBase : ISessionDataService
    {
        public abstract object Get(string key);

        public T Get<T>(string key)
        {
            var data = Get(key);

            if (data != null)
                return (T)data;

            var type = typeof(T);

            if (type == typeof(int) || type == typeof(long) || type == typeof(byte) || type == typeof(short))
                data = default(T);

            if (type == typeof(bool))
                data = false;

            return (T)data;
        }

        public T Get<T>(string key, T defaultValue)
        {
            var data = Get(key);

            return data != null
                ? (T)data
                : defaultValue;
        }

        public T Get<T>(string key, Func<T> getData)
        {
            var data = Get(key);

            if (data != null)
                return (T)data;
            var result = getData();

            Put(key, result);

            return result;
        }

        public abstract void Put(string key, object data, int expirationTimeout = 0);

        public abstract void Clear(string[] sessionKeyPrefix);

        public abstract void Abandon();
    }
}
