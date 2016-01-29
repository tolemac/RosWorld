using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RowWorld.Ecs
{
	public class Engine
	{
		private int _currentId;
		private readonly Dictionary<long, Entity> _entities = new Dictionary<long, Entity>();
		private readonly List<EcsSystem> _systems = new List<EcsSystem>();
		
		private void AddEntity(Entity entity)
		{
			_entities.Add(_currentId, entity);
			entity.Id = _currentId++;
		}

		public EcsSystem AddSystem(EcsSystem system)
		{
			_systems.Add(system);
			system.SetComponents();
			return system;
		}

		public TEntity CreateEntity<TEntity>() where TEntity : Entity
		{
			var entity = Activator.CreateInstance<TEntity>();
			AddEntity(entity);
			return entity;
		}

		public IEnumerable<Entity> GetEntitiesByComponents(params Type[] componentTypes)
		{
			return _entities.Where(e =>
			{
				return e.Value.ComponentTypes.Any() && componentTypes.All(t =>
				{
					return e.Value.ComponentTypes.Contains(t);
				});
			}).Select(e => e.Value);
		}

		public DateTime StartTime { get; set; }

		public void Start()
		{
			StartTime = CurrentProcessTime = LastProcessTime = DateTime.Now;
		}

		public DateTime CurrentProcessTime { get; set; }
		public DateTime LastProcessTime { get; set; }

		public long DeltaMs => (long) (CurrentProcessTime - LastProcessTime).TotalMilliseconds;

		public void ProcessSystems()
		{
			CurrentProcessTime = DateTime.Now;
			foreach (var system in _systems)
			{
				system.Process(this);
			}
			LastProcessTime = DateTime.Now;
		}
	}
}
