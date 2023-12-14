using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class StudioCountry
    {
        public int Id { set; get; }
        public int CreatorId { set; get; }
        public string Name { set; get; }
        public StudioCountry() { }
        public StudioCountry(int id, int creatorId, string name) { Id = id; CreatorId = creatorId; Name = name; }
    }
}
