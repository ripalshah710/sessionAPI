using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInfoApi.Model
{
    public class SessionInfo
    {
        public SessionBasicDetails basicDetails { get; set; }
        public List<SessionCredits> credits { get; set; }
        public SessionPresenter presenterSid { get; set; }
        public SessionExclusiveItems exclusiveItems { get; set; }
        public SessionLocation location { get; set; }
        public SessionMemberGroup memberGroup { get; set; }
        public List<SessionSchedule> schedule { get; set; }
        public SessionPromoCode promo { get; set; }
        public SessionDiscount discount { get; set; }
        public SessionImage sessionImage { get; set; }


    }
    public class SessionBasicDetails
    {
        public int event_id { get; set; }
        public double fee { get; set; }
        public int limit { get; set; }
        public DateTime regStart { get; set; }
        public DateTime regEnd { get; set; }
        public string contractAmount { get; set; }
        public string fee_early { get; set; }
        public string regearly { get; set; }
        public int facilitator_id { get; set; }
        public int contactperson_id { get; set; }
        public int eval_id { get; set; }
        public int noEvalOnline { get; set; }
        public int noCatalog { get; set; }
        public int notOnline { get; set; }
        public string webcomments { get; set; }
        public string subTitle { get; set; }
        public string confirmationComments { get; set; }
        public string internalComments { get; set; }
        public int breakoutsession_id { get; set; }
        public string breakoutDisplay { get; set; }
        public string breakoutCode { get; set; }
        public int breakoutSort { get; set; }
        public int breakoutEnabled { get; set; }
        public int approved { get; set; }
        public int numOnWaitingList { get; set; }
        public int allowPO { get; set; }
        public int noWaitinglist { get; set; }
        public int subscription_length_id { get; set; }
        public int subscription_length { get; set; }
        public string organization_id { get; set; }
        public int active { get; set; }
        public int numRegistered { get; set; }
        public int Online_Type_ID { get; set; }
        public string instructors { get; set; }
        public string PrerequisiteSessionID { get; set; }
        public string NextSessionID { get; set; }
        public string dimensions { get; set; }
        public string standards { get; set; }
        public int LMSTypeID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string contactperson_firstname { get; set; }
        public string contactperson_lastname { get; set; }
        public string contactperson_email { get; set; }
        public  int sessionId { get; set; }

    }
    public class SessionCredits
    {
        public int creditID { get; set; }
        public string creditDisplay { get; set; }
        public int creditEnabled { get; set; }       
        public string creditCode { get; set; }
        public int creditSort { get; set; }
        public double amount { get; set; }
    }
    public class SessionPresenter
    {
        public Guid sid { get; set; } 
    }
    public class SessionExclusiveItems
    {
        public int item_pk { get; set; }
        public string display { get; set; }
        public string code { get; set; }
        public int sort { get; set; }
        public int enabled { get; set; }
    }
    public class SessionLocation
    {
        public int location_id { get; set; }
        public string location_type { get; set; }
    }
    public class SessionMemberGroup
    {
        public int groupID { get; set; }
        public string groupDisplay { get; set; }
        public string groupCode { get; set; }
        public int groupSort { get; set; }
        public int groupEnabled { get; set; }
        public decimal groupFee { get; set; }
    }
    public class SessionSchedule
    {
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int location_id { get; set; }
        public string display { get; set; }
        public string street1 { get; set; }
        public string street2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }

    }
    public class SessionPromoCode
    {
        public string promotionalCode { get; set; }
        public double promotionalFee { get; set; }
    }
    public class SessionDiscount
    {
        public string code { get; set; }
        public string display { get; set; }
        public double fee { get; set; }
    }
    public class SessionImage
    {
        public byte[] image_content { get; set; }
    }
}
