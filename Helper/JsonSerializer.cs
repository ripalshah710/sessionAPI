using Newtonsoft.Json;
using SessionInfoApi.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInfoApi.Helper
{
    public class JsonSerializer
    {
        public  string ConvertToJson(DataSet dataSet)
        {
            var sessionBasicDetails = CommonMethod.ConvertToList<SessionBasicDetails>(dataSet.Tables[0]);
            var sessionCredits = CommonMethod.ConvertToList<SessionCredits>(dataSet.Tables[1]);
            var sessionPresenters = CommonMethod.ConvertToList<SessionPresenter>(dataSet.Tables[2]);
            var sessionExclusiveItems = CommonMethod.ConvertToList<SessionExclusiveItems>(dataSet.Tables[3]);
            var sessionLocations = CommonMethod.ConvertToList<SessionLocation>(dataSet.Tables[4]);
            var sessionMemberGroups = CommonMethod.ConvertToList<SessionMemberGroup>(dataSet.Tables[5]);
            var sessionSchedules = CommonMethod.ConvertToList<SessionSchedule>(dataSet.Tables[6]);
            var sessionPromoCodes = CommonMethod.ConvertToList<SessionPromoCode>(dataSet.Tables[7]);
            var sessionDiscounts = CommonMethod.ConvertToList<SessionDiscount>(dataSet.Tables[8]);
            var sessionImages = CommonMethod.ConvertToList<SessionImage>(dataSet.Tables[9]);
            SessionInfo sessionInfo = new SessionInfo();
            sessionInfo.basicDetails = sessionBasicDetails.FirstOrDefault();
            sessionInfo.credits = sessionCredits;
            sessionInfo.presenterSid = sessionPresenters.FirstOrDefault();
            sessionInfo.exclusiveItems = sessionExclusiveItems.FirstOrDefault();
            sessionInfo.location = sessionLocations.FirstOrDefault();
            sessionInfo.memberGroup = sessionMemberGroups.FirstOrDefault();
            sessionInfo.schedule = sessionSchedules;
            sessionInfo.promo = sessionPromoCodes.FirstOrDefault();
            sessionInfo.discount = sessionDiscounts.FirstOrDefault();
            sessionInfo.sessionImage = sessionImages.FirstOrDefault();
            var data= JsonConvert.SerializeObject(sessionInfo);
            return data;

        }
        public static class CommonMethod
        {
            public static List<T> ConvertToList<T>(DataTable dt)
            {
                var columnNames = dt.Columns.Cast<DataColumn>().Select(c => c.ColumnName.ToLower()).ToList();
                var properties = typeof(T).GetProperties();
                return dt.AsEnumerable().Select(row => {
                    var objT = Activator.CreateInstance<T>();
                    foreach (var pro in properties)
                    {
                        if (columnNames.Contains(pro.Name.ToLower()))
                        {
                            try
                            {
                                pro.SetValue(objT, row[pro.Name]);
                            }
                            catch (Exception) { }
                        }
                    }
                    return objT;
                }).ToList();
            }
        }
    }
}
