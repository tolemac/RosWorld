using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RosWorld.Engine;
using RosWorld.Game.Elements;

namespace RosWorld.Game.Playing
{
	public class PlayingMission
	{
		public PlayingWorld PlayingWorld { get; set; }
		public DateTime StartTime { get; set; }
		public ICollection<Goal> AcquiredGoals { get; set; }
		public int People { get; set; }

		public Script CalculateScript { get; set; }

	}
}
