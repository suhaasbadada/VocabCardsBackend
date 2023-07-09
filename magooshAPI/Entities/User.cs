using System.Collections.Generic;
using System.Linq;

namespace magooshAPI.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public Dictionary<int, string> WordLearnt { get; set; }

        public User()
        {
            WordLearnt = Enumerable.Range(0, 1067).ToDictionary(i => i, _ => "false");
        }
    }
}
