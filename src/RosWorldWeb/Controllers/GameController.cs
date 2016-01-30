using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using RosWorldWeb.Game;
using RowWorld.Ecs.Dtos;

namespace RosWorldWeb.Controllers
{
	public class GameController : Controller
	{
		private readonly SrvEntityDtoCreator _srvEntityDto;
		public GameController(SrvEntityDtoCreator _srvEntityDto)
		{
			this._srvEntityDto = _srvEntityDto;
		}

		[HttpPost]
		public string GetEngine()
		{
			GameConfig.Engine.ProcessSystems();
			var result = JsonConvert.SerializeObject(GameConfig.Engine);
			//var result = JsonConvert.SerializeObject(GameConfig.Engine.GetEntitiesByComponents(typeof(GoldComponent)));

			return result;
		}

		[HttpPost]
		public string GetEntities()
		{
			GameConfig.Engine.ProcessSystems();
			var result = JsonConvert.SerializeObject(GameConfig.Engine.GetEntitiesByComponents().Select(e => _srvEntityDto.CreateDto(e)));
			
			return result;
		}


		[HttpPost]
		public bool Player1BuildHouse()
		{
			return GameConfig.Player1.BuildHouse();
		}

		[HttpPost]
		public void Reset()
		{
			GameConfig.Config();
		}


	}
}