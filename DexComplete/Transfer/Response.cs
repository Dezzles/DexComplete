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
		public bool Volatile { get; set; } = true;
		public static Response Succeed(bool Volatile = true)
		{
			return new Response()
			{
				Message = "",
				Status = ResponseCode.Success,
				Volatile = Volatile
			};
		}
		public static Response Succeed(object obj, bool Volatile = true)
		{
			return new Response()
			{
				Message = "",
				Status = ResponseCode.Success,
				Value = obj,
				Volatile = Volatile
			};
		}
		public static Response Error(string Error, bool Volatile = true)
		{
			return new Response()
			{
				Status = ResponseCode.Error,
				Message = Error,
				Value = null,
				Volatile = Volatile
			};
		}

		public static Response Maintenance(string Message, bool Volatile = true)
		{
			return new Response()
			{
				Status = ResponseCode.Maintenance,
				Message = Message,
				Value = null,
				Volatile = Volatile
			};
		}

		public static Response NotLoggedIn()
		{
			return new Response()
			{
				Status = ResponseCode.NotLoggedIn,
				Volatile = true
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
