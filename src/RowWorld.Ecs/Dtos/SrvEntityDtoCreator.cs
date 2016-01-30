using System.Collections.Generic;
using System.Dynamic;

namespace RowWorld.Ecs.Dtos
{
	public class SrvEntityDtoCreator
	{
		public dynamic CreateDto(Entity entity)
		{
			var result = new ExpandoObject() as IDictionary<string, object>;

			result.Add("Id", entity.Id);
			foreach (var component in entity.Components)
			{
				result.Add(component.GetType().Name, component);
			}

			return result;
		}
	}
}
