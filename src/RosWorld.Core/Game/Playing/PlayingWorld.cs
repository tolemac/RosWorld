using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosWorld.Game.Elements;

namespace RosWorld.Game.Playing
{
	public class PlayingWorld
	{
		public World World { get; set; }
		public Player Player { get; set; }

		public ICollection<PlayingMission> PlayingMissions { get; set; }
	}
}
