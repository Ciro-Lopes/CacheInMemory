namespace Api.CacheInMemory
{
    public static class UserService
    {
        private static readonly List<User> _users = new List<User>();

        public static User? GetUserByName(string userName)
        {
            Thread.Sleep(3000);
            return _users.FirstOrDefault(x => x.Name == userName);
        }

        public static List<User>? GetAll()
        {
            return _users;
        }

        public static User Insert(User user)
        {
            _users.Add(user);

            return user;
        }
    }
}