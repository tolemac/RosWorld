using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RowWorld.Ecs;

namespace TestEngineConsoleApplication
{
	public class GoldComponent : Component
	{
		public GoldComponent(long amount)
		{
			Amount = amount;
		}

		public double Amount { get; set; }
	}

	public class PeopleComponent : Component
	{
		public PeopleComponent(long amount)
		{
			Amount = amount;
		}

		public double Amount { get; set; }
	}

	public class Player : Entity
	{
		
	}

	public class GoldSystem : EcsSystem
	{
		#region Overrides of EcsSystem

		public override void SetComponents()
		{
			Components.Add(typeof(GoldComponent));
			Components.Add(typeof(PeopleComponent));
		}

		protected override void ProcessEntity(Engine engine, Entity entity)
		{
			// 2 per 1000 milliseconds
			var amountPerSecond = 2;
			var people = entity.GetComponent<PeopleComponent>().Amount;
			entity.GetComponent<GoldComponent>().Amount += (engine.DeltaMs*amountPerSecond/1000.0) * people;
		}

		#endregion
	}

	public class Program
	{
		static void Main(string[] args)
		{
			var engine = new Engine();

			engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(1));
			engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(2));
			engine.AddSystem(new GoldSystem());

			engine.Start();

			ConsoleKeyInfo key;
			while ((key = Console.ReadKey()).Key != ConsoleKey.Q)
			{
				engine.ProcessSystems();
				foreach (var entity in engine.GetEntitiesByComponents(typeof(GoldComponent)))
				{
					Console.WriteLine($"Entity {entity.Id} => Gold: {entity.GetComponent<GoldComponent>().Amount} - in {(DateTime.Now - engine.StartTime).TotalSeconds} seconds");
				}
			}
		}
	}
}
