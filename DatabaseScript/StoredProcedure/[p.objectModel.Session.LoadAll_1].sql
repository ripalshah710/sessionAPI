--exec [p.objectModel.Session.LoadAll_1] 1627527  
  
CREATE Procedure [dbo].[p.objectModel.Session.LoadAll_1]  
(   
       @session_id int  
)  
AS  
--1. Load Basic  
select  
       [session].[event_id],  
       [session].[fee_s] [fee],  
       [session].[limit],  
       [session].[regStart],  
       [session].[regEnd],  
       [contractAmount] = c.Fee,  
       [session].[fee_early],  
       [session].[regEarly],  
       [session].[facilitator_id],  
       [contactperson_id] = ContactPerson.user_pk,  
       [session].[eval_id],  
       [noEvalOnline] = CONVERT(BIT, 0), --NOT in database  
       [session].[noCatalog],  
       [notOnline] = [session].web,  
       webcomments = [session].[Comments],  
       internalComments = [session].resource_notes,  
       [confirmationComments] = [session].[confirmation],  
       [session].[subTitle],  
       isnull([session].[breakoutsession_id],0) [breakoutsession_id],  
       isnull([breakout].[display], 'Choose an Option') [breakoutDisplay],  
       isnull([breakout].[code],'') [breakoutCode],  
       isnull([breakout].[sort],Round(( ( 10000 - 1000 - 1 ) * Rand() + 1000 ), 0)) [breakoutSort], -- ticket 15551  
       isnull([breakout].[enabled],0) [breakoutEnabled],  
       [approved] = CASE WHEN [session].approved =1 OR [session].[status_id] = 1 THEN CONVERT(BIT, 1) ELSE CONVERT(BIT, 0) END,  
       [dbo].[fn.objectModel.Session.NumOnWaitinglist]([session].[obj_id]) [numOnWaitingList],  
       [dbo].[fn.objectModel.Session.NumRegistered]([session].[obj_id]) [numRegistered],  
       active = ~[session].[delete], --Delete is not active  
       null [organization_id],  
       isnull([session].Online_Type_ID, 0) Online_Type_ID,  
       subscription_length.display AS subscription_length,  
       subscription_length.item_pk AS subscription_length_id,   
       [session].noWaitinglist,  
       [session].allowPO,   
       instructors = dbo.[fn.session.presenters]([session].obj_id),  
       [session].PrerequisiteSessionID,   
       NextSessionID = s2.obj_id  
       ,ec.dimensions,  
       ec.standards  
          ,ISNULL([session].[LMS_Type_ID],-1) LMSTypeID ,
		   e.title,
		  e.description,
		 ContactPerson.fname [contactperson_firstname],
		 ContactPerson.lname [contactperson_lastname],
		 account.uid [contactperson_email],
	    [session].obj_id [sessionId]
from  
       [event.session] [session]  WITH (NOLOCK)   
              left join [escworks.item] [breakout]   WITH (NOLOCK) on [breakout].item_pk = [session].[breakoutsession_id]  
              left join [escworks.user] eu   WITH (NOLOCK) ON [session].contactpersonsid = eu.[sid]  
              left join  [user] ContactPerson   WITH (NOLOCK) ON eu.activeuser = ContactPerson.user_pk  
              left join [event.session.contract] c   WITH (NOLOCK) ON c.obj_id = [session].obj_id  
              left join [escworks.item] subscription_length   WITH (NOLOCK) ON subscription_length.item_pk = [session].subscription_length  
              left join [event.session] s2  WITH (NOLOCK) ON s2.PrerequisiteSessionID = [session].obj_id  
              inner join [event] e on e.obj_id = session.event_id  
              left join [event.cube] ec on ec.obj_id =e.obj_id 
			    left join [escworks.user.account] account WITH (NOLOCK) on account.sid=eu.sid
      
where  
       [session].[obj_id] = @session_id  
         
--2. Load Credits      
select  
       [credit].[item_pk] [creditID],  
       [credit].[display] [creditDisplay],  
       [credit].[enabled] [creditEnabled],  
       [credit].[sort] [creditSort],  
       [credit].[code] [creditCode],  
       [event.session.multi.credits].amount  
from  
       [event.session.multi.credits]  WITH (NOLOCK)   
              inner join [escworks.item] [credit]   WITH (NOLOCK) on [credit].item_pk = [event.session.multi.credits].[credit_id]  
where  
       [event.session.multi.credits].[obj_id]=@session_id  
  
--3. Load Additional Instructors  
select  
       [event.session.multi.presenter].[sid]  
from   
       [event.session.multi.presenter]  WITH (NOLOCK)   
where  
       [event.session.multi.presenter].[obj_id] = @session_id  
  
--4. LoadExclusiveItems  
select   
       item_pk,  
       display,  
       code,  
       sort,  
       enabled  
from  
       [event.session.exclusive.item]  WITH (NOLOCK)   
              inner join [escworks.item]   WITH (NOLOCK) on [escworks.item].item_pk = [event.session.exclusive.item].item_id  
where  
       [event.session.exclusive.item].[obj_id] = @session_id  
         
--5. LoadExclusiveSites  
select   
       location_id = [exclusive.location.site_id],  
       location_type = 'site'  
from [event.session]  WITH (NOLOCK)   
where obj_id = @session_id AND [exclusive.location.site_id] IS NOT NULL  
UNION  
select   
       location_id = [location.site_id],  
       location_type = 'room'  
from [event.session.exclusive.location]  WITH (NOLOCK)   
where obj_id = @session_id  
         
--6. LoadMemberGroupFees     
select   
       [group].item_pk [groupID],  
       [group].display [groupDisplay],  
       [group].code [groupCode],  
       [group].[sort] [groupSort],  
       [group].[enabled] [groupEnabled],  
       [event.session.multi.fee].[fee] [groupFee]  
from  
       [event.session.multi.fee] WITH (NOLOCK)   
              inner join [escworks.item] [group]  WITH (NOLOCK) on [group].[item_pk] = [event.session.multi.fee].[itm_id]  
where  
       [event.session.multi.fee].[obj_id] = @session_id  
                
--7. LoadSchedule            
select  
       [begin] [startDate],  
       [end] [endDate],  
       [location_id]  
from   
       [event.session.schedule] WITH (NOLOCK)   
where  
       [event.session.schedule].[obj_id] = @session_id  
                
--8. LoadPromoCodes  
select  
       [promo].[promotionalCode],  
       [promo].[promotionalFee]  
from  
       [event.session.multi.promocode] [promo]  WITH (NOLOCK)   
where   
       [promo].[obj_id] = @session_id  
  
--9. Group Discount  
SELECT   
       ei.code,   
       ei.display as [description],   
       ssmm.fee  
FROM [event.session.multi.multienrollfee] ssmm  WITH (NOLOCK)   
       INNER JOIN [escworks.item] ei   WITH (NOLOCK) ON ssmm.[attendeeFeeGroup.itm_id] = ei.item_pk AND ei.itemgroup_id = 1121  
WHERE   
       obj_id = @session_id  
ORDER BY ei.sort       
--10. Session Image  
 SELECT [binary] as image_content from [event.session.uploadcontent] [uploadcontent] where [uploadcontent].obj_id=@session_id  