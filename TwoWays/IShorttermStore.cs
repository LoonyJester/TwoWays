namespace TwoWays
{
    public interface IShorttermStore
    {
        object Get(string key);

        T Get<T>(string key);

        T Get<T>(string key, T defaultValue);

        void Put(string key, object data, int expirationTimeout = 0);
    }
}
