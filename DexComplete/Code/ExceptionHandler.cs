using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace DexComplete.Code
{
	class OopsExceptionHandler : ExceptionHandler
	{
		public override void Handle(ExceptionHandlerContext context)
		{
			if (context.Exception is Code.ExceptionResponse)
			{
				Code.ExceptionResponse ex = context.Exception as Code.ExceptionResponse;
				context.Result = new TextPlainErrorResult
				{
					Request = context.ExceptionContext.Request,
					Content = Transfer.Response.Error(ex.Message)
				};
			}
			else if (context.Exception is Code.ExceptionMaintenance)
			{
				context.Result = new TextPlainErrorResult
				{
					Content = Transfer.Response.Maintenance("Server is down for maintenance")
				};
			}
			else
			{
				context.Result = new TextPlainErrorResult
				{
					Request = context.ExceptionContext.Request,
					Content = Transfer.Response.Error(context.Exception.Message)
				};
			}
		}

		private class TextPlainErrorResult : IHttpActionResult
		{
			public HttpRequestMessage Request { get; set; }

			public Transfer.Response Content { get; set; }

			public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
			{
				HttpResponseMessage response =
								 new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ObjectContent(typeof(Transfer.Response), Content, new JsonMediaTypeFormatter());
				response.RequestMessage = Request;
				return Task.FromResult(response);
			}
		}
	}
}
