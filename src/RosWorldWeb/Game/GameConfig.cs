using RosWorldWeb.Game;
using RowWorld.Ecs;
using RowWorld.Ecs.Components;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(GameConfig), "Config")]

namespace RosWorldWeb.Game
{

	public class GoldComponent : DoubleComponent
	{
		public GoldComponent(double amount) : base(amount)
		{
		}
	}

	public class PeopleComponent : LongComponent
	{
		public PeopleComponent(long amount) : base(amount)
		{
		}
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
			if (gold.Value > HousesComponent.HousePrice)
			{
				gold.Value -= HousesComponent.HousePrice;
				if (!HasComponent<HousesComponent>())
					AddComponent(new HousesComponent(0));
				GetComponent<HousesComponent>().Amount++;
				GetComponent<PeopleComponent>().Value += HousesComponent.PeopleIncrementPerHouse;
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
			var people = entity.GetComponent<PeopleComponent>().Value;
			entity.GetComponent<GoldComponent>().Value += (engine.DeltaMs * amountPerSecond / 1000.0) * people;
		}

		#endregion
	}


	public class GameConfig
	{
		public static Engine Engine;
		public static Player Player1;
		public static Player Player2;

		public static void Config()
		{
			var engine = Engine = new Engine();

			Player1 = (Player)engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(1));
			Player2 = (Player)engine.CreateEntity<Player>().AddComponent(new GoldComponent(0)).AddComponent(new PeopleComponent(2));
			engine.AddSystem(new GoldSystem());

			engine.Start();

		}


	}
}
