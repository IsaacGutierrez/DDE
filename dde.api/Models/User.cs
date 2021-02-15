using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }

        [JsonIgnore]
        public string Password { get; set; }

        public User()
        {

        }


        public User(int Id, string FullName, string UserName)
        {
            this.Id = Id;
            this.FullName = FullName;
            this.Username = UserName;
        }
    }
}
