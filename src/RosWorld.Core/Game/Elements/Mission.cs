using System;
using System.Collections.Generic;

namespace RosWorld.Game.Elements
{
	public class Mission
	{
		public ICollection<RawMaterial> RawMaterials { get; set; }
		public ICollection<Goal> Goals { get; set; }
		public ICollection<Event> Events { get; set; }
	}
}
