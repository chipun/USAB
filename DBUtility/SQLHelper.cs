//===============================================================================
// This file is based on the Microsoft Data Access Application Block for .NET
// For more information please go to 
// http://msdn.microsoft.com/library/en-us/dnbda/html/daab-rm.asp
//===============================================================================

using System;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using System.Collections;

namespace USAB.DBUtility
{
    /// <summary>
    /// The SqlHelper class is intended to encapsulate high performance, 
    /// scalable best practices for common uses of SqlClient.
    /// </summary>
    public abstract class SqlHelper
    {

        //Database connection strings
        public static readonly string USABConnectionString = ConfigurationManager.ConnectionStrings["USABWarriorsDBConnectionString"].ConnectionString;

        // Hashtable to store cached parameters
        private static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        /// <summary>
        /// Execute a OdbcCommand (that returns no resultset) against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a OdbcConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(string connectionString, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {

            OdbcCommand cmd = new OdbcCommand();

            using (OdbcConnection conn = new OdbcConnection(connectionString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                int val = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a OdbcCommand (that returns no resultset) against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OdbcConnection connection, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {

            OdbcCommand cmd = new OdbcCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a OdbcCommand (that returns no resultset) using an existing SQL Transaction 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  int result = ExecuteNonQuery(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="trans">an existing sql transaction</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>an int representing the number of rows affected by the command</returns>
        public static int ExecuteNonQuery(OdbcTransaction trans, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {
            OdbcCommand cmd = new OdbcCommand();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, commandParameters);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// Execute a OdbcCommand that returns a resultset against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  SqlDataReader r = ExecuteReader(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a OdbcConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>A SqlDataReader containing the results</returns>
        public static OdbcDataReader ExecuteReader(string connectionString, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {
            OdbcCommand cmd = new OdbcCommand();
            OdbcConnection conn = new OdbcConnection(connectionString);

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OdbcDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        public static OdbcDataReader ExecuteReader(OdbcCommand cmd, string connectionString)
        {
            //OdbcCommand cmd = new OdbcCommand();
            OdbcConnection conn = new OdbcConnection(connectionString);
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;

            // we use a try/catch here because if the method throws an exception we want to 
            // close the connection throw code, because no datareader will exist, hence the 
            // commandBehaviour.CloseConnection will not work
            try
            {
                //PrepareCommand(cmd, conn, null, cmdType, cmdText, commandParameters);
                OdbcDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return rdr;
            }
            catch
            {
                conn.Close();
                throw;
            }
        }

        /// <summary>
        /// Execute a OdbcCommand that returns the first column of the first record against the database specified in the connection string 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="connectionString">a valid connection string for a OdbcConnection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(string connectionString, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {
            OdbcCommand cmd = new OdbcCommand();

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
                object val = cmd.ExecuteScalar();
                cmd.Parameters.Clear();
                return val;
            }
        }

        /// <summary>
        /// Execute a OdbcCommand that returns the first column of the first record against an existing database connection 
        /// using the provided parameters.
        /// </summary>
        /// <remarks>
        /// e.g.:  
        ///  Object obj = ExecuteScalar(connString, CommandType.StoredProcedure, "PublishOrders", new OdbcParameter("@prodid", 24));
        /// </remarks>
        /// <param name="conn">an existing database connection</param>
        /// <param name="commandType">the CommandType (stored procedure, text, etc.)</param>
        /// <param name="commandText">the stored procedure name or T-SQL command</param>
        /// <param name="commandParameters">an array of SqlParamters used to execute the command</param>
        /// <returns>An object that should be converted to the expected type using Convert.To{Type}</returns>
        public static object ExecuteScalar(OdbcConnection connection, CommandType cmdType, string cmdText, params OdbcParameter[] commandParameters)
        {

            OdbcCommand cmd = new OdbcCommand();

            PrepareCommand(cmd, connection, null, cmdType, cmdText, commandParameters);
            object val = cmd.ExecuteScalar();
            cmd.Parameters.Clear();
            return val;
        }

        /// <summary>
        /// add parameter array to the cache
        /// </summary>
        /// <param name="cacheKey">Key to the parameter cache</param>
        /// <param name="cmdParms">an array of SqlParamters to be cached</param>
        public static void CacheParameters(string cacheKey, params OdbcParameter[] commandParameters)
        {
            parmCache[cacheKey] = commandParameters;
        }

        /// <summary>
        /// Retrieve cached parameters
        /// </summary>
        /// <param name="cacheKey">key used to lookup parameters</param>
        /// <returns>Cached SqlParamters array</returns>
        public static OdbcParameter[] GetCachedParameters(string cacheKey)
        {
            OdbcParameter[] cachedParms = (OdbcParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            OdbcParameter[] clonedParms = new OdbcParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (OdbcParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        /// <summary>
        /// Prepare a command for execution
        /// </summary>
        /// <param name="cmd">OdbcCommand object</param>
        /// <param name="conn">OdbcConnection object</param>
        /// <param name="trans">SqlTransaction object</param>
        /// <param name="cmdType">Cmd type e.g. stored procedure or text</param>
        /// <param name="cmdText">Command text, e.g. Select * from Products</param>
        /// <param name="cmdParms">OdbcParameters to use in the command</param>
        private static void PrepareCommand(OdbcCommand cmd, OdbcConnection conn, OdbcTransaction trans, CommandType cmdType, string cmdText, OdbcParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OdbcParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }


        public static void FillData(ref DataTable DataTable, CommandType commandType, string commandText, params OdbcParameter[] commandParameters)
        {
            DataSet ds = new DataSet();
            FillData(ref ds, commandType, commandText, commandParameters);
            if (ds != null && ds.Tables.Count > 0)
            {
                DataTable = ds.Tables[0];
            }
        }

        public static void FillData(ref DataSet DataSet, CommandType commandType, string commandText, params OdbcParameter[] commandParameters)
        {
            OdbcCommand cmd = new OdbcCommand();
            OdbcDataAdapter da = default(OdbcDataAdapter);
            OdbcConnection connection = new OdbcConnection();
            try
            {
                PrepareCommand(connection, cmd, null, commandType, commandText, commandParameters);
                da = new OdbcDataAdapter(cmd);
                da.Fill(DataSet);
                cmd.Parameters.Clear();
            }
            catch (Exception ex)
            {
                ex.Data.Add("Command Text", commandText);
                throw;
            }
            finally
            {
                if ((cmd != null))
                    cmd.Dispose();
                if ((connection != null))
                    connection.Dispose();
            }
        }

        private static void PrepareCommand(OdbcConnection conn, OdbcCommand cmd, OdbcTransaction trans,
           CommandType cmdType, string cmdText, OdbcParameter[] cmdParms)
        {
            conn.ConnectionString = USABConnectionString;
            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            trans = conn.BeginTransaction();
            cmd.CommandText = cmdText;

            if (trans != null)
                cmd.Transaction = trans;

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (OdbcParameter parm in cmdParms)
                    cmd.Parameters.Add(parm);
            }
        }
    
    }
}
