using ConsumeAPI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SessionInfoApi.DataAccessLayer;
using SessionInfoApi.Helper;
using SessionInfoAPI;
using System;

namespace SessionInfoApi.Controllers
{

    [ApiController]
    public class SessionController : ControllerBase
    {
        private readonly DbData dbData = new DbData();
        public SessionController(IConfiguration configuration)
        {
            Util.configuration = configuration;
            DbConnection.configuration = configuration;
            DBSqlConnection.configuration = configuration;

        }
        [HttpGet]
        [Route("v1/GetSessionInfo")]
        public ActionResult GetSessionInfo(int sessionId)
        {
            string result = string.Empty;
            try
            {
                if (HeaderValidation.ValidateHeaders(this.Request.Headers, "X"))
                {
                    DbConnection.headerData = this.Request.Headers;
                    if (AuthenticateUser.ValidateUser(this.Request.Headers))
                    {
                        result = dbData.GetSessionData(sessionId);
                    }
                    else
                    {
                        result = "unauthorized user";
                    }
                }
                else
                {
                    result = "unauthorized user";
                }
                return Ok(result);
            }
            catch(Exception ex)
            {
                Logger.Info(">>> SessionInfoController Exception occured: " + ex.Message);
                result = ex.Message;
                return BadRequest(result);
            }//catch  

        }

    }
}
