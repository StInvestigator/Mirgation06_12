using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirgation06_12
{
    public class CommandPlayer
    {
        public int Id { get; set; }
        public int CommandId { get; set; }
        public int PlayerId { get; set; }
        public CommandPlayer(int id, int commandId, int playerId)
        {
            Id = id;
            CommandId = commandId;
            PlayerId = playerId;
        }
    }
}
