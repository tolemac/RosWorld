using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace RowWorld.Ecs.Components
{
	public class ValueComponent<TValue> : Component
	{
		public ValueComponent(TValue value)
		{
			Value = value;
		}

		public TValue Value { get; set; }
	}
}
