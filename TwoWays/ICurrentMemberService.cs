namespace TwoWays
{
    public interface ICurrentMemberService: IService
    {
        UserInfo CurrentUser { get; }
    }
}
