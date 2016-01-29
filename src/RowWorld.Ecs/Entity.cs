using System;
using System.Collections.Generic;

namespace RowWorld.Ecs
{
	public class Entity
	{
		public long Id { get; internal set; }
	
		private readonly Dictionary<Type, Component> _components = new Dictionary<Type, Component>();

		public IEnumerable<Component> Components => _components.Values;
		public IEnumerable<Type> ComponentTypes => _components.Keys;

		public Entity AddComponent(Type type, Component cmp)
		{
			_components.Add(type, cmp);
			return this;
		}

		public Entity AddComponent<TComponent>(TComponent cmp) where TComponent : Component
		{
			AddComponent(typeof (TComponent), cmp);
			return this;
		}

		public Component RemoveComponent(Type type)
		{
			var cmp = _components[type];
			_components.Remove(type);
			return cmp;
		}

		public TComponent RemoveComponent<TComponent>() where TComponent : Component
		{
			return (TComponent) RemoveComponent(typeof (TComponent));
		}

		public Component GetComponent(Type type)
		{
			return _components[type];
		}

		public TComponent GetComponent<TComponent>() where TComponent : Component
		{
			return (TComponent) GetComponent(typeof(TComponent));
		}

		public Component SetComponent(Type type, Component cmp)
		{
			_components[type] = cmp;
			return cmp;
		}

		public TComponent SetComponent<TComponent>(TComponent cmp) where TComponent : Component
		{
			return (TComponent) SetComponent(typeof (TComponent), cmp);
		}

		public Component this[Type type]
		{
			get { return GetComponent(type); }
			set { SetComponent(type, value); }
		}
	}
}
