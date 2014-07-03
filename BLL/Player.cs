using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.Model;
using USAB.IDAL;
using System.Data;

namespace USAB.BLL
{
    public class Player
    {
        // Get an instance of the User DAL using the DALFactory
        // Making this static will cache the DAL instance after the initial load
        private static readonly IPlayer dal = USAB.DALFactory.DataAccess.CreatePlayer();

        public PlayerInfo getPlayer(int id)
        {
            if (id <= 0)
                return null;

            return dal.getPlayer(id);
        }

        public IList<PlayerInfo> getPlayers()
        {
            return dal.getPlayers();
        }

        public DataTable getAllPlayers()
        {
            return dal.getAllPlayers();
        }

        public DataTable getAllPlayers(string level)
        {
            if(string.IsNullOrWhiteSpace(level))
                return null;

            return dal.getAllPlayers(level);
        }
    }
}
