using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HowToHATEOAS.Core.Domain.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiskFirst.Hateoas;

namespace HowToHATEOAS.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PlayerController : ControllerBase
	{
		private readonly ILinksService _linksService;
		public PlayerController(ILinksService linksService)
		{
			_linksService = linksService;
		}

		[HttpGet(Name = "GetPlayers")]
		public async Task<IEnumerable<Player>> Get()
		{
			var players = _allPlayers;
			foreach (var player in players)
			{
				await _linksService.AddLinksAsync(player);
			}
			return players;
		}

		[HttpGet("{id}", Name = "GetPlayerById")]
		public async Task<Player> Get(string id)
		{
			var player = _allPlayers.Where(x => x.Id == id.ToUpper()).FirstOrDefault();
			await _linksService.AddLinksAsync(player);
			return player;
		}

		[HttpPut("{id}/status/{status}", Name = "UpdatePlayerStatus")]
		public async Task<Player> Put(string id, string status)
		{
			//need to make this do stuff...  this is just a placeholder...
			var player = _allPlayers.Where(x => x.Id == id.ToUpper()).FirstOrDefault();
			await _linksService.AddLinksAsync(player);
			return player;
		}

		private IEnumerable<Player> _allPlayers = new List<Player>
		{
			new Player("KC5", "Brett", 5, new Status(4, "Retired")),
			new Player("KC15", "Merrifield", 15, new Status(1, "Active")),
			new Player("KC13", "Perez", 13, new Status(2, "Injured"))
		};
	}
}