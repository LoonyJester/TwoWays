namespace TwoWays
{
    public interface ISessionDataStore : IShorttermStore 
    {
        void Clear(string[] sessionKeyPrefix);
        void Abandon();
    }
}
