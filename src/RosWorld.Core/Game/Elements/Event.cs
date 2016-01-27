using System;
using RosWorld.Engine;

namespace RosWorld.Game.Elements
{
	public class Event
	{
		public TimeSpan When { get; set; }
		public Script Condition { get; set; }
		public Script Action { get; set; }
	}
}
