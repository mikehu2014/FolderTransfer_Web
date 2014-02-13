using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using System.Diagnostics;
namespace IMSite.Data
{
    /// <summary>
    /// Conn 的摘要说明
    /// </summary>
    public class Conn
    {
        static string connectionString = string.Empty;

        static System.Collections.Generic.Dictionary<String, String> dicConn = new System.Collections.Generic.Dictionary<string, string>();

        SqlCommand dbCommand;

        bool _storeParams = false;

        #region 字段

        public bool IsStoreParams
        {
            get { return _storeParams; }
            set { _storeParams = value; }
        }

        public CommandType CommandType
        {
            get { return dbCommand.CommandType; }
            set { dbCommand.CommandType = value; }
        }

        public void ClearParams()
        {
            dbCommand.Parameters.Clear();
        }

        public static string ConnectionString
        {
            get {return Conn.connectionString; }
            set { Conn.connectionString = value; }
        }
        //private static SqlConnection connection;
        #endregion

        #region 构造函数

        public Conn()
        {
            //InitConnString();
            dbCommand = CreateDb().CreateCommand();
        }

        public Conn(String connKey)
        {
            //InitConnString();
            if (!dicConn.ContainsKey(connKey))
            {
                AddConnString(connKey);
            }
            string connstring = dicConn[connKey];
            dbCommand = CreateDb(connstring).CreateCommand();
        }
        #endregion

        #region 内部静态函数
        static void InitConnString()
        {
            lock (dicConn)
            {
                if (dicConn.Count == 0)
                {
                    //AddConnString("Chat4SupportConnectionString");
                    //AddConnString("statdb");
                    //connectionString = dicConn["Chat4SupportConnectionString"];
                }
            }
        }

        /// <summary>
        /// 创建一个SqlCommand，并打开数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static SqlCommand CreateSqlCommand(string sql, ArrayList list, SqlTransaction tran)
        {
            SqlConnection dbConnection;
            if (tran != null)
                dbConnection = tran.Connection;
            else
                dbConnection = Conn.CreateDb();

            SqlCommand dbCommand = new System.Data.SqlClient.SqlCommand(sql, dbConnection, tran);
            if (list != null)
            {
                foreach (SqlParameter p in list)
                {
                    dbCommand.Parameters.Add(p);
                }
            }
            if (dbConnection.State != ConnectionState.Open)
                dbConnection.Open();
            return dbCommand;
        }

        /// <summary>
        /// 创建一个SqlCommand，并打开数据库
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        private static SqlCommand CreateSqlCommand(string sql, ArrayList list)
        {
            return CreateSqlCommand(sql, list, null);
        }
        #endregion

        #region 动态函数

        public DbCommand CreateCommand()
        {
            return (DbCommand)dbCommand.Connection.CreateCommand();
        }

        public object ExecuteScalarD(string sql)
        {
            dbCommand.CommandText = sql;
            dbCommand.Connection.Open();
            object obj;
            try
            {
                obj = dbCommand.ExecuteScalar();
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return obj;
        }

        public int ExecuteNonQueryD(string sql)
        {
            dbCommand.CommandText = sql;
            dbCommand.Connection.Open();
            int obj;
            try
            {
                obj = dbCommand.ExecuteNonQuery();
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return obj;
        }

        public int ExecuteNonQueryNotClose(string sql)
        {
            dbCommand.CommandText = sql;
            
            int obj;
            try
            {
                obj = dbCommand.ExecuteNonQuery();
            }
            finally
            {
            
            }
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return obj;
        }

        public SqlDataReader ExecuteReaderD(string sql)
        {
            dbCommand.Connection.Open();
            dbCommand.CommandText = sql;
            SqlDataReader dr = dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return dr;
          
        }

        public SqlDataReader ExecuteReader(string sql, CommandType cmdtype, CommandBehavior cmdbehavior)
        {
            dbCommand.Connection.Open();
            dbCommand.CommandText = sql;
            dbCommand.CommandType = cmdtype;
            SqlDataReader dr = dbCommand.ExecuteReader(cmdbehavior);
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return dr;
        }

        public DataTable ExecuteDataTable(string sql, bool isSp)
        {
            if (isSp)
                dbCommand.CommandType = CommandType.StoredProcedure;
            else
                dbCommand.CommandType = CommandType.Text;

            dbCommand.CommandText = sql;
            SqlDataAdapter sqlda = new SqlDataAdapter(dbCommand);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);
            dbCommand.Connection.Close();
            if (!_storeParams)
                dbCommand.Parameters.Clear();
            return ds.Tables[0];
        }

        public SqlParameter AddParamWithValue(String name, object value)
        {
            return dbCommand.Parameters.AddWithValue(name, value);
        }

        public void SetParamWithValue(String name, object value)
        {
            dbCommand.Parameters[name].Value = value;
        }

        public SqlParameter AddParam(String name, SqlDbType type)
        {
            return dbCommand.Parameters.Add(name, type);
        }
        #endregion

        #region 静态方法
        //创建一个新的连接
        public static SqlConnection CreateDb()
        {
            return new System.Data.SqlClient.SqlConnection(connectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connstr">连接字符串</param>
        /// <returns>sqlconnection</returns>
        public static SqlConnection CreateDb(string connstr)
        {
            return new System.Data.SqlClient.SqlConnection(connstr);
        }

        public static SqlDataReader ExecuteReader(string sql, ArrayList list)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, list);
            return  dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader ExecuteReaderSP(string sql,ArrayList paramlist)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, paramlist);
            dbCommand.CommandType = CommandType.StoredProcedure;
            return dbCommand.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader ExecuteReader(string sql)
        {
            return ExecuteReader(sql, null);
        }

        public static int ExecuteNonQuerySP(string spName, ArrayList list)
        {
            SqlCommand dbCommand = CreateSqlCommand(spName, list);
            dbCommand.CommandType = CommandType.StoredProcedure;
            int i = 0;
            try
            {
                i = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            return i;
        }


        public static int ExecuteNonQuery(string sql, ArrayList list)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, list, null);
            int i = 0;
            try
            {
                i = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            return i;
        }

        public static int ExecuteNonQuery(string sql, ArrayList list,SqlTransaction tran)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, list, tran);
            int i = 0;
            try
            {
                i = dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //tran.Rollback();
                throw ex;
                //EventLog.WriteEvent("");
            }
            return i;
        }

        public static object ExecuteScalarSP(string sql,ArrayList list)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, list);
            dbCommand.CommandType = CommandType.StoredProcedure;
            object obj;
            try
            {
                obj = dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            return obj;
        }

        public static object ExecuteScalar(string sql)
        {
            return ExecuteScalar(sql,null);
        }

        public static object ExecuteScalar(string sql, ArrayList list,SqlTransaction tran)
        {
            if (tran == null)
                return ExecuteScalar(sql, list);
            else
            {
                SqlCommand dbCommand = CreateSqlCommand(sql, list, tran);
                object obj = null;
                try
                {
                    obj = dbCommand.ExecuteScalar();
                }
                catch(Exception ex)
                {
                    //tran.Rollback();
                    throw ex;
                }
                dbCommand.Parameters.Clear();
                dbCommand.Dispose();
                return obj;
            }
        }

        public static object ExecuteScalar(string sql, ArrayList list)
        {
            SqlCommand dbCommand = CreateSqlCommand(sql, list);
            object obj;
            try
            {
                obj = dbCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dbCommand.Connection.Close();
            }
            dbCommand.Parameters.Clear();
            dbCommand.Dispose();
            return obj;
        }

        public static int ExecuteNonQuery(string queryString)
        {
            return ExecuteNonQuery(queryString, null);
        }

        public static DataSet ExecuteDataSet(string sql, ArrayList paramList)
        {
            return ExecuteDataSet(sql, paramList, false);
        }

        public static DataSet ExecuteDataSet(string sql, ArrayList paramList,bool isSP)
        {
            SqlCommand cmd = CreateSqlCommand(sql, paramList);

            if (isSP)
                cmd.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlda.Fill(ds);
            cmd.Connection.Close();
            ds.Locale = System.Threading.Thread.CurrentThread.CurrentUICulture;
            return ds;
        }

        //添加数据库连接
        public static void AddConnString(String key)
        {
            dicConn.Add(key, ConfigurationManager.ConnectionStrings[key].ConnectionString);
        }

        //更改数据库连接
        public static void ChangeConnString(String key)
        {
            connectionString = dicConn[key];
        }
        #endregion
    }
}