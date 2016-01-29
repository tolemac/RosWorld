using System;
using System.Collections.Generic;
using System.Linq;

namespace RowWorld.Ecs
{
	public abstract class EcsSystem
	{
		protected IList<Type> Components = new List<Type>();

		public abstract void SetComponents();

		protected abstract void ProcessEntity(Engine engine, Entity entity);

		public void Process(Engine engine)
		{
			var list = engine.GetEntitiesByComponents(Components.ToArray());
			foreach (var entity in list)
			{
				ProcessEntity(engine, entity);
			}
		}
	}
}
