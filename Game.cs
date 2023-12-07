using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class Game
    {
        public int Id { get; set; }
        public int Command1Id { get; set; }
        public int Command2Id { get; set; }
        public int Command1Goals { get; set; }
        public int Command2Goals { get; set; }
        public DateTime Date { get; set; }
        public Game(int id, int command1Id, int command2Id, int command1Goals, int command2Goals, DateTime date)
        {
            Id = id;
            Command1Id = command1Id;
            Command2Id = command2Id;
            Command1Goals = command1Goals;
            Command2Goals = command2Goals;
            Date = date;
        }
    }
}
