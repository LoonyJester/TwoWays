namespace FirstWay
{
    public class CurrentMemberService : ICurrentMemberService
    {
        private const string FullUserInfoSessionKey = "CurrentMemberService_FulleUserInfo";

        private readonly ISessionDataService _ssn;

        public CurrentMemberService(ISessionDataService ssnDataService)
        {
            _ssn = ssnDataService;
        }

        public UserInfo CurrentUser
        {
            get
            {
                return _ssn.Get(FullUserInfoSessionKey, GetCurrentUser);
            }
        }

        private UserInfo GetCurrentUser()
        {
            return new UserInfo();
        }
    }
}
