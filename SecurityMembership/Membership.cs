using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.DBUtility;

namespace USAB.SecurityMembership
{
    public class Membership
    {
        DB_SPs sp = new DB_SPs();

        public bool createUser(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(password))
                return false;

            OdbcParameter[] parms = new OdbcParameter[] 
            {
                new OdbcParameter("@EMAIL", OdbcType.VarChar, 50),
                new OdbcParameter("@PASSWORD", OdbcType.VarChar, 50),
                new OdbcParameter("@BOOL", OdbcType.Bit)

            };

            parms[0].Value = email;
            parms[1].Value = password;
            parms[2].Direction = ParameterDirection.Output;

            OdbcCommand cmd = new OdbcCommand("{call " + sp.usab_sp_create_user + "(?,?,?)}");
            foreach (OdbcParameter parm in parms)
                cmd.Parameters.Add(parm);

            // Create the connection to the database
            using (OdbcConnection conn = new OdbcConnection(SqlHelper.USABConnectionString))
            {
                conn.Open();

                using (OdbcTransaction trans = conn.BeginTransaction())
                {
                    cmd.Connection = conn;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Transaction = trans;
                    try
                    {
                        int num = cmd.ExecuteNonQuery();

                        trans.Commit();

                        bool access = Convert.ToBoolean(cmd.Parameters[2].Value);

                        return access;

                    }
                    catch (InvalidOperationException e)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            
        }

    }
}
