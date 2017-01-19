using System;
using System.Runtime.Serialization;

namespace PetCenter.BusinessCommon
{
	[Serializable]
	public class PetCenterBusinessException : Exception
	{
		private Exception businessException;
		private string message;

		public Exception BusinessException
		{
			get { return this.businessException; }
		}

		public override string Message
		{
			get { return this.message; }
		}

		public PetCenterBusinessException(Exception exception)
		{
			this.businessException = exception;
			this.message = "There has been an exception in the Business Layer - " + exception.Message;
		}

        public PetCenterBusinessException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
            : base(info, context) 
		{

			this.businessException = (Exception)  info.GetValue("_BussinessException", typeof(Exception)   );
			this.message = info.GetString("_Message");
	            
		}

		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("_BussinessException", businessException);
			info.AddValue("_Message", message);  
		} 
	}
}