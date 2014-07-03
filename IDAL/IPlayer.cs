using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.Model;
using System.Data;

namespace USAB.IDAL
{
    public interface IPlayer
    {

        PlayerInfo getPlayer(int id);

        IList<PlayerInfo> getPlayers();

        DataTable getAllPlayers();

        DataTable getAllPlayers(string level);
    }
}
