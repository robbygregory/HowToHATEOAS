using HowToHATEOAS.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HowToHATEOAS.Core.Domain.Services
{
	public static class StatusService
	{
		private static IList<Status> _allStatuses = new List<Status>
		{
			new Status(1, "Active"),
			new Status(2, "Injured"),
			new Status(3, "Suspended"),
			new Status(4, "Retired")
		};

		public static IEnumerable<Status> GetAvaialableStatuses(Status status)
		{
			return _allStatuses.Where(x => x.Id != status.Id);
		}

	}
}
