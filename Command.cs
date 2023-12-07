using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class Command
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public Command(int id, string name, string country) 
        {
            Id = id;
            Name = name;
            Country = country;
        }
    }
}
