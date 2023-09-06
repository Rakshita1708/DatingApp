using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTO
{
    public class UserDto
    {
        //returning our username and token
        public string Username { get; set; }

        public string Token { get; set; }
    }
}