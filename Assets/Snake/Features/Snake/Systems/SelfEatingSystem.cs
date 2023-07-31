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
	using System.Collections.Generic;
	using Systems;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SelfEatingSystem : ISystem, IAdvanceTick {
		public static event Action OnSelfEating;

		public World world { get; set; }

		private Filter snakeHeadFilter;
		private Filter snakeBodyFilter;

		void ISystemBase.OnConstruct() {
			snakeHeadFilter = Filter.Create().With<SnakeHead>().With<Cell>().Push();
			snakeBodyFilter = Filter.Create().With<SnakeBody>().With<Cell>().Without<SnakeHead>().Push();
		}

		void ISystemBase.OnDeconstruct() { }

		void IAdvanceTick.AdvanceTick(in float deltaTime) {
			if (snakeHeadFilter.Count == 0) {
				return;
			}

			foreach (var snakeHeadEntity in snakeHeadFilter) {
				var coordinate = snakeHeadEntity.Get<Cell>().Coordinate;
				foreach (var snakeBodyEntity in snakeBodyFilter) {
					if (snakeBodyEntity.Get<Cell>().Coordinate == coordinate) {
						OnSelfEating?.Invoke();
						return;
					}
				}
			}
		}
	}
}