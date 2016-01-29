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

    public class HousesComponent : Component
    {
        public static int HousePrice = 100;
        public static int PeopleIncrementPerHouse = 10;
        public HousesComponent(long amount)
        {
            Amount = amount;
        }

        public long Amount { get; set; }
    }

	public class Player : Entity
	{
	    public bool BuildHouse()
	    {
	        var gold = GetComponent<GoldComponent>();
	        if (gold.Amount > HousesComponent.HousePrice)
	        {
	            gold.Amount -= HousesComponent.HousePrice;
	            if (!HasComponent<HousesComponent>())
	                AddComponent(new HousesComponent(0));
	            GetComponent<HousesComponent>().Amount++;
	            GetComponent<PeopleComponent>().Amount += HousesComponent.PeopleIncrementPerHouse;
	        }

	        return true;
	    }
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

			var player1 = (Player) engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(1));
			engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(2));
			engine.AddSystem(new GoldSystem());

			engine.Start();

			ConsoleKeyInfo key;
			while ((key = Console.ReadKey()).Key != ConsoleKey.Q)
			{
			    if (key.Key == ConsoleKey.H)
			        player1.BuildHouse();

				engine.ProcessSystems();
				foreach (var entity in engine.GetEntitiesByComponents(typeof(GoldComponent)))
				{
					Console.WriteLine($"Entity {entity.Id} =>");
                    Console.WriteLine($"    Gold: {entity.GetComponent<GoldComponent>().Amount} - in {(DateTime.Now - engine.StartTime).TotalSeconds} seconds");
                    Console.WriteLine($"    People: {entity.GetComponent<PeopleComponent>().Amount}");
                    if (entity.HasComponent<HousesComponent>())
                        Console.WriteLine($"    Houses: {entity.GetComponent<HousesComponent>().Amount}");
                }
            }
		}
	}
}
