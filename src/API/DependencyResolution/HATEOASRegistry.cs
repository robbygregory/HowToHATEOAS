using Lamar;
using RiskFirst.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HowToHATEOAS.API.DependencyResolution
{
	public class HATEOASRegistry : ServiceRegistry
	{
		public HATEOASRegistry()
		{
			For<ILinksService>().Use<DefaultLinksService>();
		}
	}
}
