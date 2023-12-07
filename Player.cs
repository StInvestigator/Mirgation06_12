using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IngameNumber { get; set; }
        public string Position { get; set; }
        public Player(int id, string name,int ingameNumber, string position)
        {
            Id = id;
            Name = name;
            IngameNumber = ingameNumber;
            Position = position;
        }
    }
}
