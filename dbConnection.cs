using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DBEditor
{
    class dbConnection
    {
        public SqlConnection conn;
        public SqlTransaction transaction;
        public dbConnection()  // constructor Function
        {
            string strProject = "199.192.79.74"; //Enter your SQL server instance name
            string strDatabase = "lc_db_auth"; //Enter your database name
            string strUserID = "andrew"; // Enter your SQL Server User Name
            string strPassword = "56M4eVDkRroTYyPptjRJyKU2QLcx8Z"; // Enter your SQL Server Password
            string strconn = "data source=" + strProject +
              ";Persist Security Info=false;database=" + strDatabase +
              ";user id=" + strUserID + ";password=" +
              strPassword + ";Connection Timeout = 0";
            conn = new SqlConnection(strconn);
        }
        public void openConnection() // Open database Connection
        {
            conn.Close();
            conn.Open();
            transaction = conn.BeginTransaction();
        }
        public void closeConnection() // database connection close
        {
            transaction.Commit();
            conn.Close();
        }
        public void errorTransaction()
        {
            transaction.Rollback();
            conn.Close();
        }
        protected void ExecuteSQL(string sSQL)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn);
            cmd.ExecuteNonQuery();
        }
        protected DataSet FillData(string sSQL, string sTable)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn, transaction);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adapter.Fill(ds, sTable);
            return ds;
        }
        protected SqlDataReader setDataReader(string sSQL)
        {
            SqlCommand cmd = new SqlCommand(sSQL, conn, transaction);
            cmd.CommandTimeout = 300;
            SqlDataReader rtnReader;
            rtnReader = cmd.ExecuteReader();
            return rtnReader;
        }   
    }
}
