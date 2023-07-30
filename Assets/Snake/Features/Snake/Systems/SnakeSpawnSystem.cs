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
	using Systems;
	using UnityEngine;
#pragma warning restore

#if ECS_COMPILE_IL2CPP_OPTIONS
    [Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.NullChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.ArrayBoundsChecks, false),
     Unity.IL2CPP.CompilerServices.Il2CppSetOptionAttribute(Unity.IL2CPP.CompilerServices.Option.DivideByZeroChecks, false)]
#endif
	public sealed class SnakeSpawnSystem : ISystemFilter {
		private SnakeFeature feature;

		public World world { get; set; }

		private Entity tail;

		void ISystemBase.OnConstruct() {
			this.GetFeature(out feature);
		}

		void ISystemBase.OnDeconstruct() { }

#if !CSHARP_8_OR_NEWER
		bool ISystemFilter.jobs => false;

		int ISystemFilter.jobsBatchCount => 64;
#endif
		Filter ISystemFilter.filter { get; set; }

		Filter ISystemFilter.CreateFilter() {
			return Filter
				.Create("Filter-SnakeSpawnSystem")
				.With<SnakeAuthoring>()
				.Push();
		}

		void ISystemFilter.AdvanceTick(in Entity entity, in float deltaTime) {
			Vector2Int tailCoordinate;
			if (tail.IsEmpty()) {
				entity.Set<SnakeHead>();
				tailCoordinate = Vector2Int.zero;
			} else {
				tailCoordinate = tail.Get<Cell>().Coordinate;
			}

			entity.Set(new CellAuthoring {
				Coordinate = tailCoordinate,
				Material = feature.SnakeMaterial
			});

			entity.Set(new SnakeBody {
				Next = tail,
				LastCoordinate = tailCoordinate,
			});

			tail = entity;

			entity.Remove<SnakeAuthoring>();
		}
	}
}