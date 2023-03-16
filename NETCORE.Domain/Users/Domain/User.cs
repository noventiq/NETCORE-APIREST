using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace NETCORE.Domain.Users.Domain
{
    public class User
    {
        public int Id { get; set; }
        [Column("email")]
        public string Name { get; set; }
        public string Lastname { get; set; }
        public int Age { get; set; }
        public Profile Profile { get; set; }
        public List<Rol> Roles { get; set; }
    }
}
