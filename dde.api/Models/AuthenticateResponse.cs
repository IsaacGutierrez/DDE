using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dde.api.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            this.FullName = user.FullName;
            this.Username = user.Username;
            Token = token;
        }
    }
}
