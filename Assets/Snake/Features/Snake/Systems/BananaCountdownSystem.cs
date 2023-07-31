using ME.ECS;
using Snake.Features.Grid.Components;

namespace Snake.Features.Snake.Systems {
#pragma warning disable
	using Components;
	using Markers;
	using Modules;
	using Snake.Components;
	using Snake.Markers;
	using Snake.Modules;
	using Snake.Systems;
	using System;
	using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class BananaCountdownSystem : ISystem, IAdvanceTick {
		public static event Action<float> OnCountdown;

		private SnakeFeature feature;

		public World world { get; set; }

		private Entity banana;

		private float elapsedTime = 0.0f;

		void ISystemBase.OnConstruct() {
			this.GetFeature(out feature);
		}

		void ISystemBase.OnDeconstruct() { }

		void IAdvanceTick.AdvanceTick(in float deltaTime) {
			if (world.GetMarker(out BananaMarker bananaMarker)) {
				banana = bananaMarker.Entity;
				world.RemoveMarker<BananaMarker>();
			}

			if (banana.IsEmpty()) {
				return;
			}

			if (!banana.IsAlive()) {
				elapsedTime = 0.0f;
				OnCountdown?.Invoke(0.0f);
				banana = Entity.Empty;
				return;
			}

			elapsedTime += deltaTime;
			OnCountdown?.Invoke(1.0f - elapsedTime / 5.0f);
			if (elapsedTime >= 5.0f) {
				var coordinate = banana.Get<Cell>().Coordinate;
				banana.Destroy();
				banana = Entity.Empty;

				var newBanana = Entity.Create();

				newBanana.Set(new CellAuthoring {
					Coordinate = coordinate,
					Material = feature.AppleMaterial
				});

				newBanana.Set(new Food {
					Type = FoodType.Apple
				});

				elapsedTime = 0.0f;
			}
		}
	}
}