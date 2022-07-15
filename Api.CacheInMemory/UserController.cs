using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Api.CacheInMemory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMemoryCache _memoryCache;

        public UserController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("get-by-name/{userName}")]
        public IActionResult GetUserByName(string userName)
        {
            User? user = new User();

            if (!_memoryCache.TryGetValue(userName, out user))
            {
                user = UserService.GetUserByName(userName);

                if (user == null) return NoContent();

                InsertOnCache(userName, user);
            }

            return Ok(user);
        }

        [HttpPost()]
        public IActionResult Insert(User user)
        {
            var userInserted = UserService.Insert(user);

            InsertOnCache(userInserted.Name, userInserted);

            return Ok(userInserted);
        }

        [HttpPut()]
        public IActionResult CleanCache()
        {
            CleanAllCache();

            return NoContent();
        }

        private void InsertOnCache(string? key, object value)
        {
            _memoryCache.Set(key, value, new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(10)));
        }

        private void CleanAllCache()
        {
            var users = UserService.GetAll();

            users?.ForEach(user => _memoryCache.Remove(user.Name));
        }
    }
}