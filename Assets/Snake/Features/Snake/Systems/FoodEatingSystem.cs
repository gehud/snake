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
	using System.Collections.Generic;
	using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class FoodEatingSystem : ISystem, IAdvanceTick {
		public World world { get; set; }

		private Filter foodFilter;
		private Filter snakeFilter;

		private int appleCounter = 0;

		void ISystemBase.OnConstruct() {
			foodFilter = Filter.Create().With<Food>().With<Cell>().Push();
			snakeFilter = Filter.Create().With<SnakeHead>().With<Cell>().Push();
		}

		void ISystemBase.OnDeconstruct() { }

		void IAdvanceTick.AdvanceTick(in float deltaTime) {
			foreach (var foodEntity in foodFilter) {
				var foodCoordinate = foodEntity.Get<Cell>().Coordinate;
				foreach (var snakeEntity in snakeFilter) {
					var snakeCooordinate = snakeEntity.Get<Cell>().Coordinate;
					if (foodCoordinate == snakeCooordinate) {
						var foodType = foodEntity.Get<Food>().Type;
						int additionalCount = 0;
						switch (foodType) {
							case FoodType.Apple:
								++appleCounter;
								additionalCount = 1;
								break;
							case FoodType.Banana:
								additionalCount = 2;
								break;
						}

						var newFoodType = FoodType.Apple;
						if (appleCounter == 10) {
							newFoodType = FoodType.Banana;
							appleCounter = 0;
						}

						world.AddEntity().Set(new FoodAuthoring { 
							Type = newFoodType
						});

						for (int i = 0; i < additionalCount; i++) {
							world.AddEntity().Set<SnakeAuthoring>();
						}

						foodEntity.Destroy();

						break;
					}
				}
			}

		}
	}
}