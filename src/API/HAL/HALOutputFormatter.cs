using HowToHATEOAS.Core.Domain.Model;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using RiskFirst.Hateoas;
using RiskFirst.Hateoas.Models;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HowToHATEOAS.API.HAL
{
	public class HalOutputFormatter : JsonOutputFormatter
	{

		public HalOutputFormatter(JsonSerializerSettings serializerSettings, ArrayPool<char> charPool) : base(serializerSettings, charPool)
		{
			SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("application/hal+json"));
		}

		public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
		{
			IServiceProvider serviceProvider = context.HttpContext.RequestServices;
			var linksService = serviceProvider.GetService(typeof(ILinksService)) as ILinksService;

			if (context.Object is IEnumerable<LinkContainer>)
			{
				foreach (LinkContainer model in context.Object as IEnumerable<LinkContainer>)
				{
					await linksService.AddLinksAsync(model);
				}
			}
			else
			{
				var model = context.Object as LinkContainer;
				await linksService.AddLinksAsync(model);
			}
			await base.WriteResponseBodyAsync(context, selectedEncoding);
		}
	}
}
