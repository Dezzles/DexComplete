using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Transfer
{
	public class Response
	{
		public ResponseCode Status { get; set; }
		public string Message { get; set; }
		public object Value { get; set; }
		public static Response Succeed()
		{
			return new Response()
			{
				Message = "",
				Status = ResponseCode.Success
			};
		}
		public static Response Succeed(object obj)
		{
			return new Response()
			{
				Message = "",
				Status = ResponseCode.Success,
				Value = obj
			};
		}
		public static Response Error(string Error)
		{
			return new Response()
			{
				Status = ResponseCode.Error,
				Message = Error,
				Value = null
			};
		}

		public static Response Maintenance(string Message)
		{
			return new Response()
			{
				Status = ResponseCode.Maintenance,
				Message = Message,
				Value = null
			};
		}

		public static Response NotLoggedIn()
		{
			return new Response()
			{
				Status = ResponseCode.NotLoggedIn
			};
		}

	}
	public enum ResponseCode
	{
		Success,
		Error,
		NotLoggedIn,
		Maintenance,
		ServerError
	}

}
