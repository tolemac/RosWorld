using RosWorldWeb.Game;
using RowWorld.Ecs;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(GameConfig), "Config")]


namespace RosWorldWeb.Game
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
		public static int PeopleIncrementPerHouse = 5;
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
				return true;
			}

			return false;
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
			entity.GetComponent<GoldComponent>().Amount += (engine.DeltaMs * amountPerSecond / 1000.0) * people;
		}

		#endregion
	}


	public class GameConfig
	{
		public static Engine Engine;
		public static Player Player1;

		public static void Config()
		{
			var engine = Engine = new Engine();

			Player1 = (Player) engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(1));
			engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(2));
			engine.AddSystem(new GoldSystem());

			engine.Start();

		}


	}
}
