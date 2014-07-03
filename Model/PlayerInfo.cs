using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace USAB.Model
{
    public class PlayerInfo
    {
        public PlayerInfo() { }


        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string height { get; set; }
        public string weight { get; set; }
        public string city { get; set; }
        public string birthdate { get; set; }
        public string state { get; set; }
        public string year { get; set; }
        public string bio { get; set; }
        public string jersey_nbr { get; set; }


        public string position { get; set; }
    }
}
