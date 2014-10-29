namespace TwoWays
{
    public class CurrentMemberService: BaseUseSessionStoredService, ICurrentMemberService
    {

        private readonly IUsersRepo _repo;
        public CurrentMemberService(ISessionDataStore sesseionStore) : base(sesseionStore)
        {
        }

        public UserInfo CurrentUser
        {
            get
            {
                return GetSessionValue(GetCurrentUser);
            }         
        }

        private UserInfo GetCurrentUser()
        {
            return new UserInfo();
        }
    }
}
