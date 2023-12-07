using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class GameGoal
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int CommandId { get; set; }
        public int GoalNumber { get; set; }
        public int PlayerId { get; set; }
        public GameGoal(int id, int gameId,int goalNumber, int playerId, int commandId)
        {
            Id = id;
            GameId = gameId;
            GoalNumber = goalNumber;
            PlayerId = playerId;
            CommandId = commandId;
        }
    }
}
