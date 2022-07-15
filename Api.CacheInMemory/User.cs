namespace Api.CacheInMemory
{
    public class User
    {
        public User(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public User(){}

        public string? Name { get; set; }
        public int Age { get; set; }
    }
}