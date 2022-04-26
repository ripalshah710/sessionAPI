using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInfoApi.Helper
{
    public class HeaderValidation
    {
        public static bool ValidateHeaders(IHeaderDictionary headerData, string headerStart)
        {
            string version = headerData.FirstOrDefault(x => x.Key.ToLower() == headerStart.ToLower() + "-Version".ToLower()).Value.FirstOrDefault();
            string sourceapp = headerData.FirstOrDefault(x => x.Key.ToLower() == headerStart.ToLower() + "-SourceApp".ToLower()).Value.FirstOrDefault();
            string token = headerData.FirstOrDefault(x => x.Key.ToLower() == headerStart.ToLower() + "-Token".ToLower()).Value.FirstOrDefault();
            string sessionId = headerData.FirstOrDefault(x => x.Key.ToLower() == headerStart.ToLower() + "-SessionId".ToLower()).Value.FirstOrDefault();
            bool status = false;
            try
            {
                if (version == Util.GetAppSetting(headerStart + "version")
                    && sourceapp == Util.GetAppSetting(headerStart + "sourceapp")
                    && token == Util.GetAppSetting(headerStart + "token")
                    && sessionId == Util.GetAppSetting(headerStart + "sessionid"))
                {
                    status = true;
                }//if
            }//try
            catch
            {
                status = false;
            }//catch
            return status;
        }//ValidateHeaders
    }//HeaderValidation
}
