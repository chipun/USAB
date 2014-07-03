using System.Reflection;
using System.Configuration;
using USAB.IDAL;


namespace USAB.DALFactory
{
    public sealed class DataAccess
    {
        // Look up the DAL implementation we should be using
        private static readonly string path = ConfigurationManager.AppSettings["WebDAL"];

        private DataAccess() { }

        public static IPlayer CreatePlayer()
        {
            string className = path + ".Player";
            return (IPlayer)Assembly.Load(path).CreateInstance(className);
        }

        public static IMember CreateMember()
        {
            string className = path + ".Member";
            return (IMember)Assembly.Load(path).CreateInstance(className);
        }

        
    }
}
