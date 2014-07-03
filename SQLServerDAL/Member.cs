using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USAB.Model;
using USAB.IDAL;
using USAB.DBUtility;
using System.Data.Odbc;
using System.Data;

namespace USAB.SQLServerDAL
{
    public class Member : IMember
    {

        private string PARM_ID = "@member_id";

        private string SP_SEL_MEMBER_BY_ID = "USAB_SP_SEL_MEMBER_BY_ID";
        private string SP_ACCESS_MEMBER = "USAB_SP_ACCESS_MEMBER";
        private string SP_UPDATE_MEMBER = "USAB_SP_UP_MEMBER";
        private string SP_SAVE_MEMBER = "USAB_SP_INS_MEMBER";

        private string PARM_MEMBER_ID = "@MEMBER_ID";
        private string PARM_FIRSTNAME = "@FIRSTNAME";
        private string PARM_LASTNAME = "@LASTNAME";
        private string PARM_USERNAME = "@USERNAME";
        private string PARM_PASSWORD = "@PASSWORD";
        private string PARM_ADDRESS1 = "@ADDRESS1";
        private string PARM_ADDRESS2 = "@ADDRESS2";
        private string PARM_CITY = "@CITY ";
        private string PARM_STATE = "@LASTNAME";
        private string PARM_ZIP = "@ZIP";
        private string PARM_BIRTHDATE = "@BIRTHDATE";
        private string PARM_MIDDLE_NAME = "@MIDDLE_NAME";
        private string PARM_ROLE = "@ROLE";
        private string PARM_STATUS = "@STATUS";
        private string PARM_EMAIL= "@EMAIL";

        public MemberInfo getMember(int id)
        {
            MemberInfo setMemberInfo = new MemberInfo();

            OdbcParameter parm = new OdbcParameter(PARM_ID, OdbcType.Int);
            parm.Value = id;

            OdbcCommand cmd = new OdbcCommand("{call " + SP_SEL_MEMBER_BY_ID + "(?)}");

            cmd.Parameters.Add(parm);
            cmd.CommandType = CommandType.StoredProcedure;

            using (OdbcDataReader rdr = SqlHelper.ExecuteReader(cmd, SqlHelper.USABConnectionString))
            {
                if (rdr.Read())
                {
                    setMemberInfo.member_id = rdr.GetInt16(0);
                    setMemberInfo.first_name = rdr.IsDBNull(1) ? string.Empty : rdr.GetString(1);
                    setMemberInfo.last_name = rdr.IsDBNull(2) ? string.Empty : rdr.GetString(2);
                    setMemberInfo.middle_name = rdr.IsDBNull(3) ? string.Empty : rdr.GetString(3);
                    setMemberInfo.email = rdr.IsDBNull(4) ? string.Empty : rdr.GetString(4);
                    setMemberInfo.role = rdr.IsDBNull(5) ? string.Empty : rdr.GetString(5);
                    setMemberInfo.status = rdr.IsDBNull(6) ? string.Empty : rdr.GetString(6);
                    setMemberInfo.address1 = rdr.IsDBNull(7) ? string.Empty : rdr.GetString(7);
                    setMemberInfo.address2 = rdr.IsDBNull(8) ? string.Empty : rdr.GetString(8);
                    setMemberInfo.city = rdr.IsDBNull(9) ? string.Empty : rdr.GetString(9);
                    setMemberInfo.state = rdr.IsDBNull(10) ? string.Empty : rdr.GetString(10);
                    setMemberInfo.zip = rdr.IsDBNull(11) ? string.Empty : rdr.GetString(11);
                    setMemberInfo.birthdate = rdr.IsDBNull(12) ? string.Empty : rdr.GetString(12);

                }
                else
                {

                    setMemberInfo.member_id = 0;
                    setMemberInfo.first_name = string.Empty;
                    setMemberInfo.last_name = string.Empty;
                    setMemberInfo.middle_name = string.Empty;
                    setMemberInfo.email = string.Empty;
                    setMemberInfo.role = string.Empty;
                    setMemberInfo.status = string.Empty;
                    setMemberInfo.address1 = string.Empty;
                    setMemberInfo.address2 = string.Empty;
                    setMemberInfo.city = string.Empty;
                    setMemberInfo.state = string.Empty;
                    setMemberInfo.zip = string.Empty;
                    setMemberInfo.birthdate = string.Empty;
                }
            }

            return setMemberInfo;
        }

        public bool SecurityMember(string email, string password)
        {
            OdbcParameter[] parms = new OdbcParameter[] 
            {
                new OdbcParameter(PARM_EMAIL, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_PASSWORD, OdbcType.VarChar, 50),
                new OdbcParameter("@BOOL", OdbcType.Bit),

            };

            parms[0].Value = email;
            parms[1].Value = password;
            parms[2].Direction = ParameterDirection.Output;



            OdbcCommand cmd = new OdbcCommand("{call " + SP_ACCESS_MEMBER + "(?,?,?)}");
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

        public bool updateMember(MemberInfo member)
        {
            OdbcParameter[] parms = new OdbcParameter[] 
            {
                new OdbcParameter(PARM_MEMBER_ID, OdbcType.Int),
                new OdbcParameter(PARM_USERNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_PASSWORD, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_FIRSTNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_LASTNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ADDRESS1, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ADDRESS2, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_CITY, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_STATE, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ZIP, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_BIRTHDATE, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_MIDDLE_NAME, OdbcType.VarChar, 50),

            };

            parms[0].Value = member.member_id;
            parms[1].Value = member.username;
            parms[2].Value = member.password;
            parms[3].Value = member.first_name;
            parms[4].Value = member.last_name;
            parms[5].Value = member.address1;
            parms[6].Value = member.address2;
            parms[7].Value = member.city;
            parms[8].Value = member.state;
            parms[9].Value = member.zip;
            parms[10].Value = member.birthdate;
            parms[11].Value = member.middle_name;

            OdbcCommand cmd = new OdbcCommand("{call " + SP_UPDATE_MEMBER + "(?,?,?,?,?,?,?,?,?,?,?,?)}");

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


                        if (num > 0)
                            return true;

                        return false;

                    }
                    catch (InvalidOperationException e)
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
        }

        public bool saveMember(MemberInfo member)
        {
            OdbcParameter[] parms = new OdbcParameter[] 
            {
                new OdbcParameter(PARM_MEMBER_ID, OdbcType.Int),
                new OdbcParameter(PARM_USERNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_PASSWORD, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_FIRSTNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_LASTNAME, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ADDRESS1, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ADDRESS2, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_CITY, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_STATE, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ZIP, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_BIRTHDATE, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_ROLE, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_STATUS, OdbcType.VarChar, 50),
                new OdbcParameter(PARM_EMAIL, OdbcType.VarChar, 50),

            };

            parms[0].Value = member.member_id;
            parms[1].Value = member.username;
            parms[2].Value = member.password;
            parms[3].Value = member.first_name;
            parms[4].Value = member.last_name;
            parms[5].Value = member.address1;
            parms[6].Value = member.address2;
            parms[7].Value = member.city;
            parms[8].Value = member.state;
            parms[9].Value = member.zip;
            parms[10].Value = member.birthdate;
            parms[11].Value = "user";
            parms[12].Value = "A";
            parms[13].Value = member.email;


            parms[0].Direction = ParameterDirection.Output;

            OdbcCommand cmd = new OdbcCommand("{call " + SP_SAVE_MEMBER + "(?,?,?,?,?,?,?,?,?,?,?,?,?,?)}");

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
                        int id;

                        bool b = int.TryParse(cmd.Parameters[0].Value.ToString(), out id);
                        if (b) return true;
                        return false;

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
