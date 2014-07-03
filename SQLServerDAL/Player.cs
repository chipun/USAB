using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using USAB.IDAL;
using USAB.Model;
using USAB.DBUtility;


namespace USAB.SQLServerDAL
{
    public class Player : IPlayer
    {

        
        private string PARM_ID = "@PlayerID";
        private string PARM_LEVEL = "@LEVEL";

        private string SP_SEL_PLAYERBYID = "SP_SEL_PLAYERBYID";
        private string SP_SELECT_PLAYERS = "SP_SEL_PLAYERS";
        private string SP_SELECT_PLAYERS_BY_LEVEL = "SP_SEL_PLAYERS_BY_LEVEL";
        private string usab_sp_create_user;
   
        public PlayerInfo getPlayer(int id)
        {
            PlayerInfo user = null;

            OdbcParameter parm = new OdbcParameter(PARM_ID, OdbcType.Int);
            parm.Value = id;

            OdbcCommand cmd = new OdbcCommand("{call " + SP_SEL_PLAYERBYID + "(?)}");

            cmd.Parameters.Add(parm);
            cmd.CommandType = CommandType.StoredProcedure;

            using (OdbcDataReader rdr = SqlHelper.ExecuteReader(cmd, SqlHelper.USABConnectionString))
            {
                if (rdr.Read())
                {
                    user = new PlayerInfo();
                    user.id = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                    user.firstname = rdr.IsDBNull(1) ? "" : rdr.GetString(1);
                    user.lastname = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                    user.position = rdr.IsDBNull(3) ? "" : rdr.GetString(3);
                    user.height = rdr.IsDBNull(4) ? "" : rdr.GetString(4);
                    user.weight = rdr.IsDBNull(5) ? "" : rdr.GetString(5);
                    user.city = rdr.IsDBNull(6) ? "" : rdr.GetString(6);
                    user.state = rdr.IsDBNull(7) ? "" : rdr.GetString(7);
                    user.birthdate = rdr.IsDBNull(8) ? "" : rdr.GetString(8);
                    user.year = rdr.IsDBNull(9) ? "" : rdr.GetString(9);
                    user.bio = rdr.IsDBNull(10) ? "" : rdr.GetString(10);
                    user.jersey_nbr = rdr.IsDBNull(11) ? "" : rdr.GetString(11);
                }
                else
                {
                    user = null;
                }
            }

            return user;
        }

        public IList<PlayerInfo> getPlayers()
        {

            IList<PlayerInfo> players = new List<PlayerInfo>();

            OdbcCommand cmd = new OdbcCommand("{call " + SP_SELECT_PLAYERS + "}");
            cmd.CommandType = CommandType.StoredProcedure;

            using (OdbcDataReader rdr = SqlHelper.ExecuteReader(cmd, SqlHelper.USABConnectionString))
            {
                while (rdr.Read())
                {
                    PlayerInfo user = new PlayerInfo();
                    user.id = rdr.IsDBNull(0) ? 0 : rdr.GetInt32(0);
                    user.firstname = rdr.IsDBNull(1) ? "" : rdr.GetString(1);
                    user.lastname = rdr.IsDBNull(2) ? "" : rdr.GetString(2);
                    user.position = rdr.IsDBNull(3) ? "" : rdr.GetString(3);
                    user.height = rdr.IsDBNull(4) ? "" : rdr.GetString(4);
                    user.weight = rdr.IsDBNull(5) ? "" : rdr.GetString(5);
                    user.city = rdr.IsDBNull(6) ? "" : rdr.GetString(6);
                    user.state = rdr.IsDBNull(7) ? "" : rdr.GetString(7);
                    //user.birthdate = rdr.IsDBNull(8) ? "" : rdr.GetString(8);
                    //user.year = rdr.IsDBNull(9) ? "" : rdr.GetString(9);
                    //user.bio = rdr.IsDBNull(10) ? "" : rdr.GetString(10);
                    //user.jersey_nbr = rdr.IsDBNull(11) ? "" : rdr.GetString(11);

                    players.Add(user);
                }

            }

            return players;
        }

        public DataTable getAllPlayers()
        {
            DataTable dt = new DataTable();
            SqlHelper.FillData(ref dt, CommandType.StoredProcedure, "{CALL " + SP_SELECT_PLAYERS + "}");
            return dt;

        }
        public DataTable getAllPlayers(string level)
        {
            OdbcParameter parm = new OdbcParameter(PARM_LEVEL, OdbcType.VarChar,50);
            parm.Value = level;

            List<OdbcParameter> parms = new List<OdbcParameter>();
            parms.Add(parm);

            DataTable dt = new DataTable();
            SqlHelper.FillData(ref dt, CommandType.StoredProcedure, "{CALL " + SP_SELECT_PLAYERS_BY_LEVEL + "{?}", parms.ToArray());
            return dt;
        }

    }
}
