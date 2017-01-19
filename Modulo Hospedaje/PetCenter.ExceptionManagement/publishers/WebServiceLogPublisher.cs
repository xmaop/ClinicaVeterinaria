using System;
using PetCenter.ExceptionManagement.LoggerManager;

namespace PetCenter.ExceptionManagement
{
	public class WebServiceLogPublisher : IExceptionXmlPublisher
	{
		void IExceptionXmlPublisher.Publish(System.Xml.XmlDocument exceptionInfo, System.Collections.Specialized.NameValueCollection configSettings)
		{
			try
			{
				PetCenter.ExceptionManagement.LoggerManager.LoggerManager ObjMgr = new PetCenter.ExceptionManagement.LoggerManager.LoggerManager();
				string strValue = @"<SCDCExceptions xmlns=""http://tempuri.org/SCDC.Log.xsd"">" + 
					exceptionInfo.InnerXml +  "</SCDCExceptions>";
				ObjMgr.RecordLog( strValue );
			}
			catch {}
		}
	}
}