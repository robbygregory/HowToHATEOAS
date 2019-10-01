using HowToHATEOAS.Core.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace HowToHATEOAS.API.Model.Json
{
	public class PlayerModel
	{
		public string Id { get; set; }
		public string LastName { get; set; }
		public short Number { get; set; }
		public Status Status { get; set; }
		[IgnoreDataMember]
		public IEnumerable<Status> AvailableStatuses { get; set; }



	}
}
