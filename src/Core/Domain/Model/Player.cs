using HowToHATEOAS.Core.Domain.Services;
using RiskFirst.Hateoas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HowToHATEOAS.Core.Domain.Model
{
	public class Player: LinkContainer
	{
		public string Id { get; set; }
		public string LastName { get; set; }
		public short Number { get; set; }
		public Status Status { get; set; }
		public IEnumerable<Status> AvailableStatuses { get { return StatusService.GetAvaialableStatuses(Status); } }

		public Player() { }
		public Player(string id, string lastName, short number, Status status) : base()
		{
			Id = id;
			LastName = lastName;
			Number = number;
			Status = status;
		}
	}
}
