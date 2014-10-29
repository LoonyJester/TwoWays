using System;

namespace FirstWay
{
    public interface ISessionDataService
    {
        object Get(string key);

        T Get<T>(string key);

        T Get<T>(string key, T defaultValue);

        T Get<T>(string key, Func<T> getData);

        void Put(string key, object data, int expirationTimeout = 0);

        void Clear(string[] sessionKeyPrefix);
        void Abandon();
    }
}
