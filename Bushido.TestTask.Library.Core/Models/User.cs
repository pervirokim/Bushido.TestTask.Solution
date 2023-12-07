using Bushido.TestTask.Library.Core.Models;
using Newtonsoft.Json;

namespace Bushido.TestTask.Cloud.Authentication.Models.DBModel
{
    public class User : BaseDBModel
    {
        public User(string email, string firstname, string lastname, string passwordhash) :base()
        {
            Email = email;
            Firstname = firstname;  
            LastName = lastname;
            Passwordhash = passwordhash;
            LastLoginDate = DateTime.UtcNow;
        }

        public User() : base()
        {
            
        }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("passwordhash")]
        public string Passwordhash { get; set; }

        [JsonProperty("firstname")]
        public string? Firstname { get; set; }

        [JsonProperty("lastname")]
        public string? LastName { get; set; }

        [JsonProperty("lastLoginDate")]
        public DateTime? LastLoginDate { get; set; }

        //we also can include avatar, eneilverified and other properties based on oauth2, but for test task in my mind that doesnt metter :)
    }
}
