using Microsoft.Extensions.Configuration;
using SessionInfoApi.Helper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using ConsumeAPI;
using Microsoft.AspNetCore.Http;

namespace SessionInfoApi.DataAccessLayer
{
    public sealed class DbConnection
    {
        public static IConfiguration configuration;
        private DbConnection() { }//DatabaseSingleton
        public static IHeaderDictionary headerData = new HeaderDictionary();

        /// <summary>
        /// Get Connction Instance
        /// </summary>
        public static SqlConnection GetInstance
        {   get
            {
                if (Util.GetAppSetting("isLocalDBConn").ToLower().Equals("true"))
                {
                    string conn = Util.GetConncetionStringValue("dbconnection");
                    return new SqlConnection(conn);
                }//if
                else
                {
                    var dbSqlConnection = DBSqlConnection.GetConnectionAPI(headerData).ConfigureAwait(false).GetAwaiter().GetResult();
                    return new SqlConnection(dbSqlConnection.DBConnectionsList[0].ConnectionString);
                }//else
            }//get
        }//GetInstance
    }//DatabaseSingleton
}
