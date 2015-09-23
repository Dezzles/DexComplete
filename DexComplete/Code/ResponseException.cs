using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DexComplete.Code
{
	public class ExceptionResponse : Exception
	{
		public ExceptionResponse(string Message)
			: base(Message)
		{
		}
	}

	public class Exception404 : Exception
	{
		public Exception404() 
			: base("Not found")
		{

		}
	}

	public class ExceptionMaintenance : Exception
	{
		public ExceptionMaintenance()
			: base("Server is closed for maintenance")
		{

		}
	}
}
