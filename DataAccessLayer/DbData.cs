using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using SessionInfoApi.Helper;

namespace SessionInfoApi.DataAccessLayer
{
    public class DbData
    {
        public string GetSessionData(int sessionId)
        {
            JsonSerializer jsonSerializer = new JsonSerializer();
            DataSet ds = new DataSet();            
            SqlCommand cmd = new SqlCommand("[p.objectModel.Session.LoadAll_1]", DbConnection.GetInstance);
            cmd.CommandType = CommandType.StoredProcedure;         
            cmd.Parameters.AddWithValue("@session_id", sessionId);
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            sqlDataAdapter.Fill(ds);
           return jsonSerializer.ConvertToJson(ds);
        }
    }
}
