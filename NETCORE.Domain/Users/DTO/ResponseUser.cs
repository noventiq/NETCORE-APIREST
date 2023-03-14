using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NETCORE.Domain.Users.DTO
{
    public class ResponseUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Lastname{ get; set; }
    }
}
